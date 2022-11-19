// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using System;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DailyDesktop.Core;
using DailyDesktop.Core.Providers;

namespace DailyDesktop.Providers.MTG
{
    public class MTGProvider : IProvider
    {
        private const string IMAGE_URI_PATTERN = "(?<=//)[^\\s]*?1920x1080_wallpaper\\.jpg";
        private const string RELEVANT_SOURCE_PATTERN = "(?<= )Wallpapers";
        private const string TITLE_PATTERN = "(?<=<h3.*?>).*?(?=</h3>)";
        private const string AUTHOR_PATTERN = "(?<=BY ).*?(?=<)";

        public string DisplayName => "Magic: The Gathering";
        public string Description => "Grabs new weekly Magic: The Gathering wallpaper from the official Wizards of the Coast website and sets it as the desktop wallpaper.";
        public string SourceUri => "https://magic.wizards.com/en/news#wallpapers";

        public async Task<WallpaperInfo> GetWallpaperInfo(HttpClient client)
        {
            // Scrape info from wallpapers page

            string pageHtml = await client.GetStringAsync(SourceUri);

            string imageUri = "https://" + Regex.Match(pageHtml, IMAGE_URI_PATTERN).Value;
            if (string.IsNullOrWhiteSpace(imageUri))
                throw new ProviderException("Didn't find an image URI.");

            string relevantHtml = pageHtml.Substring(Regex.Match(pageHtml, RELEVANT_SOURCE_PATTERN).Index);
            string author = Regex.Match(relevantHtml, AUTHOR_PATTERN).Value.Trim();
            string title = Regex.Match(relevantHtml, TITLE_PATTERN).Value.Trim();

            // Get card text through Scryfall API

            var response = await client.GetAsync("https://api.scryfall.com/cards/named?format=text&fuzzy=" + title);
            string cardText = response.IsSuccessStatusCode ? await response.Content.ReadAsStringAsync() : response.ReasonPhrase;

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
