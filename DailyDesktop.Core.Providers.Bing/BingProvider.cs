// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using System;
using System.Net;
using System.Text.RegularExpressions;

namespace DailyDesktop.Core.Providers.Bing
{
    public class BingProvider : IProvider
    {
        private const string RESOLUTION_PATTERN = "(?<=(_))([0-9x]*)(?=(\\.))";
        private const string RESOLUTION_REPLACEMENT = "1920x1080";
        private const string IMAGE_URI_PATTERN = "(?<=(<a href=\"))(/th\\?id=)(.*?)(?=(&))";
        private const string AUTHOR_PATTERN = "(?<=(<div class=\"copyright\" id=\"copyright\">))(.*?)(?=(</div>))";
        private const string TITLE_PATTERN = "(?<=(<div class=\"vs_bs_title\">))(.*?)(?=(</div>))";
        private const string TITLE_URI_PATTERN = "(?<=(<a href=\"/))search(.*?)(?=(\"(.*?)class=\"learn_more\">))";
        private const string DESCRIPTION_PATTERN = "(?<=(<span(.*?)id=\"iotd_desc\">))(.*?)(?=(</span>))";

        public string DisplayName => "Bing";
        public string Description => "Grabs Bing's featured Image of the Day, which can be found on Bing's home page.";
        public string SourceUri => "https://www.bing.com/";

        public WallpaperInfo GetWallpaperInfo()
        {
            string pageHtml = string.Empty;
            using (WebClient client = new WebClient())
            {
                pageHtml = client.DownloadString(SourceUri);
            }
            Match imageRelativeUriMatch = Regex.Match(pageHtml, IMAGE_URI_PATTERN);
            string imageRelativeUri = imageRelativeUriMatch.Value;
            if (string.IsNullOrWhiteSpace(imageRelativeUri))
                throw new ProviderException("Didn't find a relative image URI.");
            imageRelativeUri = Regex.Replace(imageRelativeUri, RESOLUTION_PATTERN, RESOLUTION_REPLACEMENT);
            string imageUri = SourceUri + imageRelativeUri;

            Match authorMatch = Regex.Match(pageHtml, AUTHOR_PATTERN);
            string author = authorMatch.Value;

            Match titleMatch = Regex.Match(pageHtml, TITLE_PATTERN);
            string title = titleMatch.Value;

            Match titleRelativeUriMatch = Regex.Match(pageHtml, TITLE_URI_PATTERN);
            string titleUri = SourceUri + WebUtility.HtmlDecode(titleRelativeUriMatch.Value).Replace("\"", "%22");

            Match descriptionMatch = Regex.Match(pageHtml, DESCRIPTION_PATTERN);
            string description = $"TODAY ON BING\r\n{descriptionMatch.Value}";

            WallpaperInfo wallpaper = new WallpaperInfo
            {
               ImageUri = imageUri,
               Date = DateTime.Now,
               Author = author,
               AuthorUri = null,
               Title = title,
               TitleUri = titleUri,
               Description = description,
            };

            return wallpaper;
        }
    }
}
