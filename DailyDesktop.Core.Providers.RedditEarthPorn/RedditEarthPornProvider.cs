// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using System;
using System.Net;
using System.Text.RegularExpressions;

namespace DailyDesktop.Core.Providers.RedditEarthPorn
{
    public class RedditEarthPornProvider : IProvider
    {
        private const string IMAGE_URI_PATTERN = "(https://i\\.redd\\.it/)([^\\s/]*?)(\\.)([A-Za-z]*)";
        private const string AUTHOR_PATTERN = "(?<=(\"author\":\"))(.*?)(?=\")";
        private const string TITLE = "Photograph";
        private const string TITLE_URI_PATTERN = "(?<=(Posted by(.*?)href=\"))(.*?)(?=\")";
        private const string DESCRIPTION_PATTERN = "(?<=(},\"title\":\"))(.*?)(?=\",\"author\")";

        public string DisplayName => "r/EarthPorn";
        public string Description => "Looks at the top post in the last 24 hours in the well-known r/EarthPorn, reddit's premiere landscape photography subreddit.";
        public string SourceUri => "https://www.reddit.com/r/EarthPorn/top/?sort=top&t=day";

        public WallpaperInfo GetWallpaperInfo()
        {
            string subredditHtml = null;
            using (WebClient client = new WebClient())
            {
                client.Headers.Add(HttpRequestHeader.UserAgent, "daily-desktop/0.0 (https://github.com/goodtrailer/daily-desktop)");
                subredditHtml = client.DownloadString(SourceUri);
            }

            Match imageUriMatch = Regex.Match(subredditHtml, IMAGE_URI_PATTERN);
            string imageUri = imageUriMatch.Value;
            if (string.IsNullOrWhiteSpace(imageUri))
                throw new ProviderException("Didn't find an image URI.");

            Match authorMatch = Regex.Match(subredditHtml, AUTHOR_PATTERN);
            string author = $"u/{authorMatch.Value}";
            string authorUri = $"https://www.reddit.com/" + author;

            Match titleUriMatch = Regex.Match(subredditHtml, TITLE_URI_PATTERN);
            string titleUri = titleUriMatch.Value;

            Match descriptionMatch = Regex.Match(subredditHtml, DESCRIPTION_PATTERN);
            string description = descriptionMatch.Value;

            WallpaperInfo wallpaper = new WallpaperInfo
            {
                ImageUri = imageUri,
                Date = DateTime.Now,
                Author = author,
                AuthorUri = authorUri,
                Title = TITLE,
                TitleUri = titleUri,
                Description = description,
            };

            return wallpaper;
        }
    }
}
