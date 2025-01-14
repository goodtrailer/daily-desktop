// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using DailyDesktop.Core.Configuration;
using DailyDesktop.Core.Providers;

namespace DailyDesktop.Providers.Bing
{
    public class BingProvider : IProvider
    {
        public string DisplayName => "Bing";
        public string Description => "Grabs Bing's featured Image of the Day, which can be found on Bing's home page.";
        public string SourceUri => "https://www.bing.com";

        private struct ResponseImage
        {
            public ResponseImage() { }

            public string Url { get; set; } = "";
            public string CopyrightLink { get; set; } = "";
            public string Title { get; set; } = "";
            public string CopyrightOnly { get; set; } = "";
            public string Desc { get; set; } = "";
        }

        private struct Response
        {
            public Response() { }

            public List<ResponseImage> Images { get; set; } = new List<ResponseImage>();
        }

        public async Task ConfigureWallpaperAsync(HttpClient client, IPublicWallpaperConfiguration wallpaperConfig, CancellationToken cancellationToken)
        {
            string json = await client.GetStringAsync("https://global.bing.com/HPImageArchive.aspx?format=js&idx=0&n=9&pid=hp&FORM=BEHPTB&uhd=1&uhdwidth=3840&uhdheight=2160");
            Response response;

            try
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                };
                response = JsonSerializer.Deserialize<Response>(json, options);
            }
            catch (JsonException)
            {
                throw new ProviderException("Bing API response could not be parsed as JSON. Here it is:\n\"\"\"\n" + json + "\n\"\"\"");
            }

            if (response.Images.Count == 0)
                throw new ProviderException("Bing API response did not contain any images.");

            var image = response.Images[0];

            if (string.IsNullOrWhiteSpace(image.Url))
                throw new ProviderException("Didn't find a relative image URI.");

            string imageUri = SourceUri + image.Url;

            string author = image.CopyrightOnly;
            string title = image.Title;
            string titleUri = SourceUri + image.CopyrightLink;
            string description = "TODAY ON BING\r\n" + image.Desc;

            await wallpaperConfig.SetImageUriAsync(imageUri, cancellationToken);
            await wallpaperConfig.SetAuthorAsync(author, cancellationToken);
            await wallpaperConfig.SetTitleAsync(title, cancellationToken);
            await wallpaperConfig.SetTitleUriAsync(titleUri, cancellationToken);
            await wallpaperConfig.SetDescriptionAsync(description, cancellationToken);
        }
    }
}
