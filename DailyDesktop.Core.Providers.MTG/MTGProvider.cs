// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using System.Net;
using System.Text.RegularExpressions;

namespace DailyDesktop.Core.Providers.MTG
{
    public class MTGProvider : IProvider
    {
        private const string IMAGE_URI_PATTERN = "(?<=(<a(.*?)href=\"))(.*?)(?=(\">1920x1080</a>))";

        public string Key => "MTG";
        public string DisplayName => "Magic: The Gathering";
        public string Description => "Grabs new weekly Magic: The Gathering wallpaper from the official Wizards of the Coast website and sets it as the desktop wallpaper.";
        public string SourceUri => "https://magic.wizards.com/en/articles/media/wallpapers";

        public string GetImageUri()
        {
            string pageHtml = string.Empty;
            using (WebClient client = new WebClient())
            {
                pageHtml = client.DownloadString(SourceUri);
            }

            Match imageUriMatch = Regex.Match(pageHtml, IMAGE_URI_PATTERN);
            string imageUri = imageUriMatch.Value;
            if (string.IsNullOrWhiteSpace(imageUri))
                throw new ProviderException("Didn't find an image URI.");

            return imageUri;
        }
    }
}
