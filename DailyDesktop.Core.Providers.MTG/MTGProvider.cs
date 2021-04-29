﻿// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using System.Net;

namespace DailyDesktop.Core.Providers.MTG
{
    public class MTGProvider : IProvider
    {
        private const string TAG_TEXT = ">1920x1080</a>";
        private const string IMAGE_SUFFIX = ".jpg";
        private const string IMAGE_PREFIX = "https://media.magic.wizards.com/images/wallpaper/";

        public string Key => "MTG";
        public string DisplayName => "Magic: The Gathering";
        public string Description => "Grabs new weekly Magic: The Gathering wallpaper from the official Wizards of the Coast website and sets it as the desktop wallpaper.";
        public string SourceUri => "https://magic.wizards.com/en/articles/media/wallpapers";

        public string GetImageUri()
        {
            string htmlSource = string.Empty;
            using (WebClient client = new WebClient())
            {
                htmlSource = client.DownloadString(SourceUri);
            }

            int textIndex = htmlSource.IndexOf(TAG_TEXT, System.StringComparison.CurrentCultureIgnoreCase);
            int endIndex = htmlSource.LastIndexOf(IMAGE_SUFFIX, textIndex, System.StringComparison.CurrentCultureIgnoreCase) + IMAGE_SUFFIX.Length;
            int startIndex = htmlSource.LastIndexOf(IMAGE_PREFIX, endIndex, System.StringComparison.CurrentCultureIgnoreCase);
            string imageUri = htmlSource.Substring(startIndex, endIndex - startIndex);

            return imageUri;
        }
    }
}
