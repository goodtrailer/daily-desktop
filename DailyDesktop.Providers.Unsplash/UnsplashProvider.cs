// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using System;
using System.Net;
using System.Text.RegularExpressions;
using DailyDesktop.Core;
using DailyDesktop.Core.Providers;

namespace DailyDesktop.Providers.Unsplash
{
    public class UnsplashProvider : IProvider
    {
        private const string IMAGE_URI_PATTERN = "(?<=<source srcSet=\")(.*?)(?=\\?)";
        private const string TITLE_PATTERN = "(?<=(itemProp=\"contentUrl\"(.*?)title=\"))(.*?)(?=(\"))";
        private const string TITLE_RELATIVE_URI_PATTERN = "(?<=(itemProp=\"contentUrl\"(.*?)href=\"/))(.*?)(?=(\"))";
        private const string AUTHOR_PATTERN = "(?<=(Photo of the Day(.*?)>))([^<]*?)(?=(</a>))";
        private const string AUTHOR_RELATIVE_URI_PATTERN = "(?<=(Photo of the Day(.*?)href=\"/))([^<]*?)(?=(\">))";

        private const string MAKE_PATTERN = "(?<=(\\\\\"make\\\\\":\\\\\"))(.*?)(?=(\\\\\"))";
        private const string MODEL_MATCH = "(?<=(\\\\\"model\\\\\":\\\\\"))(.*?)(?=(\\\\\"))";
        private const string FOCAL_LENGTH_PATTERN = "(?<=(\\\\\"focal_length\\\\\":\\\\\"))(.*?)(?=(\\\\\"))";
        private const string APERTURE_PATTERN = "(?<=(\\\\\"aperture\\\\\":\\\\\"))(.*?)(?=(\\\\\"))";
        private const string SHUTTER_SPEED_PATTERN = "(?<=(\\\\\"exposure_time\\\\\":\\\\\"))(.*?)(?=(\\\\\"))";
        private const string ISO_PATTERN = "(?<=(\\\\\"iso\\\\\":))[0-9]*";

        public string DisplayName => "Unsplash";
        public string Description => "Nabs the Photo of the Day that is currently being displayed on the front page of the website Unsplash, an online source for high-quality and freely-usable images.";
        public string SourceUri => "https://unsplash.com/collections/1459961/photo-of-the-day-(archive)";

        public WallpaperInfo GetWallpaperInfo()
        {
            // Scrape info from home page

            string homeHtml;
            using (WebClient client = this.CreateWebClient())
                homeHtml = client.DownloadString("https://unsplash.com/");

            string imageUri = Regex.Match(homeHtml, IMAGE_URI_PATTERN).Value;
            if (string.IsNullOrWhiteSpace(imageUri))
                throw new ProviderException("Didn't find an image URI.");

            string title = Regex.Match(homeHtml, TITLE_PATTERN).Value;
            string titleUri = "https://unsplash.com/" + Regex.Match(homeHtml, TITLE_RELATIVE_URI_PATTERN).Value;
            string author = Regex.Match(homeHtml, AUTHOR_PATTERN).Value;
            string authorUri = "https://unsplash.com/" + Regex.Match(homeHtml, AUTHOR_RELATIVE_URI_PATTERN).Value;

            // Scrape camera specs from image page

            string pageHtml;
            using (WebClient client = this.CreateWebClient())
                pageHtml = client.DownloadString(titleUri);

            string description =
                $"Camera Make: {Regex.Match(pageHtml, MAKE_PATTERN).Value}\r\n" +
                $"Camera Model: {Regex.Match(pageHtml, MODEL_MATCH).Value}\r\n" +
                $"Focal Length: {Regex.Match(pageHtml, FOCAL_LENGTH_PATTERN).Value}mm\r\n" +
                $"Aperture: ƒ/{Regex.Match(pageHtml, APERTURE_PATTERN).Value}\r\n" +
                $"Shutter Speed: {Regex.Match(pageHtml, SHUTTER_SPEED_PATTERN).Value}s\r\n" +
                $"ISO: {Regex.Match(pageHtml, ISO_PATTERN).Value}";

            return new WallpaperInfo
            {
                ImageUri = imageUri,
                Date = DateTime.Now,
                Author = author,
                AuthorUri = authorUri,
                Title = title,
                TitleUri = titleUri,
                Description = description,
            };
        }
    }
}
