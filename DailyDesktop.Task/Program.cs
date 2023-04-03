// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using System.CommandLine;
using System.CommandLine.Invocation;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using DailyDesktop.Core.Configuration;
using DailyDesktop.Core.Providers;
using DailyDesktop.Core.Util;
using SuperfastBlur;

namespace DailyDesktop.Task
{
    internal static class Program
    {
        private const string IMAGE_FILENAME = "Daily Desktop Wallpaper";
        private const double MAX_BLUR_FRACTION = 0.025;

        [DllImport("user32.dll")]
        private static extern bool SetProcessDPIAware();

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern int SystemParametersInfo(int uAction, int uParam, string lpvParam, int fuWinIni);

        private static int Main(string[] args)
        {
            RootCommand rootCommand = new RootCommand("Daily Desktop task target executable");
            rootCommand.AddArgument(new Argument<string>("dllPath"));
            rootCommand.AddOption(new Option<string>("--json", "", "Where to output the wallpaper info JSON file"));
            rootCommand.AddOption(new Option<bool>("--resize", false, "Resize to screen resolution if larger"));
            rootCommand.AddOption(new Option<int?>("--blur", () => null, "Use blurred-fit mode with the passed value for background blur strength"));
            rootCommand.Handler = CommandHandler.Create<string, string, bool, int?>(handleArguments);

            return rootCommand.Invoke(args);
        }

        private static async Task<int> handleArguments(string dllPath, string json, bool resize, int? blur)
        {
            if (string.IsNullOrWhiteSpace(dllPath))
                throw new ProviderException("Missing IProvider DLL module path");

            var store = new ProviderStore();
            var providerType = await store.Add(dllPath, AsyncUtils.TimedCancel());
            var provider = IProvider.Instantiate(providerType);

            string imagePath = await downloadWallpaper(provider, json, AsyncUtils.LongCancel());

            SetProcessDPIAware();

            if (resize)
                applyResize(imagePath);

            if (blur != null)
                applyBlurredFit(imagePath, blur.Value);

            string tiffPath = convertToTiff(imagePath);

            // https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-systemparametersinfoa#parameters
            // SPI_SETDESKWALLPAPER, 0, path, SPIF_UPDATEINIFILE | SPIF_SENDCHANGE
            return SystemParametersInfo(0x14, 0, tiffPath, 0x1 | 0x2);
        }

        private static async Task<string> downloadWallpaper(IProvider provider, string jsonPath, CancellationToken cancellationToken = default)
        {
            string imagePath = Path.Combine(Path.GetTempPath(), IMAGE_FILENAME);

            var wallpaperConfig = new WallpaperConfiguration(jsonPath);
            await provider.ConfigureWallpaperAsync(wallpaperConfig, cancellationToken);
            await wallpaperConfig.TrySerializeAsync(cancellationToken);

            provider.ConfigureHttpRequestHeaders(HttpUtils.Client.DefaultRequestHeaders);
            var stream = await HttpUtils.Client.GetStreamAsync(wallpaperConfig.ImageUri, cancellationToken);
            using (var fstream = new FileStream(imagePath, FileMode.OpenOrCreate))
                await stream.CopyToAsync(fstream, cancellationToken);

            return imagePath;
        }

        private static void applyResize(string imagePath)
        {
            Size screenSize = SystemInformation.PrimaryMonitorSize;
            float screenAspectRatio = (float)screenSize.Width / screenSize.Height;

            Bitmap image = new Bitmap(imagePath);

            float imageAspectRatio = (float)image.Width / image.Height;
            Size targetSize = new Size
            {
                Width = imageAspectRatio > screenAspectRatio ? screenSize.Width : (int)(screenSize.Height * imageAspectRatio),
                Height = imageAspectRatio > screenAspectRatio ? (int)(screenSize.Width / imageAspectRatio) : screenSize.Height,
            };

            if (targetSize.Width < image.Width)
            {
                using (Bitmap resized = new Bitmap(image, targetSize))
                {
                    image.Dispose();
                    resized.Save(imagePath);
                }
            }
            else
            {
                image.Dispose();
            }
        }

        private static void applyBlurredFit(string imagePath, int blurStrength)
        {
            Size screenSize = SystemInformation.PrimaryMonitorSize;
            float screenAspectRatio = (float)screenSize.Width / screenSize.Height;

            Bitmap image = new Bitmap(imagePath);
            float imageAspectRatio = (float)image.Width / image.Height;

            Rectangle backgroundRect;
            Size fillSize;
            if (imageAspectRatio < screenAspectRatio)
            {
                backgroundRect = new Rectangle
                {
                    Width = (int)(image.Height * screenAspectRatio),
                    Height = image.Height,
                };
                fillSize = new Size
                {
                    Width = backgroundRect.Width,
                    Height = (int)(backgroundRect.Width / imageAspectRatio),
                };
                backgroundRect.Y = (fillSize.Height - backgroundRect.Height) / 2;
            }
            else
            {
                backgroundRect = new Rectangle
                {
                    Width = image.Width,
                    Height = (int)(image.Width / screenAspectRatio),
                };
                fillSize = new Size
                {
                    Width = (int)(backgroundRect.Height * imageAspectRatio),
                    Height = backgroundRect.Height,
                };
                backgroundRect.X = (fillSize.Width - backgroundRect.Width) / 2;
            }

            GaussianBlur gaussianBlur;
            using (Bitmap fill = new Bitmap(image, fillSize))
            {
                using (Bitmap background = fill.Clone(backgroundRect, PixelFormat.Format32bppArgb))
                    gaussianBlur = new GaussianBlur(background);
            }

            int largestDim = (imageAspectRatio > 1) ? image.Width : image.Height;
            int radius = (int)(MAX_BLUR_FRACTION * largestDim * blurStrength / 100);

            using (Bitmap blurred = gaussianBlur.Process(radius))
            {
                using (Graphics graphics = Graphics.FromImage(blurred))
                {
                    int x = (blurred.Width - image.Width) / 2;
                    int y = (blurred.Height - image.Height) / 2;
                    graphics.DrawImage(image, x, y, image.Width, image.Height);
                }

                image.Dispose();
                blurred.Save(imagePath);
            }
        }

        private static string convertToTiff(string imagePath)
        {
            Bitmap image = new Bitmap(imagePath);

            ImageCodecInfo tiffCodecInfo = ImageCodecInfo.GetImageEncoders().First(c => c.FormatID == ImageFormat.Tiff.Guid);

            string tifPath = imagePath + ".tif";
            image.Save(tifPath, tiffCodecInfo, null);

            return tifPath;
        }
    }
}
