﻿// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using DailyDesktop.Core.Configuration;
using DailyDesktop.Core.Providers;

namespace DailyDesktop.Providers.FalseKnees
{
    public class FalseKneesProvider : IProvider
    {
        private const string AUTHOR = "Joshua Barkman";
        private const string AUTHOR_URI = "https://falseknees.com/about.html";
        private const string IMAGE_RELATIVE_URI_PATTERN = "imgs/[0-9]*?\\.[a-zA-Z]+";
        private const string TITLE_RELATIVE_URI_PATTERN = "(?<=URL=).*?(?=\")";
        private const string DESCRIPTION_PATTERN = "(?<=<img.*title=\").*?(?=\")";
        private const string TITLE_PATTERN = "(?<=<p class=\"div-overflow\">.*?- ).*?(?=</p>)";

        public string DisplayName => "False Knees";

        public string Description => "Gets the most recent False Knees comic! False Knees is a webcomic written by Joshua Barkman. He says, \"All silly nonsense is my own.\"\r\n" +
            "Using blurred-fit mode is highly recommended due to the often extreme aspect ratios of False Knees comics.";

        public string SourceUri => "https://falseknees.com";

        public async Task ConfigureWallpaperAsync(HttpClient client, IPublicWallpaperConfiguration wallpaperConfig, CancellationToken cancellationToken)
        {
            // Scrape info from front page

            string pageHtml = await client.GetStringAsync(SourceUri, cancellationToken);

            string titleUri = SourceUri + "/" + Regex.Match(pageHtml, TITLE_RELATIVE_URI_PATTERN).Value;

            // Scrape info from image (title) page

            string titleHtml = await client.GetStringAsync(titleUri, cancellationToken);
            string imageUri = SourceUri + "/comics/" + Regex.Match(titleHtml, IMAGE_RELATIVE_URI_PATTERN).Value;
            string description = Regex.Match(titleHtml, DESCRIPTION_PATTERN).Value;

            // Scrape title from archive page

            string archiveHtml = await client.GetStringAsync("https://falseknees.com/archive.html", cancellationToken);

            string title = Regex.Match(archiveHtml, TITLE_PATTERN).Value;

            await wallpaperConfig.SetImageUriAsync(imageUri, cancellationToken);
            await wallpaperConfig.SetAuthorAsync(AUTHOR, cancellationToken);
            await wallpaperConfig.SetAuthorUriAsync(AUTHOR_URI, cancellationToken);
            await wallpaperConfig.SetTitleAsync(title, cancellationToken);
            await wallpaperConfig.SetTitleUriAsync(titleUri, cancellationToken);
            await wallpaperConfig.SetDescriptionAsync(description, cancellationToken);
        }
    }
}
