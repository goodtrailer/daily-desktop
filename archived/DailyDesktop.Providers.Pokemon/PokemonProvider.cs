// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using DailyDesktop.Core.Configuration;
using DailyDesktop.Core.Providers;

namespace DailyDesktop.Providers.Pokemon
{
    public class PokemonProvider : IProvider
    {
        private const string IMAGE_RELATIVE_URI_PATTERN = "\\.\\./\\.\\.[^\\s]*?1920x1080[^s]*?jpg";
        private const string TITLE_PATTERN = "(?<=data-name=\").*?(?=\")";
        private const string COUNT_PATTERN = "(?<=\"count\":)[0-9]*?(?=,)";
        private const string ID_PATTERN = "(?<=\"id\":\").*?(?=\")";

        private const string FOUND_DESCRIPTION = "Found a card of the same name: ";
        private const string NOT_FOUND_DESCRIPTION = "Card doesn't seem to exist (yet).";

        public string DisplayName => "Pokémon TCG";

        public string Description => "Gets occasionally new Pokémon TCG wallpaper from the official Pokémon website.";

        public string SourceUri => "https://tcg.pokemon.com/en-us/wallpapers/";

        public async Task ConfigureWallpaperAsync(HttpClient client, IPublicWallpaperConfiguration wallpaperConfig, CancellationToken cancellationToken)
        {
            // Scrape info from wallpapers page

            string pageHtml = await client.GetStringAsync(SourceUri, cancellationToken);

            string imageUri = SourceUri + Regex.Match(pageHtml, IMAGE_RELATIVE_URI_PATTERN).Value;
            if (string.IsNullOrWhiteSpace(imageUri))
                throw new ProviderException("Didn't find any image URI.");

            string title = Regex.Match(pageHtml, TITLE_PATTERN).Value;
            string titleUri = imageUri;

            // Check if card exists through Pokemon TCG API

            var response = await client.GetAsync("https://api.pokemontcg.io/v2/cards?q=name:\"" + title + "\"", cancellationToken);

            string? description;
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync(cancellationToken);

                int count;
                int.TryParse(Regex.Match(content, COUNT_PATTERN).Value, out count);

                if (count > 0)
                {
                    string id = Regex.Match(content, ID_PATTERN).Value;
                    string similarCardUri = "https://pokemontcg.guru/card/x/" + id;
                    description = FOUND_DESCRIPTION + similarCardUri;
                }
                else
                {
                    description = NOT_FOUND_DESCRIPTION;
                }
            }
            else
            {
                description = response.ReasonPhrase;
            }

            await wallpaperConfig.SetImageUriAsync(imageUri, cancellationToken);
            await wallpaperConfig.SetTitleAsync(title, cancellationToken);
            await wallpaperConfig.SetTitleUriAsync(titleUri, cancellationToken);
            await wallpaperConfig.SetDescriptionAsync(description, cancellationToken);
        }
    }
}
