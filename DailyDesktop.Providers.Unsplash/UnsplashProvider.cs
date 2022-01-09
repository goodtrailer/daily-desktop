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
        private const string TRUE_SOURCE_URI = "https://unsplash.com/";
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
            string homeHtml = null;
            using (WebClient client = new WebClient())
            {
                client.Headers.Add(HttpRequestHeader.UserAgent, "daily-desktop/0.0 (https://github.com/goodtrailer/daily-desktop)");
                homeHtml = client.DownloadString(TRUE_SOURCE_URI);
            }

            Match imageUriMatch = Regex.Match(homeHtml, IMAGE_URI_PATTERN);
            string imageUri = imageUriMatch.Value;
            if (string.IsNullOrWhiteSpace(imageUri))
                throw new ProviderException("Didn't find an image URI.");

            Match titleMatch = Regex.Match(homeHtml, TITLE_PATTERN);
            string title = titleMatch.Value;

            Match titleRelativeUriMatch = Regex.Match(homeHtml, TITLE_RELATIVE_URI_PATTERN);
            string titleUri = TRUE_SOURCE_URI + titleRelativeUriMatch.Value;

            Match authorMatch = Regex.Match(homeHtml, AUTHOR_PATTERN);
            string author = authorMatch.Value;

            Match authorRelativeUriMatch = Regex.Match(homeHtml, AUTHOR_RELATIVE_URI_PATTERN);
            string authorUri = TRUE_SOURCE_URI + authorRelativeUriMatch.Value;

            string pageHtml = null;
            using (WebClient client = new WebClient())
            {
                client.Headers.Add(HttpRequestHeader.UserAgent, "daily-desktop/0.0 (https://github.com/goodtrailer/daily-desktop)");
                pageHtml = client.DownloadString(titleUri);
            }

            Match makeMatch = Regex.Match(pageHtml, MAKE_PATTERN);
            Match modelMatch = Regex.Match(pageHtml, MODEL_MATCH);
            Match focalLengthMatch = Regex.Match(pageHtml, FOCAL_LENGTH_PATTERN);
            Match apertureMatch = Regex.Match(pageHtml, APERTURE_PATTERN);
            Match shutterSpeedMatch = Regex.Match(pageHtml, SHUTTER_SPEED_PATTERN);
            Match isoMatch = Regex.Match(pageHtml, ISO_PATTERN);

            string description =
                $"Camera Make: {makeMatch.Value}\r\n" +
                $"Camera Model: {modelMatch.Value}\r\n" +
                $"Focal Length: {focalLengthMatch.Value}mm\r\n" +
                $"Aperture: ƒ/{apertureMatch.Value}\r\n" +
                $"Shutter Speed: {shutterSpeedMatch.Value}s\r\n" +
                $"ISO: {isoMatch.Value}";

            WallpaperInfo wallpaper = new WallpaperInfo
            {
                ImageUri = imageUri,
                Date = DateTime.Now,
                Author = author,
                AuthorUri = authorUri,
                Title = title,
                TitleUri = titleUri,
                Description = description,
            };

            return wallpaper;
        }
    }
}
