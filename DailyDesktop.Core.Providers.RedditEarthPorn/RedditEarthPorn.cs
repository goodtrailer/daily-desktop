// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using System.Net;
using System.Text.RegularExpressions;

namespace DailyDesktop.Core.Providers.RedditEarthPorn
{
    public class RedditEarthPorn : IProvider
    {
        private const string IMAGE_URI_PATTERN = "(https://i\\.redd\\.it/)([^\\s/]*?)(\\.)([A-Za-z]*)";

        public string Key => "R/EARTHPORN";
        public string DisplayName => "r/EarthPorn";
        public string Description => "Looks at the top post in the last 24 hours in the well-known r/EarthPorn, reddit's premiere landscape photography subreddit.";
        public string SourceUri => "https://www.reddit.com/r/EarthPorn/top/?sort=top&t=day";

        public string GetImageUri()
        {
            string pageSource = string.Empty;
            using (WebClient client = new WebClient())
            {
                pageSource = client.DownloadString(SourceUri);
            }

            Match imageUriMatch = Regex.Match(pageSource, IMAGE_URI_PATTERN);
            string imageUri = imageUriMatch.Value;
            if (string.IsNullOrWhiteSpace(imageUri))
                throw new ProviderException("Didn't find an image URI.");

            return imageUri;
        }
    }
}
