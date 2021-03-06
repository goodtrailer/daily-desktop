// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using System;
using System.Net;
using System.Text.RegularExpressions;
using DailyDesktop.Core;
using DailyDesktop.Core.Providers;

namespace DailyDesktop.Providers.Pixiv
{
    public class PixivProvider : IProvider
    {
        private const string IMAGE_ID_PATTERN = "(?<=data-id=\")(.*?)(?=\")";
        private const string IMAGE_URI_PATTERN = "(?<=\"original\":\")(.*?)(?=\")";
        private const string AUTHOR_PATTERN = "(?<=((\"authorId\":\")(.*?)(\"userName\":\")))(.*?[^\\\\])(?=(\"))";
        private const string AUTHOR_ID_PATTERN = "(?<=(\"authorId\":\"))([0-9]*)";
        private const string TITLE_PATTERN = "(?<=(<meta property=\"twitter:title\" content=\"))([\\S\\s]*?[^\\\\])(?=(\">))";
        private const string DESCRIPTION_PATTERN = "(?<=(<meta property=\"twitter:description\" content=\"))([\\S\\s]*?[^\\\\])(?=(\">))";

        public string DisplayName => "pixiv";
        public string Description => "Fetches the illustration ranked #1 on the pixiv Overall Daily Rankings for the previous day.\r\n" +
            "Using blurred-fit mode is highly recommended due to the large variety of aspect ratios of illustrations found on pixiv.";
        public string SourceUri => "https://www.pixiv.net/ranking.php?content=illust";

        public void ConfigureWebClient(WebClient client)
        {
            client.Headers.Add("Referer", "https://www.pixiv.net");
        }

        public WallpaperInfo GetWallpaperInfo()
        {
            // Search for image ID of #1 illustration on daily rankings page

            string rankingHtml;
            using (WebClient client = this.CreateWebClient())
                rankingHtml = client.DownloadString(SourceUri);

            string imageId = Regex.Match(rankingHtml, IMAGE_ID_PATTERN).Value;
            if (string.IsNullOrWhiteSpace(imageId))
                throw new ProviderException("Didn't find an image ID.");

            string imagePageUri = "https://www.pixiv.net/en/artworks/" + imageId;

            // Search for wallpaper info on image page

            string imagePageHtml;
            using (WebClient client = this.CreateWebClient())
                imagePageHtml = client.DownloadString(imagePageUri);

            string imageUri = Regex.Match(imagePageHtml, IMAGE_URI_PATTERN).Value;
            if (string.IsNullOrWhiteSpace(imageUri))
                throw new ProviderException("Didn't find an image URI.");

            string title = Regex.Match(imagePageHtml, TITLE_PATTERN).Value;
            string author = Regex.Match(imagePageHtml, AUTHOR_PATTERN).Value;
            string authorUri = "https://www.pixiv.net/users/" + Regex.Match(imagePageHtml, AUTHOR_ID_PATTERN).Value;
            string description = WebUtility.HtmlDecode(Regex.Match(imagePageHtml, DESCRIPTION_PATTERN).Value);

            return new WallpaperInfo
            {
                ImageUri = imageUri,
                Date = DateTime.Now,
                Title = title,
                TitleUri = imagePageUri,
                Author = author,
                AuthorUri = authorUri,
                Description = description,
            };
        }
    }
}
