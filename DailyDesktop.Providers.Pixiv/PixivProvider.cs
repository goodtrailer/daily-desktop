// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using DailyDesktop.Core.Configuration;
using DailyDesktop.Core.Providers;

namespace DailyDesktop.Providers.Pixiv
{
    public class PixivProvider : IProvider
    {
        private const string METADATA_PATTERN = "(?<=\"contents\":\\[){.*?}(?=(,{|]))";
        private const string IMAGE_PATH_PATTERN = "(?<=(img-master/))(.*?)_p0";
        private const string DESCRIPTION_PATTERN = "(?<=(summary_large_image(.*?)\"description\":\"))(.*?)(?=\")";

        public string DisplayName => "pixiv";
        public string Description => "Fetches the illustration ranked #1 on the pixiv Overall Daily Rankings for the previous day.\r\n" +
            "Using blurred-fit mode is highly recommended due to the large variety of aspect ratios of illustrations found on pixiv.";
        public string SourceUri => "https://www.pixiv.net/ranking.php?mode=daily&content=illust";

        public void ConfigureHttpRequestHeaders(HttpRequestHeaders headers)
        {
            headers.Referrer = new Uri("https://www.pixiv.net");
        }

        struct Metadata
        {
            public string Title { get; set; }
            public List<string> Tags { get; set; }
            public string Url { get; set; }
            public string UserName { get; set; }
            public ContentType IllustContentType { get; set; }
            public bool IllustSeries { get; set; }
            public int IllustId { get; set; }
            public int Width { get; set; }
            public int Height { get; set; }
            public int UserId { get; set; }
            public int Rank { get; set; }
            public int RatingCount { get; set; }
            public int ViewCount { get; set; }
            public int IllustUploadTimestamp { get; set; }

            public struct ContentType
            {
                public int Sexual { get; set; }
                public bool Lo { get; set; }
                public bool Grotesque { get; set; }
                public bool Violent { get; set; }
                public bool Homosexual { get; set; }
                public bool Drug { get; set; }
                public bool Thoughts { get; set; }
                public bool Antisocial { get; set; }
                public bool Religion { get; set; }
                public bool Original { get; set; }
                public bool Furry { get; set; }
                public bool Bl { get; set; }
                public bool Yuri { get; set; }
            }
        }

        public async Task ConfigureWallpaperAsync(HttpClient client, IPublicWallpaperConfiguration wallpaperConfig, CancellationToken cancellationToken)
        {
            // Search for image ID of #1 illustration on daily rankings page

            string rankingHtml = await client.GetStringAsync(SourceUri, cancellationToken);

            string metadataString = Regex.Match(rankingHtml, METADATA_PATTERN).Value;

            var options = new JsonSerializerOptions
            {
                AllowTrailingCommas = true,
                PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
            };
            options.AllowTrailingCommas = true;
            var metadata = JsonSerializer.Deserialize<Metadata>(metadataString, options);

            string imagePath = Regex.Match(metadata.Url, IMAGE_PATH_PATTERN).Value;
            if (string.IsNullOrWhiteSpace(imagePath))
                throw new ProviderException("Didn't find an image path, HTML was:\"\"\"\n" + rankingHtml + "\n\"\"\"");

            string[] imageUris =
            [
                "https://i.pximg.net/img-original/" + imagePath + ".png",
                "https://i.pximg.net/img-original/" + imagePath + ".jpg",
            ];

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

            string titleUri = "https://www.pixiv.net/artworks/" + metadata.IllustId;
            string authorUri = "https://www.pixiv.net/users/" + metadata.UserId;
            string title = metadata.Title;
            string author = metadata.UserName;

            // Search for wallpaper info on image page

            string imagePageHtml = await client.GetStringAsync(titleUri, cancellationToken);

            string description = WebUtility.HtmlDecode(Regex.Matches(imagePageHtml, DESCRIPTION_PATTERN).LastOrDefault()?.Value) ?? "";
            description = description.Replace("\\r", "\r").Replace("\\n", "\n");
            if (metadata.Tags.Count > 0)
                description += "\n\n" + string.Join(" ", metadata.Tags.Select(t => "#" + t));

            await wallpaperConfig.SetImageUriAsync(imageUri, cancellationToken);
            await wallpaperConfig.SetAuthorAsync(author, cancellationToken);
            await wallpaperConfig.SetAuthorUriAsync(authorUri, cancellationToken);
            await wallpaperConfig.SetTitleAsync(title, cancellationToken);
            await wallpaperConfig.SetTitleUriAsync(titleUri, cancellationToken);
            await wallpaperConfig.SetDescriptionAsync(description, cancellationToken);
        }
    }
}
