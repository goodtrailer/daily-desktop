using DailyDesktop.Core.Providers;
using System;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;

namespace DailyDesktop.Task
{
    public class Program
    {
        private const string IMAGE_FILENAME = "Daily Desktop Wallpaper";

        public static void Main(string[] args)
        {
            if (args.Length < 1)
                throw new ProviderException("No IProvider.Key passed.");

            ProviderStore store = new ProviderStore();
            IProvider provider = store.Providers[args[0]];

            updateWallpaper(provider);
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern int SystemParametersInfo(int uAction, int uParam, string lpvParam, int fuWinIni);

        private static void updateWallpaper(IProvider provider)
        {
            string path = Path.Combine(Path.GetTempPath(), IMAGE_FILENAME);

            string uri = provider.GetImageUri();
            using (WebClient client = new WebClient())
            {
                client.DownloadFile(uri, path);
            }

            // https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-systemparametersinfoa#parameters
            // SPI_SETDESKWALLPAPER, 0, path, SPIF_UPDATEINIFILE | SPIF_SENDCHANGE
            SystemParametersInfo(0x14, 0, path, 0x1 | 0x2);
        }
    }
}
