// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using DailyDesktop.Core.Providers;
using SuperfastBlur;

namespace DailyDesktop.Task
{
    internal class Program
    {
        private const string IMAGE_FILENAME = "Daily Desktop Wallpaper";
        private const double MAX_BLUR_FRACTION = 0.025;

        private static void Main(string[] args)
        {
            if (args.Length < 1)
                throw new ProviderException("No IProvider.Key passed.");

            ProviderStore store = new ProviderStore();
            IProvider provider = store.Providers[args[0]];

            string imagePath = downloadWallpaper(provider);

            int blurStrength = 0;
            bool doBlurredFit = false;
            if (args.Length >= 2)
                doBlurredFit = int.TryParse(args[1], out blurStrength);

            if (doBlurredFit)
                applyBlurredFit(imagePath, blurStrength);

            // https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-systemparametersinfoa#parameters
            // SPI_SETDESKWALLPAPER, 0, path, SPIF_UPDATEINIFILE | SPIF_SENDCHANGE
            SystemParametersInfo(0x14, 0, imagePath, 0x1 | 0x2);
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern int SystemParametersInfo(int uAction, int uParam, string lpvParam, int fuWinIni);

        private static string downloadWallpaper(IProvider provider)
        {
            string imagePath = Path.Combine(Path.GetTempPath(), IMAGE_FILENAME);

            string uri = provider.GetImageUri();
            using (WebClient client = new WebClient())
            {
                client.DownloadFile(uri, imagePath);
            }

            return imagePath;
        }

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
    }
}
