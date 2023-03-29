// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using System.Net.Http;
using System.Text.RegularExpressions;
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

        public async Task ConfigureWallpaper(HttpClient client, IPublicWallpaperConfiguration wallpaperConfig)
        {
            // Scrape info from POTD RSS feed

            string feedXml = HttpUtility.HtmlDecode(await client.GetStringAsync("https://commons.wikimedia.org/w/api.php?action=featuredfeed&feed=potd&feedformat=atom"));

            wallpaperConfig.Title = HttpUtility.UrlDecode(Regex.Matches(feedXml, TITLE_RELATIVE_URI_PATTERN)[^1].Value);
            wallpaperConfig.TitleUri = "https://commons.wikimedia.org/wiki/" + wallpaperConfig.Title;
            wallpaperConfig.Description = Regex.Replace(Regex.Matches(feedXml, DESCRIPTION_PATTERN)[^1].Value, "<[^>]*>", "");

            // Scrape info from API request

            string requestXml = HttpUtility.HtmlDecode(await client.GetStringAsync("https://magnus-toolserver.toolforge.org/commonsapi.php?image=" + wallpaperConfig.Title.Substring("File:".Length)));

            wallpaperConfig.ImageUri = Regex.Match(requestXml, IMAGE_URI_PATTERN).Value;
            if (string.IsNullOrEmpty(wallpaperConfig.ImageUri))
                throw new ProviderException("Didn't find an image URI.");

            string authorElement = Regex.Match(requestXml, AUTHOR_ELEMENT_PATTERN).Value;
            wallpaperConfig.Author = Regex.Match(authorElement, AUTHOR_PATTERN).Value;
            wallpaperConfig.AuthorUri = Regex.Match(authorElement, AUTHOR_URI_PATTERN).Value;
            if (string.IsNullOrWhiteSpace(wallpaperConfig.Author))
                wallpaperConfig.Author = Regex.Replace(authorElement, "<[^>]*>", "");
        }
    }
}
