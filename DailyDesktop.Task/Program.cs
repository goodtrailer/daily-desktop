// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using System;
using System.CommandLine;
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

        private static async Task<int> Main(string[] args)
        {
            var dllPathArg = new Argument<string>("dllPath")
            {
                Description = "Path to DLL module containing the IProvider implementation",
            };

            var jsonOption = new Option<string>("--json")
            {
                DefaultValueFactory = _ => "",
                Description = "Where to output the wallpaper info JSON file",
            };

            var resizeOption = new Option<bool>("--resize")
            {
                DefaultValueFactory = _ => false,
                Description = "Resize to screen resolution if larger",
            };

            var blurOption = new Option<int?>("--blur")
            {
                DefaultValueFactory = _ => null,
                Description = "Use blurred-fit mode with the passed value for background blur strength",
            };

            var rootCommand = new RootCommand("Daily Desktop task target executable")
            {
                dllPathArg,
                jsonOption,
                resizeOption,
                blurOption
            };

            ParseResult parseResult = rootCommand.Parse(args);
            return await tryHandleArguments(
                parseResult.GetRequiredValue(dllPathArg),
                parseResult.GetRequiredValue(jsonOption),
                parseResult.GetRequiredValue(resizeOption),
                parseResult.GetRequiredValue(blurOption)
            );
        }

        private static async Task<int> tryHandleArguments(string dllPath, string json, bool resize, int? blur)
        {
            try
            {
                return await handleArguments(dllPath, json, resize, blur);
            }
            catch (Exception e)
            {
                var wallpaperConfig = new WallpaperConfiguration(json);
                await wallpaperConfig.SetTitleAsync("Exception encountered", AsyncUtils.LongCancel());
                await wallpaperConfig.SetAuthorAsync("provider", AsyncUtils.LongCancel());
                await wallpaperConfig.SetDescriptionAsync(e.Message + "\n\n" + e.StackTrace, AsyncUtils.LongCancel());
                await wallpaperConfig.TrySerializeAsync(AsyncUtils.LongCancel());
                return 1;
            }
        }

        private static async Task<int> handleArguments(string dllPath, string jsonPath, bool resize, int? blur)
        {
            if (string.IsNullOrWhiteSpace(dllPath))
                throw new ProviderException("Missing IProvider DLL module path");

            var store = new ProviderStore();
            var providerType = await store.AddAsync(dllPath, AsyncUtils.TimedCancel());
            var provider = IProvider.Instantiate(providerType);

            string imagePath = await downloadWallpaper(provider, jsonPath, AsyncUtils.LongCancel());
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
                image.Resize((uint)targetSize.Width, (uint)targetSize.Height);
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
                    Width = (uint)(image.Height * screenAspectRatio),
                    Height = image.Height,
                };
                fillSize = new Size
                {
                    Width = (int)backgroundRect.Width,
                    Height = (int)(backgroundRect.Width / imageAspectRatio),
                };
                backgroundRect.X = 0;
                backgroundRect.Y = (fillSize.Height - (int)backgroundRect.Height) / 2;
            }
            else
            {
                backgroundRect = new MagickGeometry(0, 0, 0, 0)
                {
                    Width = image.Width,
                    Height = (uint)(image.Width / screenAspectRatio),
                };
                fillSize = new Size
                {
                    Width = (int)(backgroundRect.Height * imageAspectRatio),
                    Height = (int)backgroundRect.Height,
                };
                backgroundRect.X = (fillSize.Width - (int)backgroundRect.Width) / 2;
                backgroundRect.Y = 0;
            }

            if (fillSize.Width == image.Width && fillSize.Height == image.Height)
                return;

            var fill = new MagickImage(image);
            fill.Resize((uint)fillSize.Width, (uint)fillSize.Height);
            fill.Crop(backgroundRect);

            uint largestDim = (imageAspectRatio > 1) ? image.Width : image.Height;
            int sigma = (int)(MAX_BLUR_FRACTION * largestDim * blurStrength / 100);
            fill.Blur(0, sigma);

            int x = ((int)fill.Width - (int)image.Width) / 2;
            int y = ((int)fill.Height - (int)image.Height) / 2;
            fill.Composite(image, x, y);
            image.Extent(fill.Width, fill.Height);
            image.CopyPixels(fill);
        }
    }
}
