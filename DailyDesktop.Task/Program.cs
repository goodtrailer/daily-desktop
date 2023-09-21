// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using System.CommandLine;
using System.CommandLine.Invocation;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using DailyDesktop.Core.Configuration;
using DailyDesktop.Core.Providers;
using DailyDesktop.Core.Util;
using ImageMagick;

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
            var providerType = await store.AddAsync(dllPath, AsyncUtils.TimedCancel());
            var provider = IProvider.Instantiate(providerType);

            string imagePath = await downloadWallpaper(provider, json, AsyncUtils.LongCancel());
            string outputPath = imagePath + ".tif";

            SetProcessDPIAware();

            using var image = new MagickImage(imagePath);

            if (resize)
                applyResize(image);

            if (blur != null)
                applyBlurredFit(image, blur.Value);

            image.Format = MagickFormat.Tif;
            image.Write(outputPath);

            // https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-systemparametersinfoa#parameters
            // SPI_SETDESKWALLPAPER, 0, path, SPIF_UPDATEINIFILE | SPIF_SENDCHANGE
            return SystemParametersInfo(0x14, 0, outputPath, 0x1 | 0x2);
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

        private static void applyResize(MagickImage image)
        {
            var screenSize = SystemInformation.PrimaryMonitorSize;

            float screenAspectRatio = (float)screenSize.Width / screenSize.Height;
            float imageAspectRatio = (float)image.Width / image.Height;

            var targetSize = new Size
            {
                Width = imageAspectRatio > screenAspectRatio ? screenSize.Width : (int)(screenSize.Height * imageAspectRatio),
                Height = imageAspectRatio > screenAspectRatio ? (int)(screenSize.Width / imageAspectRatio) : screenSize.Height,
            };

            if (targetSize.Width < image.Width)
                image.Resize(targetSize.Width, targetSize.Height);
        }

        private static void applyBlurredFit(MagickImage image, int blurStrength)
        {
            var screenSize = SystemInformation.PrimaryMonitorSize;

            float screenAspectRatio = (float)screenSize.Width / screenSize.Height;
            float imageAspectRatio = (float)image.Width / image.Height;

            MagickGeometry backgroundRect;
            Size fillSize;
            if (imageAspectRatio < screenAspectRatio)
            {
                backgroundRect = new MagickGeometry
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
                backgroundRect = new MagickGeometry
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

            if (fillSize.Width == image.Width && fillSize.Height == image.Height)
                return;

            var fill = new MagickImage(image);
            fill.Resize(fillSize.Width, fillSize.Height);
            fill.Crop(backgroundRect);

            int largestDim = (imageAspectRatio > 1) ? image.Width : image.Height;
            int sigma = (int)(MAX_BLUR_FRACTION * largestDim * blurStrength / 100);
            fill.Blur(0, sigma);

            int x = (fill.Width - image.Width) / 2;
            int y = (fill.Height - image.Height) / 2;
            fill.Composite(image, x, y);
            image.Extent(fill.Width, fill.Height);
            image.CopyPixels(fill);
        }
    }
}
