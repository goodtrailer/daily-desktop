// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using System;
using System.CommandLine;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using DailyDesktop.Core.Configuration;
using DailyDesktop.Core.Providers;
using DailyDesktop.Core.Util;
using ImageMagick;
using Silk.NET.GLFW;

namespace DailyDesktop.Task
{
    using Task = System.Threading.Tasks.Task;

    internal static partial class Program
    {
        private const string IMAGE_FILENAME = "Daily Desktop Wallpaper";
        private const double MAX_BLUR_FRACTION = 0.025;

        [LibraryImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static partial bool SetProcessDPIAware();

        [LibraryImport("user32.dll", StringMarshalling = StringMarshalling.Utf16)]
        private static partial int SystemParametersInfo(int uAction, int uParam, string lpvParam, int fuWinIni);

        private static Size? screenSizeCache = null;
        private static Size screenSize
        {
            get
            {
                if (screenSizeCache is Size cache)
                    return cache;

                Size value = new Size();
                var glfw = Glfw.GetApi() ?? throw new InvalidOperationException("Could not get GLFW API");
                if (!glfw.Init())
                    throw new InvalidOperationException("GLFW did not initialize correctly");
                try
                {
                    unsafe
                    {
                        var monitor = glfw.GetPrimaryMonitor();
                        if (monitor == null)
                            throw new InvalidOperationException("GLFW could not find primary monitor");

                        var mode = glfw.GetVideoMode(monitor);
                        if (mode == null)
                            throw new InvalidOperationException("GLFW could not find video mode for primary monitor");

                        value.Width = mode->Width;
                        value.Height = mode->Height;
                    }

                    if (value.Width <= 0)
                        throw new InvalidOperationException("Invalid screen width retrieved: " + value.Width);
                    if (value.Height <= 0)
                        throw new InvalidOperationException("Invalid screen height retrieved: " + value.Height);
                }
                finally
                {
                    glfw.Terminate();
                }

                screenSizeCache = value;
                return value;
            }
        }

        private static async Task<int> Main(string[] args)
        {
            var dllPathArg = new Argument<string>("dllPath")
            {
                Description = "Path to DLL module containing the IProvider implementation",
            };

            var jsonPathOption = new Option<string>("--json")
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
                jsonPathOption,
                resizeOption,
                blurOption
            };

            rootCommand.SetAction(async parseResult =>
            {
                try
                {
                    await handleArguments(
                        parseResult.GetRequiredValue(dllPathArg),
                        parseResult.GetRequiredValue(jsonPathOption),
                        parseResult.GetRequiredValue(resizeOption),
                        parseResult.GetRequiredValue(blurOption)
                    );
                    return 0;
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(e);
                    return 1;
                }
            });

            return await rootCommand.Parse(args).InvokeAsync();
        }

        private static async Task handleArguments(string dllPath, string jsonPath, bool resize, int? blur)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(dllPath))
                    throw new ArgumentException("Missing IProvider DLL module path");

                var store = new ProviderStore();
                var providerType = await store.AddAsync(dllPath, AsyncUtils.TimedCancel());
                var provider = IProvider.Instantiate(providerType);

                string imagePath = await downloadWallpaper(provider, jsonPath, AsyncUtils.LongCancel());
                using var image = new MagickImage(imagePath);

                if (resize)
                    applyResize(image);

                if (blur != null)
                    applyBlurredFit(image, blur.Value);

                await setWallpaper(image, imagePath);
            }
            catch (Exception e)
            {
                var wallpaperConfig = new WallpaperConfiguration(jsonPath);
                await wallpaperConfig.SetTitleAsync("Exception encountered", AsyncUtils.LongCancel());
                await wallpaperConfig.SetAuthorAsync("provider", AsyncUtils.LongCancel());
                await wallpaperConfig.SetDescriptionAsync(e.Message + "\n\n" + e.StackTrace, AsyncUtils.LongCancel());
                await wallpaperConfig.TrySerializeAsync(AsyncUtils.LongCancel());
                throw;
            }
        }

        private static async Task setWallpaper(MagickImage image, string imagePath)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                string outputPath = imagePath + ".tif";
                image.Format = MagickFormat.Tif;
                image.Write(outputPath);

                // https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-systemparametersinfoa#parameters
                // SPI_SETDESKWALLPAPER, 0, path, SPIF_UPDATEINIFILE | SPIF_SENDCHANGE
                int exitCode = SystemParametersInfo(0x14, 0, outputPath, 0x1 | 0x2);
                if (exitCode != 0)
                    throw new InvalidOperationException("SystemParamtersInfo(...) returned code: " + exitCode);
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                // TODO: Add a selector for desktop environment, and pass this
                // as an argument. Only show selector in DailyDesktop.Desktop
                // if on Linux. Currently this only works for KDE 6

                string outputPath = imagePath + ".png";
                image.Format = MagickFormat.Png;
                image.Write(outputPath);

                using var process = new Process();
                process.StartInfo.FileName = "/usr/bin/plasma-apply-wallpaperimage";
                process.StartInfo.ArgumentList.Add(outputPath);
                process.StartInfo.CreateNoWindow = true;

                process.Start();
                await process.WaitForExitAsync();
                if (process.ExitCode != 0)
                    throw new InvalidOperationException("plasma-apply-wallpaperimage exited with code: " + process.ExitCode);
            }
            else
            {
                throw new PlatformNotSupportedException("Platform is not Windows/Linux");
            }
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
