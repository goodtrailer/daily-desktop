﻿// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using System;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DailyDesktop.Core;
using DailyDesktop.Core.Providers;

namespace DailyDesktop.Providers.RedditEarthPorn
{
    public class RedditEarthPornProvider : IProvider
    {
        private const string IMAGE_URI_PATTERN = "(https://i\\.redd\\.it/)([^\\s/]*?)(\\.)([A-Za-z]*)";
        private const string AUTHOR_PATTERN = "(?<=(\"author\":\"))(.*?)(?=\")";
        private const string TITLE = "Photograph";
        private const string TITLE_URI_PATTERN = "(?<=(Posted by(.*?)href=\"))(.*?)(?=\")";
        private const string DESCRIPTION_PATTERN = "(?<=(<h3 class=.*?>))(.*?)(?=</h3>)";

        public string DisplayName => "r/EarthPorn";
        public string Description => "Looks at the top post in the last 24 hours in the well-known r/EarthPorn, reddit's premiere landscape photography subreddit.";
        public string SourceUri => "https://www.reddit.com/r/EarthPorn/top/?sort=top&t=day";

        public async Task<WallpaperInfo?> GetWallpaperInfo(HttpClient client)
        {
            string subredditHtml = await client.GetStringAsync(SourceUri);

            string imageUri = Regex.Match(subredditHtml, IMAGE_URI_PATTERN).Value;
            if (string.IsNullOrWhiteSpace(imageUri))
                throw new ProviderException("Didn't find an image URI.");

            string author = "u/" + Regex.Match(subredditHtml, AUTHOR_PATTERN).Value;
            string authorUri = "https://www.reddit.com/" + author;
            string titleUri = "https://www.reddit.com" + Regex.Match(subredditHtml, TITLE_URI_PATTERN).Value;
            string description = Regex.Match(subredditHtml, DESCRIPTION_PATTERN).Value;

            return new WallpaperInfo
            {
                ImageUri = imageUri,
                Date = DateTime.Now,
                Author = author,
                AuthorUri = authorUri,
                Title = TITLE,
                TitleUri = titleUri,
                Description = description,
            };
        }
    }
}
