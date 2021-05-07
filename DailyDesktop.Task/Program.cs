// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using System;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Windows.Forms;
using DailyDesktop.Core;
using DailyDesktop.Core.Providers;
using SuperfastBlur;

namespace DailyDesktop.Task
{
    internal class Program
    {
        private const string IMAGE_FILENAME = "Daily Desktop Wallpaper";
        private const double MAX_BLUR_FRACTION = 0.025;

        // args: key, blur strength
        private static int Main(string[] args)
        {
            RootCommand rootCommand = new RootCommand("Daily Desktop task target executable");
            rootCommand.AddArgument(new Argument<string>("dllPath"));
            rootCommand.AddOption(new Option<string>("--json", () => string.Empty, "Where to output the wallpaper info JSON file"));
            rootCommand.AddOption(new Option<int?>("--blur", () => null, "Use blurred-fit mode with the passed value for background blur strength"));
            rootCommand.Handler = CommandHandler.Create<string, string, int?>(handleArguments);

            return rootCommand.Invoke(args);
        }

        private static int handleArguments(string dllPath, string json, int? blur)
        {
            if (string.IsNullOrWhiteSpace(dllPath))
                throw new ProviderException("Missing IProvider DLL module path");

            ProviderStore store = new ProviderStore();
            Type providerType = store.Add(dllPath);
            IProvider provider = IProvider.Instantiate(providerType);

            string imagePath = downloadWallpaper(provider, json);

            if (blur != null)
                applyBlurredFit(imagePath, blur.Value);

            string jpgPath = convertToJpeg(imagePath);

            // https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-systemparametersinfoa#parameters
            // SPI_SETDESKWALLPAPER, 0, path, SPIF_UPDATEINIFILE | SPIF_SENDCHANGE
            return SystemParametersInfo(0x14, 0, jpgPath, 0x1 | 0x2);
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        // Can do a bunch of cool stuff, including setting the desktop wallpaper
        private static extern int SystemParametersInfo(int uAction, int uParam, string lpvParam, int fuWinIni);

        // Downloads a wallpaper image from a provider and returns its path
        private static string downloadWallpaper(IProvider provider, string jsonPath = null)
        {
            string imagePath = Path.Combine(Path.GetTempPath(), IMAGE_FILENAME);

            WallpaperInfo info = provider.GetWallpaperInfo();

            if (!string.IsNullOrWhiteSpace(jsonPath))
            {
                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    WriteIndented = true,
                };
                string jsonString = JsonSerializer.Serialize(info, options);
                File.WriteAllText(jsonPath, jsonString);
            }

            using (WebClient client = new WebClient())
            {
                client.DownloadFile(info.ImageUri, imagePath);
            }

            return imagePath;
        }

        // Applies blurred-fit to an image, overriding it
        private static void applyBlurredFit(string imagePath, int blurStrength)
        {
            Rectangle screenRect = Screen.PrimaryScreen.Bounds;
            float screenAspectRatio = (float)screenRect.Width / screenRect.Height;

            Bitmap copy = null;
            using (Bitmap image = new Bitmap(imagePath))
                copy = new Bitmap(image);

            float imageAspectRatio = (float)copy.Width / copy.Height;

            Rectangle backgroundRect = new Rectangle();
            Rectangle fillRect = new Rectangle();

            if (imageAspectRatio < screenAspectRatio)
            {
                backgroundRect.Width = (int)(copy.Height * screenAspectRatio);
                backgroundRect.Height = copy.Height;

                fillRect.Width = backgroundRect.Width;
                fillRect.Height = (int)(backgroundRect.Width / imageAspectRatio);

                backgroundRect.Y = (fillRect.Height - backgroundRect.Height) / 2;
            }
            else
            {
                backgroundRect.Width = copy.Width;
                backgroundRect.Height = (int)(copy.Width / screenAspectRatio);

                fillRect.Width = (int)(backgroundRect.Height * imageAspectRatio);
                fillRect.Height = backgroundRect.Height;

                backgroundRect.X = (fillRect.Width - backgroundRect.Width) / 2;
            }

            GaussianBlur gaussianBlur;
            using (Bitmap fill = new Bitmap(copy, fillRect.Width, fillRect.Height))
            {
                using (Bitmap background = fill.Clone(backgroundRect, PixelFormat.Format32bppArgb))
                    gaussianBlur = new GaussianBlur(background);
            }

            int largestDim = (imageAspectRatio > 1) ? copy.Width : copy.Height;
            int radius = (int)(MAX_BLUR_FRACTION * largestDim * blurStrength / 100);

            using (Bitmap blurred = gaussianBlur.Process(radius))
            {
                using (Graphics graphics = Graphics.FromImage(blurred))
                {
                    int x = (blurred.Width - copy.Width) / 2;
                    int y = (blurred.Height - copy.Height) / 2;
                    graphics.DrawImageUnscaled(copy, x, y);
                }

                blurred.Save(imagePath);
            }
        }

        // Creates a new JPEG from an image and returns its path
        private static string convertToJpeg(string imagePath, long quality = 100L)
        {
            Bitmap image = new Bitmap(imagePath);

            ImageCodecInfo jpgEncoder = ImageCodecInfo.GetImageEncoders().First(c => c.FormatID == ImageFormat.Jpeg.Guid);
            EncoderParameters parameters = new EncoderParameters(1);
            parameters.Param[0] = new EncoderParameter(Encoder.Compression, quality);

            string jpgPath = imagePath + ".jpg";
            image.Save(jpgPath, jpgEncoder, parameters);

            return jpgPath;
        }
    }
}
