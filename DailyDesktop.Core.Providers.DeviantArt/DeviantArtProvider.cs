// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using System;
using System.Net;
using System.Text.RegularExpressions;

namespace DailyDesktop.Core.Providers.DeviantArt
{
    public class DeviantArtProvider : IProvider
    {
        private const string IMAGE_PAGE_URI_PATTERN = "(?<=(<a(.*?)data-hook=\"deviation_link\" href=\"))(.*?)(?=(\"))";
        private const string IMAGE_URI_PATTERN = "(?<=(<img alt=(.*?)src=\"))(.*?)(?=(\"/>))";
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

        public WallpaperInfo GetWallpaperInfo()
        {
            string dailyDeviationHtml = string.Empty;
            using (WebClient client = new WebClient())
            {
                dailyDeviationHtml = client.DownloadString(SourceUri);
            }

            Match imagePageUriMatch = Regex.Match(dailyDeviationHtml, IMAGE_PAGE_URI_PATTERN);
            string imagePageUri = imagePageUriMatch.Value;
            if (string.IsNullOrWhiteSpace(imagePageUri))
                throw new ProviderException("Didn't find an image page URI.");

            string imagePageHtml = string.Empty;
            using (WebClient client = new WebClient())
            {
                imagePageHtml = client.DownloadString(imagePageUri);
            }

            Match imageUriMatch = Regex.Match(imagePageHtml, IMAGE_URI_PATTERN);
            string imageUri = imageUriMatch.Value;
            if (string.IsNullOrWhiteSpace(imageUri))
                throw new ProviderException("Didn't find an image URI.");

            Match creditMatch = Regex.Match(imagePageHtml, CREDIT_PATTERN);
            string credit = creditMatch.Value ?? string.Empty;

            Match authorMatch = Regex.Match(credit, AUTHOR_PATTERN);
            string author = authorMatch.Value;

            string authorUri = "https://www.deviantart.com/" + WebUtility.UrlEncode(author);

            Match titleMatch = Regex.Match(credit, TITLE_PATTERN);
            string title = titleMatch.Value;

            Match descriptionMatch = Regex.Match(imagePageHtml, DESCRIPTION_PATTERN);
            string description = WebUtility.HtmlDecode(descriptionMatch.Value);
            description = Regex.Replace(description, "<([^<>]*?)>", "");
            if (string.IsNullOrWhiteSpace(descriptionMatch.Value))
                description = null;

            WallpaperInfo wallpaper = new WallpaperInfo
            {
                ImageUri = imageUri,
                Date = DateTime.Now,
                Author = author,
                AuthorUri = authorUri,
                Title = title,
                TitleUri = imagePageUri,
                Description = description,
            };

            return wallpaper;
        }
    }
}
