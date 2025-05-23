﻿// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using DailyDesktop.Core.Configuration;
using DailyDesktop.Core.Providers;

namespace DailyDesktop.Providers.WikimediaCommons
{
    public class WikimediaCommonsProvider : IProvider
    {
        // stage 1: potd feed
        private const string TITLE_RELATIVE_URI_PATTERN = "File:.*?(?=\")";
        private const string DESCRIPTION_PATTERN = "(?<=display:inline;\">).*?(?=<\\/div>)";

        // stage 2: api request
        private const string AUTHOR_ELEMENT_PATTERN = "<author>[\\S\\s]*?<\\/author>";
        private const string AUTHOR_PATTERN = "(?<=http[s]?:\\/\\/commons\\.wikimedia\\.org\\/wiki\\/.*?\" title=\").*?(?=\")";
        private const string AUTHOR_URI_PATTERN = "http[s]?:\\/\\/commons\\.wikimedia\\.org\\/wiki\\/.*?(?=\")";
        private const string IMAGE_URI_PATTERN = "(?<=<urls><file>).*?(?=<\\/file>)";

        public string DisplayName => "Wikimedia Commons";
        public string Description => "Takes the Picture of the day that has " +
            "been selected for display on the front page of Wikimedia Commons, " +
            "a repository of free-use images and related media. Pictures of " +
            "the day are chosen from Featured pictures of Commons, the finest " +
            "images on Wikimedia Commons selected by community consensus from " +
            "a collection of Featured picture candidates.";
        public string SourceUri => "https://commons.wikimedia.org/wiki/Commons:POTD";

        public async Task ConfigureWallpaperAsync(HttpClient client, IPublicWallpaperConfiguration wallpaperConfig, CancellationToken cancellationToken)
        {
            // Scrape info from POTD RSS feed

            string feedXml = HttpUtility.HtmlDecode(await client.GetStringAsync("https://commons.wikimedia.org/w/api.php?action=featuredfeed&feed=potd&feedformat=atom", cancellationToken));

            string title = HttpUtility.UrlDecode(Regex.Matches(feedXml, TITLE_RELATIVE_URI_PATTERN)[^1].Value);
            string titleUri = "https://commons.wikimedia.org/wiki/" + title;
            string description = Regex.Replace(Regex.Matches(feedXml, DESCRIPTION_PATTERN)[^1].Value, "<[^>]*>", "");

            // Scrape info from API request

            string requestXml = HttpUtility.HtmlDecode(await client.GetStringAsync("https://magnus-toolserver.toolforge.org/commonsapi.php?image=" + title.Substring("File:".Length), cancellationToken));

            string imageUri = Regex.Match(requestXml, IMAGE_URI_PATTERN).Value;
            if (string.IsNullOrEmpty(imageUri))
                throw new ProviderException("Didn't find an image URI, XML was:\"\"\"\n" + requestXml + "\n\"\"\"");

            string authorElement = Regex.Match(requestXml, AUTHOR_ELEMENT_PATTERN).Value;
            string author = Regex.Match(authorElement, AUTHOR_PATTERN).Value;
            string authorUri = Regex.Match(authorElement, AUTHOR_URI_PATTERN).Value;
            if (string.IsNullOrWhiteSpace(author))
                author = Regex.Replace(authorElement, "<[^>]*>", "");

            await wallpaperConfig.SetImageUriAsync(imageUri, cancellationToken);
            await wallpaperConfig.SetAuthorAsync(author, cancellationToken);
            await wallpaperConfig.SetAuthorUriAsync(authorUri, cancellationToken);
            await wallpaperConfig.SetTitleAsync(title, cancellationToken);
            await wallpaperConfig.SetTitleUriAsync(titleUri, cancellationToken);
            await wallpaperConfig.SetDescriptionAsync(description, cancellationToken);
        }
    }
}
