// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using System;
using System.Net;
using System.Text.RegularExpressions;

namespace DailyDesktop.Core.Providers.MTG
{
    public class MTGProvider : IProvider
    {
        private const string IMAGE_URI_PATTERN = "(?<=(<a(.*?)href=\"))(.*?)(?=(\">1920x1080</a>))";
        private const string TITLE_PATTERN = "(?<=(<h3>))(.*?)(?=(</h3))";
        private const string AUTHOR_PATTERN = "(?<=(<p class=\"author\">By))(.*?)(?=(</p>))";

        public string DisplayName => "Magic: The Gathering";
        public string Description => "Grabs new weekly Magic: The Gathering wallpaper from the official Wizards of the Coast website and sets it as the desktop wallpaper.";
        public string SourceUri => "https://magic.wizards.com/en/articles/media/wallpapers";

        public WallpaperInfo GetWallpaperInfo()
        {
            string pageHtml = string.Empty;
            using (WebClient client = new WebClient())
            {
                pageHtml = client.DownloadString(SourceUri);
            }

            Match imageUriMatch = Regex.Match(pageHtml, IMAGE_URI_PATTERN);
            string imageUri = imageUriMatch.Value;
            if (string.IsNullOrWhiteSpace(imageUri))
                throw new ProviderException("Didn't find an image URI.");

            Match authorMatch = Regex.Match(pageHtml, AUTHOR_PATTERN);
            string author = authorMatch.Value.Trim();

            Match titleMatch = Regex.Match(pageHtml, TITLE_PATTERN);
            string title = titleMatch.Value.Trim();

            string cardText = string.Empty;
            using (WebClient client = new WebClient())
            {
                string formattedName = title.Replace(" ", "%20");
                string request = $"https://api.scryfall.com/cards/named?format=text&fuzzy={formattedName}";
                cardText = client.DownloadString(request);
            }

            WallpaperInfo wallpaper = new WallpaperInfo
            {
                ImageUri = imageUri,
                Date = DateTime.Now,
                Title = title,
                TitleUri = imageUri,
                Author = author,
                AuthorUri = null,
                Description = cardText,
            };

            return wallpaper;
        }
    }
}
