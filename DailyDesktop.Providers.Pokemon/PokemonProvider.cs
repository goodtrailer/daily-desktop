// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using System;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DailyDesktop.Core;
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

        public string Description => "Weekly Pokémon TCG wallpaper from the official Pokémon website.";

        public string SourceUri => "https://tcg.pokemon.com/en-us/wallpapers/";


        public async Task<WallpaperInfo> GetWallpaperInfo(HttpClient client)
        {
            // Scrape info from wallpapers page

            string pageHtml = await client.GetStringAsync(SourceUri);

            string imageUri = SourceUri + Regex.Match(pageHtml, IMAGE_RELATIVE_URI_PATTERN).Value;
            if (string.IsNullOrWhiteSpace(imageUri))
                throw new ProviderException("Didn't find any image URI.");

            string title = Regex.Match(pageHtml, TITLE_PATTERN).Value;

            // Check if card exists through Pokemon TCG API

            var response = await client.GetAsync("https://api.pokemontcg.io/v2/cards?q=name:\"" + title + "\"");

            string description;
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();

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

            return new WallpaperInfo
            {
                ImageUri = imageUri,
                Date = DateTime.Now,
                Title = title,
                TitleUri = imageUri,
                Author = null,
                AuthorUri = null,
                Description = description,
            };
        }
    }
}
