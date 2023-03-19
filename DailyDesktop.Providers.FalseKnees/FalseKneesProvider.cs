// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using System;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DailyDesktop.Core;
using DailyDesktop.Core.Providers;

namespace DailyDesktop.Providers.FalseKnees
{
    public class FalseKneesProvider : IProvider
    {
        private const string AUTHOR = "Joshua Barkman";
        private const string AUTHOR_URI = "https://falseknees.com/about.html";
        private const string IMAGE_RELATIVE_URI_PATTERN = "imgs/[0-9]*?\\.png";
        private const string TITLE_RELATIVE_URI_PATTERN = "(?<=URL=)[0-9]+\\.[a-zA-Z]+";
        private const string DESCRIPTION_PATTERN = "(?<=<img.*title=\").*?(?=\")";
        private const string TITLE_PATTERN = "(?<=index\\.html.*- ).*?(?=<)";

        public string DisplayName => "False Knees";
        public string Description => "Gets the most recent False Knees comic! False Knees is a webcomic written by Joshua Barkman. He says, \"All silly nonsense is my own.\"\r\n" +
            "Using blurred-fit mode is highly recommended due to the often extreme aspect ratios of False Knees comics.";
        
        public string SourceUri => "https://falseknees.com/index.html";

        public async Task<WallpaperInfo> GetWallpaperInfo(HttpClient client)
        {
            // Scrape info from front page

            string pageHtml = await client.GetStringAsync(SourceUri);

            string imageUri = SourceUri + "/" + Regex.Match(pageHtml, IMAGE_RELATIVE_URI_PATTERN).Value;
            string titleUri = SourceUri + "/" + Regex.Match(pageHtml, TITLE_RELATIVE_URI_PATTERN).Value;
            string description = Regex.Match(pageHtml, DESCRIPTION_PATTERN).Value;

            // Scrape title from archive page

            string archiveHtml = await client.GetStringAsync("https://falseknees.com/archive.html");

            string title = Regex.Match(archiveHtml, TITLE_PATTERN).Value;

            return new WallpaperInfo
            {
                ImageUri = imageUri,
                Date = DateTime.Now,
                Author = AUTHOR,
                AuthorUri = AUTHOR_URI,
                Title = title,
                TitleUri = titleUri,
                Description = description,
            };
        }
    }
}
