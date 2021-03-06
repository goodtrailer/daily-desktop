// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using System;
using System.Net;
using System.Text.RegularExpressions;
using DailyDesktop.Core;
using DailyDesktop.Core.Providers;

namespace DailyDesktop.Providers.CalvinAndHobbes
{
    public class CalvinAndHobbesProvider : IProvider
    {
        private const string IMAGE_URI_PATTERN = "https://assets.amuniversal.com/.*?(?=[ \"])";
        private const string AUTHOR = "Bill Watterson";
        private const string AUTHOR_URI = null;
        private const string TITLE = "Comic strip";
        private const string TITLE_RELATIVE_URI_PATTERN = "(?<=/calvinandhobbes)[/0-9]+?(?=\")";
        private const string DESCRIPTION = null;

        public string DisplayName => "Calvin and Hobbes";
        public string Description => "Fetches today's Calvin and Hobbes comic, a daily American comic strip created by cartoonist Bill Watterson from 1985 to 1995.";
        public string SourceUri => "https://www.gocomics.com/calvinandhobbes";

        public WallpaperInfo GetWallpaperInfo()
        {
            string pageHtml;
            using (WebClient client = this.CreateWebClient())
                pageHtml = client.DownloadString(SourceUri);

            string imageUri = Regex.Match(pageHtml, IMAGE_URI_PATTERN).Value;
            string titleUri = SourceUri + Regex.Match(pageHtml, TITLE_RELATIVE_URI_PATTERN).Value;

            return new WallpaperInfo
            {
                ImageUri = imageUri,
                Date = DateTime.Now,
                Author = AUTHOR,
                AuthorUri = AUTHOR_URI,
                Title = TITLE,
                TitleUri = titleUri,
                Description = DESCRIPTION,
            };
        }
    }
}
