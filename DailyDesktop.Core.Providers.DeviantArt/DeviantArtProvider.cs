// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using System.Net;
using System.Text.RegularExpressions;

namespace DailyDesktop.Core.Providers.DeviantArt
{
    public class DeviantArtProvider : IProvider
    {
        private const string IMAGE_PAGE_URI_PATTERN = "(?<=(<a(.*?)data-hook=\"deviation_link\" href=\"))(.*?)(?=(\"))";
        private const string IMAGE_URI_PATTERN = "(?<=(<img alt=(.*?)src=\"))(.*?)(?=(\"/>))";

        public string Key => "DEVIANT";
        public string DisplayName => "DeviantArt";
        public string Description => "Fetches DeviantArt's currently featured " +
            "piece from its Daily Deviations, a collection of art handpicked " +
            "by the DeviantArt community and staff. These artworks highlight " +
            "the best of DeviantArt from a wide variety of genres.";
        public string SourceUri => "https://www.deviantart.com/daily-deviations";

        public string GetImageUri()
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

            return imageUri;
        }
    }
}
