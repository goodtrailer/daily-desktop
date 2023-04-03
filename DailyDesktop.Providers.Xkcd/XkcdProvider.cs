// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using DailyDesktop.Core.Configuration;
using DailyDesktop.Core.Providers;

namespace DailyDesktop.Providers.Xkcd
{
    public class XkcdProvider : IProvider
    {
        public const string AUTHOR = "Randall Munroe";
        public const string AUTHOR_URI = "https://xkcd.com/about";

        public const string IMAGE_URI_PATTERN = "(?<=(<meta property=\"og:image\" content=\")).*?(?=(\"))";
        public const string TITLE_PATTERN = "(?<=(<meta property=\"og:title\" content=\")).*?(?=(\"))";
        public const string TITLE_URI_PATTERN = "(?<=(<meta property=\"og:url\" content=\")).*?(?=(\"))";

        public string DisplayName => "xkcd";
        public string Description => "\"A webcomic of romance, sarcasm, math, and language.\"";
        public string SourceUri => "https://xkcd.com";

        public async Task ConfigureWallpaperAsync(HttpClient client, IPublicWallpaperConfiguration wallpaperConfig, CancellationToken cancellationToken)
        {
            string pageHtml = await client.GetStringAsync(SourceUri, cancellationToken);

            string imageUri = Regex.Match(pageHtml, IMAGE_URI_PATTERN).Value;
            string title = Regex.Match(pageHtml, TITLE_PATTERN).Value;
            string titleUri = Regex.Match(pageHtml, TITLE_URI_PATTERN).Value;

            await wallpaperConfig.SetImageUriAsync(imageUri, cancellationToken);
            await wallpaperConfig.SetAuthorAsync(AUTHOR, cancellationToken);
            await wallpaperConfig.SetAuthorUriAsync(AUTHOR_URI, cancellationToken);
            await wallpaperConfig.SetTitleAsync(title, cancellationToken);
            await wallpaperConfig.SetTitleUriAsync(titleUri, cancellationToken);
        }
    }
}
