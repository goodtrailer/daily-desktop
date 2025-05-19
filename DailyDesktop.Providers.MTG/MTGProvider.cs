// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using DailyDesktop.Core.Configuration;
using DailyDesktop.Core.Providers;

namespace DailyDesktop.Providers.MTG
{
    public class MTGProvider : IProvider
    {
        private const string IMAGE_URI_PATTERN = "(?<=//)([^\\s]*?)(1920x1080)(.*?)(?=\\\")";
        private const string RELEVANT_SOURCE_PATTERN = "(?<= )Wallpapers";
        private const string TITLE_PATTERN = "(?<=<h3.*?>).*?(?=</h3>)";
        private const string AUTHOR_PATTERN = "(?<=BY ).*?(?=<)";

        public string DisplayName => "Magic: The Gathering";
        public string Description => "Grabs new weekly Magic: The Gathering wallpaper from the official Wizards of the Coast website and sets it as the desktop wallpaper.";
        public string SourceUri => "https://magic.wizards.com/en/news#wallpapers";

        public async Task ConfigureWallpaperAsync(HttpClient client, IPublicWallpaperConfiguration wallpaperConfig, CancellationToken cancellationToken)
        {
            // Scrape info from wallpapers page

            string pageHtml = await client.GetStringAsync(SourceUri);

            string imageUri = "https://" + Regex.Match(pageHtml, IMAGE_URI_PATTERN).Value;
            if (string.IsNullOrWhiteSpace(imageUri))
                throw new ProviderException("Didn't find an image URI, HTML was:\"\"\"\n" + pageHtml + "\n\"\"\"");

            string relevantHtml = pageHtml.Substring(Regex.Match(pageHtml, RELEVANT_SOURCE_PATTERN).Index);
            string author = Regex.Match(relevantHtml, AUTHOR_PATTERN).Value.Trim();
            string title = Regex.Match(relevantHtml, TITLE_PATTERN).Value.Trim();

            // Get card text through Scryfall API

            var response = await client.GetAsync("https://api.scryfall.com/cards/named?format=text&fuzzy=" + title);
            string cardText = response.IsSuccessStatusCode ? await response.Content.ReadAsStringAsync() : response.ReasonPhrase;

            await wallpaperConfig.SetImageUriAsync(imageUri, cancellationToken);
            await wallpaperConfig.SetAuthorAsync(author, cancellationToken);
            await wallpaperConfig.SetTitleAsync(title, cancellationToken);
            await wallpaperConfig.SetTitleUriAsync(imageUri, cancellationToken);
            await wallpaperConfig.SetDescriptionAsync(cardText, cancellationToken);
        }
    }
}
