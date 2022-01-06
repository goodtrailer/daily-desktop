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
        private const string AUTHOR_PATTERN = "(?<=((\"authorId\":\")(.*?)(\"userName\":\")))(.*?[^\\\\])(?=(\"))";
        private const string AUTHOR_ID_PATTERN = "(?<=(\"authorId\":\"))([0-9]*)";
        private const string TITLE_PATTERN = "(?<=(<meta property=\"twitter:title\" content=\"))([\\S\\s]*?[^\\\\])(?=(\">))";
        private const string DESCRIPTION_PATTERN = "(?<=(<meta property=\"twitter:description\" content=\"))([\\S\\s]*?[^\\\\])(?=(\">))";
        private const string IMAGE_DOWNLOAD_NAME = "Daily Desktop pixiv";

        public string DisplayName => "pixiv";
        public string Description => "Fetches the illustration ranked #1 on the pixiv Overall Daily Rankings for the previous day.\r\n" +
            "Using blurred-fit mode is highly recommended due to the large variety of aspect ratios of illustrations found on pixiv.";
        public string SourceUri => "https://www.pixiv.net/ranking.php?content=illust";

        public WallpaperInfo GetWallpaperInfo()
        {
            // Search for image ID of #1 illustration on daily rankings page

            string rankingHtml = null;
            using (WebClient client = new WebClient())
            {
                client.Headers.Add(HttpRequestHeader.UserAgent, "daily-desktop/0.0 (https://github.com/goodtrailer/daily-desktop)");
                client.Headers.Add("Referer", "https://www.pixiv.net");
                rankingHtml = client.DownloadString(SourceUri);
            }
            Match imageIdMatch = Regex.Match(rankingHtml, IMAGE_ID_PATTERN);
            string imageId = imageIdMatch.Value;
            if (string.IsNullOrWhiteSpace(imageId))
                throw new ProviderException("Didn't find an image ID.");

            // Search for wallpaper info on image page

            string imagePageUri = $"https://www.pixiv.net/en/artworks/{imageId}";
            string imagePageHtml = null;
            using (WebClient client = new WebClient())
            {
                client.Headers.Add(HttpRequestHeader.UserAgent, "daily-desktop/0.0 (https://github.com/goodtrailer/daily-desktop)");
                client.Headers.Add("Referer", "https://www.pixiv.net");
                imagePageHtml = client.DownloadString(imagePageUri);
            }
            Match imageUriMatch = Regex.Match(imagePageHtml, IMAGE_URI_PATTERN);
            string imageUri = imageUriMatch.Value;
            if (string.IsNullOrWhiteSpace(imageUri))
                throw new ProviderException("Didn't find an image URI.");

            Match titleMatch = Regex.Match(imagePageHtml, TITLE_PATTERN);
            string title = titleMatch.Value;

            Match authorMatch = Regex.Match(imagePageHtml, AUTHOR_PATTERN);
            string author = authorMatch.Value;

            Match authorIdMatch = Regex.Match(imagePageHtml, AUTHOR_ID_PATTERN);
            string authorUri = $"https://www.pixiv.net/users/{authorIdMatch.Value}";

            Match descriptionMatch = Regex.Match(imagePageHtml, DESCRIPTION_PATTERN);
            string description = WebUtility.HtmlDecode(descriptionMatch.Value);

            // Download illustration from image URI and return its local path,
            // which is necessary because pixiv blocks requests if the Referer
            // header attribute isn't set to pixiv.net

            string imageLocalUri = Path.Combine(Path.GetTempPath(), IMAGE_DOWNLOAD_NAME);
            using (WebClient client = new WebClient())
            {
                client.Headers.Add(HttpRequestHeader.UserAgent, "daily-desktop/0.0 (https://github.com/goodtrailer/daily-desktop)");
                client.Headers.Add(HttpRequestHeader.Referer, "https://www.pixiv.net");
                client.DownloadFile(imageUri, imageLocalUri);
            }

            WallpaperInfo wallpaper = new WallpaperInfo
            {
                ImageUri = imageLocalUri,
                Date = DateTime.Now,
                Title = title,
                TitleUri = imagePageUri,
                Author = author,
                AuthorUri = authorUri,
                Description = description,
            };

            return wallpaper;
        }
    }
}
