// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using System;
using System.Net;
using System.Text.RegularExpressions;

namespace DailyDesktop.Core.Providers.FalseKnees
{
    public class FalseKneesProvider : IProvider
    {
        private const string AUTHOR = "Joshua Barkman";
        private const string AUTHOR_URI = "https://falseknees.com/about.html";

        private const string IMAGE_URI_PATTERN = "http://www\\.falseknees\\.com/imgs/[0-9]*?\\.png";
        private const string TITLE_RELATIVE_URI_PATTERN = "(?<=URL=)[0-9]+\\.[a-zA-Z]+";
        private const string DESCRIPTION_PATTERN = "(?<=<img.*title=\").*?(?=\")";

        private const string ARCHIVE_URI = "https://falseknees.com/archive.html";
        private const string TITLE_PATTERN = "(?<=index\\.html.*- ).*?(?=<)";

        public string DisplayName => "False Knees";
        public string Description => "Gets the most recent False Knees comic! False Knees is a webcomic written by Joshua Barkman. He says, \"All silly nonsense is my own.\"";
        public string SourceUri => "https://falseknees.com";

        public WallpaperInfo GetWallpaperInfo()
        {
            string imageUri = null;
            string title = null;
            string titleUri = null;
            string description = null;
            using (WebClient client = new WebClient())
            {
                client.Headers.Add(HttpRequestHeader.UserAgent, "daily-desktop/0.0 (https://github.com/goodtrailer/daily-desktop)");
                string pageHtml = null;

                pageHtml = client.DownloadString(SourceUri);
                imageUri = Regex.Match(pageHtml, IMAGE_URI_PATTERN).Value;
                titleUri = SourceUri + "/" + Regex.Match(pageHtml, TITLE_RELATIVE_URI_PATTERN).Value;
                description = Regex.Match(pageHtml, DESCRIPTION_PATTERN).Value;

                pageHtml = client.DownloadString(ARCHIVE_URI);
                title = Regex.Match(pageHtml, TITLE_PATTERN).Value;
            }

            WallpaperInfo wallpaper = new WallpaperInfo
            {
                ImageUri = imageUri,
                Date = DateTime.Now,
                Author = AUTHOR,
                AuthorUri = AUTHOR_URI,
                Title = title,
                TitleUri = titleUri,
                Description = description,
            };

            return wallpaper;
        }
    }
}
