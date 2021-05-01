// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using System;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;

namespace DailyDesktop.Core.Providers.Pixiv
{
    public class PixivProvider : IProvider
    {
        private const string IMAGE_ID_PATTERN = "(?<=data-id=\")(.*?)(?=\")";
        private const string IMAGE_URI_PATTERN = "(?<=\"original\":\")(.*?)(?=\")";
        private const string IMAGE_DOWNLOAD_NAME = "Daily Desktop pixiv";

        public string Key => "PIXIV";
        public string DisplayName => "pixiv";
        public string Description => "Fetches the illustration ranked #1 on the pixiv Overall Daily Rankings for the previous day.\r\n" +
            "Using blurred-fit mode is highly recommended due to the large variety of aspect ratios of illustrations found on pixiv.";
        public string SourceUri => "https://www.pixiv.net/ranking.php";

        public string GetImageUri()
        {
            // Search for image ID of #1 illustration on daily rankings page

            string rankingHtml = string.Empty;
            using (WebClient client = new WebClient())
            {
                client.Headers.Add("Referer", "https://www.pixiv.net");
                rankingHtml = client.DownloadString(SourceUri);
            }
            Match imageIdMatch = Regex.Match(rankingHtml, IMAGE_ID_PATTERN);
            string imageId = imageIdMatch.Value;
            if (string.IsNullOrWhiteSpace(imageId))
                throw new ProviderException("Didn't find an image ID.");

            // Search for image URI of illustration on image's actual page

            string imageHtml = string.Empty;
            using (WebClient client = new WebClient())
            {
                client.Headers.Add("Referer", "https://www.pixiv.net");
                imageHtml = client.DownloadString($"https://www.pixiv.net/en/artworks/{imageId}");
            }
            Match imageUriMatch = Regex.Match(imageHtml, IMAGE_URI_PATTERN);
            string imageUri = imageUriMatch.Value;
            if (string.IsNullOrWhiteSpace(imageUri))
                throw new ProviderException("Didn't find an image URI.");

            // Download illustration from image URI and return its local path

            string imageLocalUri = Path.Combine(Path.GetTempPath(), IMAGE_DOWNLOAD_NAME);
            using (WebClient client = new WebClient())
            {
                client.Headers.Add("Referer", "https://www.pixiv.net");
                client.DownloadFile(imageUri, imageLocalUri);
            }
            return imageLocalUri;
        }
    }
}
