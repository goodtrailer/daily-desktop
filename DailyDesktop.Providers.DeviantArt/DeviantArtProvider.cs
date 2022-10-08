// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using System;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DailyDesktop.Core;
using DailyDesktop.Core.Providers;

namespace DailyDesktop.Providers.DeviantArt
{
    public class DeviantArtProvider : IProvider
    {
        private const string IMAGE_PAGE_URI_PATTERN = "(?<=(<a(.*?)data-hook=\"deviation_link\" href=\"))(.*?)(?=(\"))";
        private const string IMAGE_URI_PATTERN = "(?<=(<img alt=(.*?)src=\"))(.*?)(?=(\"))";
        private const string CREDIT_PATTERN = "((?<=(<title data-rh=\"[truefalse]*\">))(.*?)(?=( on DeviantArt</title>)))";
        private const string AUTHOR_PATTERN = "(?<=(by ))(.(?!(by)))*$";
        private const string TITLE_PATTERN = "(.*)(?=( by))";
        private const string DESCRIPTION_PATTERN = "(?<=(</div><div class=\"legacy-journal[^\"]*\">))(.*?)(?=(</div>))";

        public string DisplayName => "DeviantArt";
        public string Description => "Fetches one of DeviantArt's currently " +
            "featured piece from its Daily Deviations, a collection of art " +
            "handpicked by the DeviantArt community and staff. These artworks " +
            "highlight the best of DeviantArt from a wide variety of genres.";
        public string SourceUri => "https://www.deviantart.com/daily-deviations";

        public async Task<WallpaperInfo> GetWallpaperInfo(HttpClient client)
        {
            // Search for image page URI from daily deviation page

            string dailyDeviationHtml = await client.GetStringAsync(SourceUri);

            string imagePageUri = Regex.Match(dailyDeviationHtml, IMAGE_PAGE_URI_PATTERN).Value;
            if (string.IsNullOrWhiteSpace(imagePageUri))
                throw new ProviderException("Didn't find an image page URI.");

            // Scrape info from image page

            string imagePageHtml = await client.GetStringAsync(imagePageUri);

            string imageUri = Regex.Match(imagePageHtml, IMAGE_URI_PATTERN).Value;
            if (string.IsNullOrWhiteSpace(imageUri))
                throw new ProviderException("Didn't find an image URI.");

            string credit = Regex.Match(imagePageHtml, CREDIT_PATTERN).Value ?? null;
            string author = Regex.Match(credit, AUTHOR_PATTERN).Value;
            string authorUri = "https://www.deviantart.com/" + WebUtility.UrlEncode(author);
            string title = Regex.Match(credit, TITLE_PATTERN).Value;
            string description = Regex.Replace(WebUtility.HtmlDecode(Regex.Match(imagePageHtml, DESCRIPTION_PATTERN).Value), "<([^<>]*?)>", "");
            if (string.IsNullOrWhiteSpace(description))
                description = null;

            return new WallpaperInfo
            {
                ImageUri = imageUri,
                Date = DateTime.Now,
                Author = author,
                AuthorUri = authorUri,
                Title = title,
                TitleUri = imagePageUri,
                Description = description,
            };
        }
    }
}
