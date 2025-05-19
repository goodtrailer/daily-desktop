// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using DailyDesktop.Core.Configuration;
using DailyDesktop.Core.Providers;

namespace DailyDesktop.Providers.Pixiv
{
    public class PixivProvider : IProvider
    {
        private const string IMAGE_ID_PATTERN = "(?<=(data-title=\"[^\"]+?\"(.*?)data-id=\"))(.*?)(?=\")";
        private const string IMAGE_PATH_PATTERN = "(?<=/img-master/)(.*?)_p0";
        private const string AUTHOR_PATTERN = "(?<=(data-title=\"[^\"]+?\"(.*?)data-user-name=\"))(.*?)(?=\")";
        private const string AUTHOR_ID_PATTERN = "(?<=data-user-id=\")(.*?)(?=\")";
        private const string TITLE_PATTERN = "(?<=data-title=\")[^\"]+?(?=\")";
        private const string DESCRIPTION_PATTERN = "(?<=(summary_large_image(.*?)\"description\":\"))(.*?)(?=\")";
        private const string TAG_PATTERN = "(?<=data-tags=\")(.*?)(?=\")";

        public string DisplayName => "pixiv";
        public string Description => "Fetches the illustration ranked #1 on the pixiv Overall Daily Rankings for the previous day.\r\n" +
            "Using blurred-fit mode is highly recommended due to the large variety of aspect ratios of illustrations found on pixiv.";
        public string SourceUri => "https://www.pixiv.net/ranking.php?mode=daily&content=illust";

        public void ConfigureHttpRequestHeaders(HttpRequestHeaders headers)
        {
            headers.Referrer = new Uri("https://www.pixiv.net");
        }

        public async Task ConfigureWallpaperAsync(HttpClient client, IPublicWallpaperConfiguration wallpaperConfig, CancellationToken cancellationToken)
        {
            // Search for image ID of #1 illustration on daily rankings page

            string rankingHtml = await client.GetStringAsync(SourceUri, cancellationToken);

            string imagePath = Regex.Match(rankingHtml, IMAGE_PATH_PATTERN).Value;
            if (string.IsNullOrWhiteSpace(imagePath))
                throw new ProviderException("Didn't find an image path, HTML was:\"\"\"\n" + rankingHtml + "\n\"\"\"");

            string[] imageUris = new[]
            {
                "https://i.pximg.net/img-original/" + imagePath + ".png",
                "https://i.pximg.net/img-original/" + imagePath + ".jpg",
            };

            string? imageUri = null;
            foreach (string uri in imageUris)
            {
                var request = new HttpRequestMessage(HttpMethod.Head, uri);
                var response = await client.SendAsync(request, cancellationToken);
                if (response.IsSuccessStatusCode)
                {
                    imageUri = uri;
                    break;
                }
            }
            if (imageUri == null)
                throw new ProviderException("None of the tried URIs worked (PNG/JPG), HTML was:\"\"\"\n" + rankingHtml + "\n\"\"\"");

            string titleUri = "https://www.pixiv.net/artworks/" + Regex.Match(rankingHtml, IMAGE_ID_PATTERN).Value;
            string authorUri = "https://www.pixiv.net/users/" + Regex.Match(rankingHtml, AUTHOR_ID_PATTERN).Value;
            string title = Regex.Match(rankingHtml, TITLE_PATTERN).Value;
            string author = Regex.Match(rankingHtml, AUTHOR_PATTERN).Value;

            // Search for wallpaper info on image page

            string imagePageHtml = await client.GetStringAsync(titleUri, cancellationToken);

            string description = WebUtility.HtmlDecode(Regex.Matches(imagePageHtml, DESCRIPTION_PATTERN).LastOrDefault()?.Value) ?? "";
            description = description.Replace("\\r", "\r").Replace("\\n", "\n");
            string tags = Regex.Match(rankingHtml, TAG_PATTERN).Value;
            if (!string.IsNullOrWhiteSpace(tags))
                description += "\n\n" + string.Join(" ", tags.Split(" ").Select(s => "#" + s));

            await wallpaperConfig.SetImageUriAsync(imageUri, cancellationToken);
            await wallpaperConfig.SetAuthorAsync(author, cancellationToken);
            await wallpaperConfig.SetAuthorUriAsync(authorUri, cancellationToken);
            await wallpaperConfig.SetTitleAsync(title, cancellationToken);
            await wallpaperConfig.SetTitleUriAsync(titleUri, cancellationToken);
            await wallpaperConfig.SetDescriptionAsync(description, cancellationToken);
        }
    }
}
