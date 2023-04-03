// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using DailyDesktop.Core.Configuration;
using DailyDesktop.Core.Providers;
using DailyDesktop.Core.Util;

namespace DailyDesktop.Providers.Bing
{
    public class BingProvider : IProvider
    {
        private const string IMAGE_RELATIVE_URI_PATTERN = "(/th\\?id=)([^\"/>]*?)1920x1080.[a-z]*";
        private const string AUTHOR_PATTERN = "(?<=(<div class=\"copyright\" id=\"copyright\">))(.*?)(?=(</div>))";
        private const string TITLE_PATTERN = "(?<=(<meta property=\"og:title\" content=\"))(.*?)(?=(\" />))";
        private const string TITLE_RELATIVE_URI_PATTERN = "(?<=(\"BackstageUrl\":\"))(.*?)(?=(\"))";
        private const string DESCRIPTION_PATTERN = "(?<=(<span(.*?)id=\"iotd_desc\">))(.*?)(?=(</span>))";

        public string DisplayName => "Bing";
        public string Description => "Grabs Bing's featured Image of the Day, which can be found on Bing's home page.";
        public string SourceUri => "https://www.bing.com";

        public async Task ConfigureWallpaperAsync(HttpClient client, IPublicWallpaperConfiguration wallpaperConfig, CancellationToken cancellationToken)
        {
            string pageHtml = await client.GetStringAsync(SourceUri, cancellationToken);

            string imageRelativeUri = Regex.Match(pageHtml, IMAGE_RELATIVE_URI_PATTERN).Value;
            if (string.IsNullOrWhiteSpace(imageRelativeUri))
                throw new ProviderException("Didn't find a relative image URI.");

            string imageUri = SourceUri + imageRelativeUri;

            string author = Regex.Match(pageHtml, AUTHOR_PATTERN).Value;
            string title = Regex.Match(pageHtml, TITLE_PATTERN).Value;
            string titleUri = SourceUri + WebUtility.HtmlDecode(Regex.Unescape(Regex.Match(pageHtml, TITLE_RELATIVE_URI_PATTERN).Value)).Replace("\"", "%22");
            string description = "TODAY ON BING\r\n" + Regex.Match(pageHtml, DESCRIPTION_PATTERN).Value;

            await wallpaperConfig.SetImageUriAsync(imageUri, cancellationToken);
            await wallpaperConfig.SetAuthorAsync(author, cancellationToken);
            await wallpaperConfig.SetTitleAsync(title, cancellationToken);
            await wallpaperConfig.SetTitleUriAsync(titleUri, cancellationToken);
            await wallpaperConfig.SetDescriptionAsync(description, cancellationToken);
        }
    }
}
