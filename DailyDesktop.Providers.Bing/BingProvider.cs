// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using System;
using System.Net;
using System.Text.RegularExpressions;
using DailyDesktop.Core;
using DailyDesktop.Core.Providers;

namespace DailyDesktop.Providers.Bing
{
    public class BingProvider : IProvider
    {
        private const string IMAGE_RELATIVE_URI_PATTERN = "(/th\\?id=)([^\"/>]*?)1920x1080.[a-z]*";
        private const string AUTHOR_PATTERN = "(?<=(<div class=\"copyright\" id=\"copyright\">))(.*?)(?=(</div>))";
        private const string TITLE_PATTERN = "(?<=(<meta property=\"og:title\" content=\"))(.*?)(?=(\" />))";
        private const string TITLE_URI_PATTERN = "(?<=(<a href=\"/))search(.*?)(?=(\"(.*?)class=\"learn_more\">))";
        private const string DESCRIPTION_PATTERN = "(?<=(<span(.*?)id=\"iotd_desc\">))(.*?)(?=(</span>))";

        public string DisplayName => "Bing";
        public string Description => "Grabs Bing's featured Image of the Day, which can be found on Bing's home page.";
        public string SourceUri => "https://www.bing.com/";

        public WallpaperInfo GetWallpaperInfo()
        {
            string pageHtml = null;
            using (WebClient client = new WebClient())
            {
                client.Headers.Add(HttpRequestHeader.UserAgent, "daily-desktop/0.0 (https://github.com/goodtrailer/daily-desktop)");
                pageHtml = client.DownloadString(SourceUri);
            }
            Match imageRelativeUriMatch = Regex.Match(pageHtml, IMAGE_RELATIVE_URI_PATTERN);
            string imageRelativeUri = imageRelativeUriMatch.Value;
            if (string.IsNullOrWhiteSpace(imageRelativeUri))
                throw new ProviderException("Didn't find a relative image URI.");
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
