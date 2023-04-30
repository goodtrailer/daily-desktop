// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using DailyDesktop.Core.Configuration;
using DailyDesktop.Core.Providers;

namespace DailyDesktop.Providers.Unsplash
{
    public class UnsplashProvider : IProvider
    {
        private const string IMAGE_URI_PATTERN = "(?<=<source srcSet=\")(.*?)(?=\\?)";
        private const string TITLE_PATTERN = "(?<=(\"description\":\"))(.*?)(?=\")";
        private const string TITLE_RELATIVE_URI_PATTERN = "(?<=(contentUrl.*?href=\"/)).*?(?=\")";
        private const string AUTHOR_RELATIVE_URI_PATTERN = "(?<=(contentUrl.*?href.*?href=\"/)).*?(?=\")";

        private const string MAKE_PATTERN = "(?<=(\\\\\"make\\\\\":\\\\\"))(.*?)(?=(\\\\\"))";
        private const string MODEL_MATCH = "(?<=(\\\\\"model\\\\\":\\\\\"))(.*?)(?=(\\\\\"))";
        private const string FOCAL_LENGTH_PATTERN = "(?<=(\\\\\"focal_length\\\\\":\\\\\"))(.*?)(?=(\\\\\"))";
        private const string APERTURE_PATTERN = "(?<=(\\\\\"aperture\\\\\":\\\\\"))(.*?)(?=(\\\\\"))";
        private const string SHUTTER_SPEED_PATTERN = "(?<=(\\\\\"exposure_time\\\\\":\\\\\"))(.*?)(?=(\\\\\"))";
        private const string ISO_PATTERN = "(?<=(\\\\\"iso\\\\\":))[0-9]*";
        private const string TAG_PATTERN_0 = "(?<=(Related tags))(.*?)(?=(collection-feed-card))";
        private const string TAG_PATTERN_1 = "(?<=(<a(.*?)\">))(.*?)(?=(</a>))";

        public string DisplayName => "Unsplash";
        public string Description => "Nabs the Photo of the Day that is currently being displayed on the front page of the website Unsplash, an online source for high-quality and freely-usable images.";
        public string SourceUri => "https://unsplash.com/collections/1459961/photo-of-the-day-(archive)";

        public async Task ConfigureWallpaperAsync(HttpClient client, IPublicWallpaperConfiguration wallpaperConfig, CancellationToken cancellationToken)
        {
            // Scrape info from home page

            string homeHtml = await client.GetStringAsync("https://unsplash.com/", cancellationToken);

            string imageUri = Regex.Match(homeHtml, IMAGE_URI_PATTERN).Value;
            if (string.IsNullOrWhiteSpace(imageUri))
                throw new ProviderException("Didn't find an image URI.");

            string titleUri = "https://unsplash.com/" + Regex.Match(homeHtml, TITLE_RELATIVE_URI_PATTERN).Value;

            string authorRelativeUri = Regex.Match(homeHtml, AUTHOR_RELATIVE_URI_PATTERN).Value;
            string authorPattern = $"(?<={authorRelativeUri}\">).*?(?=<)";

            string author = Regex.Match(homeHtml, authorPattern).Value;
            string authorUri = "https://unsplash.com/" + authorRelativeUri;

            // Scrape camera specs from image page

            string pageHtml = await client.GetStringAsync(titleUri, cancellationToken);

            string title = Regex.Match(pageHtml, TITLE_PATTERN).Value;

            string make = Regex.Match(pageHtml, MAKE_PATTERN).Value;
            string model = Regex.Match(pageHtml, MODEL_MATCH).Value;
            string focalLength = Regex.Match(pageHtml, FOCAL_LENGTH_PATTERN).Value;
            string aperture = Regex.Match(pageHtml, APERTURE_PATTERN).Value;
            string shutterSpeed = Regex.Match(pageHtml, SHUTTER_SPEED_PATTERN).Value;
            string iso = Regex.Match(pageHtml, ISO_PATTERN).Value;

            var descriptionBuilder = new StringBuilder();
            if (!string.IsNullOrWhiteSpace(make)
                || !string.IsNullOrWhiteSpace(model)
                || !string.IsNullOrWhiteSpace(focalLength)
                || !string.IsNullOrWhiteSpace(aperture)
                || !string.IsNullOrWhiteSpace(shutterSpeed)
                || !string.IsNullOrWhiteSpace(iso))
            {
                descriptionBuilder.AppendLine($"Camera Make: {make}");
                descriptionBuilder.AppendLine($"Camera Model: {model}");
                descriptionBuilder.AppendLine($"Focal Length: {focalLength}mm");
                descriptionBuilder.AppendLine($"Aperture: ƒ/{aperture}");
                descriptionBuilder.AppendLine($"Shutter Speed: {shutterSpeed}s");
                descriptionBuilder.AppendLine($"ISO: {iso}");
            }
            else
            {
                descriptionBuilder.AppendLine("No camera specs. The image is probably a 3D render.");
            }

            var tagsHtml = WebUtility.HtmlDecode(Regex.Match(pageHtml, TAG_PATTERN_0).Value);
            var tagMatches = Regex.Matches(tagsHtml, TAG_PATTERN_1).ToList();
            if (tagMatches.Count > 0)
            {
                descriptionBuilder.AppendLine();

                foreach (var tagMatch in tagMatches)
                {
                    if (!tagMatch.Success)
                        continue;

                    string tag = tagMatch.Value;
                    if (tag.Contains("hd ", System.StringComparison.CurrentCultureIgnoreCase))
                        continue;

                    descriptionBuilder.Append($"#{tag} ");
                }
            }

            await wallpaperConfig.SetImageUriAsync(imageUri, cancellationToken);
            await wallpaperConfig.SetAuthorAsync(author, cancellationToken);
            await wallpaperConfig.SetAuthorUriAsync(authorUri, cancellationToken);
            await wallpaperConfig.SetTitleAsync(title, cancellationToken);
            await wallpaperConfig.SetTitleUriAsync(titleUri, cancellationToken);
            await wallpaperConfig.SetDescriptionAsync(descriptionBuilder.ToString(), cancellationToken);
        }
    }
}
