// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DailyDesktop.Core;
using DailyDesktop.Core.Providers;

namespace DailyDesktop.Providers.MTG
{
    public class MTGProvider : IProvider
    {
        private const string IMAGE_URI_PATTERN = "(?<=(<a(.*?)href=\"))(.*?)(?=(\">1920x1080</a>))";
        private const string TITLE_PATTERN = "(?<=(<h3>))(.*?)(?=(</h3))";
        private const string AUTHOR_PATTERN = "(?<=(<p class=\"author\">By))(.*?)(?=(</p>))";

        public string DisplayName => "Magic: The Gathering";
        public string Description => "Grabs new weekly Magic: The Gathering wallpaper from the official Wizards of the Coast website and sets it as the desktop wallpaper.";
        public string SourceUri => "https://magic.wizards.com/en/articles/media/wallpapers";

        public async Task<WallpaperInfo> GetWallpaperInfo()
        {
            // Scrape info from wallpapers page

            string pageHtml;
            using (var client = this.CreateHttpClient())
                pageHtml = await client.GetStringAsync(SourceUri);

            string imageUri = Regex.Match(pageHtml, IMAGE_URI_PATTERN).Value;
            if (string.IsNullOrWhiteSpace(imageUri))
                throw new ProviderException("Didn't find an image URI.");

            string author = Regex.Match(pageHtml, AUTHOR_PATTERN).Value.Trim();
            string title = Regex.Match(pageHtml, TITLE_PATTERN).Value.Trim();

            // Get card text through Scryfall API

            string cardText;
            using (var client = this.CreateHttpClient())
            {
                var response = await client.GetAsync("https://api.scryfall.com/cards/named?format=text&fuzzy=" + title);
                cardText = response.IsSuccessStatusCode ? await response.Content.ReadAsStringAsync() : response.ReasonPhrase;
            }

            return new WallpaperInfo
            {
                ImageUri = imageUri,
                Date = DateTime.Now,
                Title = title,
                TitleUri = imageUri,
                Author = author,
                AuthorUri = null,
                Description = cardText,
            };
        }
    }
}
