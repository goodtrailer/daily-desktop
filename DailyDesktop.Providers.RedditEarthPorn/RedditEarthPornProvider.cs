// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DailyDesktop.Core.Configuration;
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

        public async Task ConfigureWallpaper(HttpClient client, IPublicWallpaperConfiguration wallpaperConfig)
        {
            wallpaperConfig.Title = TITLE;

            string subredditHtml = await client.GetStringAsync(SourceUri);

            wallpaperConfig.ImageUri = Regex.Match(subredditHtml, IMAGE_URI_PATTERN).Value;
            if (string.IsNullOrWhiteSpace(wallpaperConfig.ImageUri))
                throw new ProviderException("Didn't find an image URI.");

            wallpaperConfig.Author = "u/" + Regex.Match(subredditHtml, AUTHOR_PATTERN).Value;
            wallpaperConfig.AuthorUri = "https://www.reddit.com/" + wallpaperConfig.Author;
            wallpaperConfig.TitleUri = "https://www.reddit.com" + Regex.Match(subredditHtml, TITLE_URI_PATTERN).Value;
            wallpaperConfig.Description = Regex.Match(subredditHtml, DESCRIPTION_PATTERN).Value;
        }
    }
}
