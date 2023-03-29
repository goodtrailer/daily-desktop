﻿// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DailyDesktop.Core.Configuration;
using DailyDesktop.Core.Providers;

namespace DailyDesktop.Providers.Unsplash
{
    public class UnsplashProvider : IProvider
    {
        private const string TITLE = "Photograph";

        private const string IMAGE_URI_PATTERN = "(?<=<source srcSet=\")(.*?)(?=\\?)";
        private const string TITLE_RELATIVE_URI_PATTERN = "(?<=(contentUrl.*?href=\"/)).*?(?=\")";
        private const string AUTHOR_RELATIVE_URI_PATTERN = "(?<=(contentUrl.*?href.*?href=\"/)).*?(?=\")";

        private const string MAKE_PATTERN = "(?<=(\\\\\"make\\\\\":\\\\\"))(.*?)(?=(\\\\\"))";
        private const string MODEL_MATCH = "(?<=(\\\\\"model\\\\\":\\\\\"))(.*?)(?=(\\\\\"))";
        private const string FOCAL_LENGTH_PATTERN = "(?<=(\\\\\"focal_length\\\\\":\\\\\"))(.*?)(?=(\\\\\"))";
        private const string APERTURE_PATTERN = "(?<=(\\\\\"aperture\\\\\":\\\\\"))(.*?)(?=(\\\\\"))";
        private const string SHUTTER_SPEED_PATTERN = "(?<=(\\\\\"exposure_time\\\\\":\\\\\"))(.*?)(?=(\\\\\"))";
        private const string ISO_PATTERN = "(?<=(\\\\\"iso\\\\\":))[0-9]*";

        public string DisplayName => "Unsplash";
        public string Description => "Nabs the Photo of the Day that is currently being displayed on the front page of the website Unsplash, an online source for high-quality and freely-usable images.";
        public string SourceUri => "https://unsplash.com/collections/1459961/photo-of-the-day-(archive)";

        public async Task ConfigureWallpaper(HttpClient client, IPublicWallpaperConfiguration wallpaperConfig)
        {
            wallpaperConfig.Title = TITLE;

            // Scrape info from home page

            string homeHtml = await client.GetStringAsync("https://unsplash.com/");

            wallpaperConfig.ImageUri = Regex.Match(homeHtml, IMAGE_URI_PATTERN).Value;
            if (string.IsNullOrWhiteSpace(wallpaperConfig.ImageUri))
                throw new ProviderException("Didn't find an image URI.");

            wallpaperConfig.TitleUri = "https://unsplash.com/" + Regex.Match(homeHtml, TITLE_RELATIVE_URI_PATTERN).Value;

            string authorRelativeUri = Regex.Match(homeHtml, AUTHOR_RELATIVE_URI_PATTERN).Value;
            string authorPattern = $"(?<={authorRelativeUri}\">).*?(?=<)";

            wallpaperConfig.Author = Regex.Match(homeHtml, authorPattern).Value;
            wallpaperConfig.AuthorUri = "https://unsplash.com/" + authorRelativeUri;

            // Scrape camera specs from image page

            string pageHtml = await client.GetStringAsync(wallpaperConfig.TitleUri);

            string make = Regex.Match(pageHtml, MAKE_PATTERN).Value;
            string model = Regex.Match(pageHtml, MODEL_MATCH).Value;
            string focalLength = Regex.Match(pageHtml, FOCAL_LENGTH_PATTERN).Value;
            string aperture = Regex.Match(pageHtml, APERTURE_PATTERN).Value;
            string shutterSpeed = Regex.Match(pageHtml, SHUTTER_SPEED_PATTERN).Value;
            string iso = Regex.Match(pageHtml, ISO_PATTERN).Value;

            wallpaperConfig.Description = "No camera specs. The image is probably a 3D render.";
            if (!string.IsNullOrWhiteSpace(make)
                || !string.IsNullOrWhiteSpace(model)
                || !string.IsNullOrWhiteSpace(focalLength)
                || !string.IsNullOrWhiteSpace(aperture)
                || !string.IsNullOrWhiteSpace(shutterSpeed)
                || !string.IsNullOrWhiteSpace(iso))
            {
                wallpaperConfig.Description =
                    $"Camera Make: {make}\r\n" +
                    $"Camera Model: {model}\r\n" +
                    $"Focal Length: {focalLength}mm\r\n" +
                    $"Aperture: ƒ/{aperture}\r\n" +
                    $"Shutter Speed: {shutterSpeed}s\r\n" +
                    $"ISO: {iso}";
            }
        }
    }
}
