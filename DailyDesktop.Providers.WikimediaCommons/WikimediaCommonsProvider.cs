// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using System;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using DailyDesktop.Core;
using DailyDesktop.Core.Providers;

namespace DailyDesktop.Providers.WikimediaCommons
{
    public class WikimediaCommonsProvider : IProvider
    {
        private const string BASE_URI = "https://commons.wikimedia.org/";
        private const string BASE_TITLE_URI = BASE_URI + "wiki/File:";
        private const string BASE_API_URI = "https://magnus-toolserver.toolforge.org/commonsapi.php?image=";
        private const string POTD_FEED_URI = "https://commons.wikimedia.org/w/api.php?action=featuredfeed&feed=potd&feedformat=atom";

        // stage 1: potd feed
        private const string TITLE_PATTERN = "(?<=<img alt=\").*?(?=\" src=)";
        private const string FILENAME_PATTERN = "(?<=wiki\\/File:).*?(?=\" class=\"image\")";
        private const string DESCRIPTION_PATTERN = "(?<=display:inline;\">).*?(?=<\\/div>)";

        // stage 2: api request
        private const string AUTHOR_PATTERN = "(?<=<author><a.*?>).*?(?=<\\/a>)";
        private const string AUTHOR_URI_PATTERN = "(?<=<author><a href=\").*?(?=\")";
        private const string IMAGE_URI_PATTERN = "(?<=<urls><file>).*?(?=<\\/file>)";

        public string DisplayName => "Wikimedia Commons";
        public string Description => "Takes the Picture of the day that has " +
            "been selected for display on the front page of Wikimedia Commons, " +
            "a repository of free-use images and related media. Pictures of " +
            "the day are chosen from Featured pictures of Commons, the finest " +
            "images on Wikimedia Commons selected by community consensus from " +
            "a collection of Featured picture candidates.";
        public string SourceUri => "https://commons.wikimedia.org/wiki/Commons:POTD";

        public WallpaperInfo GetWallpaperInfo()
        {
            // stage 1 ---

            string feedXml = null;
            using (WebClient client = new WebClient())
            {
                client.Headers.Add(HttpRequestHeader.UserAgent, "daily-desktop/0.0 (https://github.com/goodtrailer/daily-desktop)");
                feedXml = HttpUtility.HtmlDecode(client.DownloadString(POTD_FEED_URI));
            }

            string description = Regex.Matches(feedXml, DESCRIPTION_PATTERN).Last().Value;
            description = Regex.Replace(description, "<[^>]*>", "");
            string title = Regex.Matches(feedXml, TITLE_PATTERN).Last().Value;

            string filename = Regex.Matches(feedXml, FILENAME_PATTERN).Last().Value;
            if (string.IsNullOrWhiteSpace(filename))
                throw new ProviderException("Didn't find an image filename.");

            string titleUri = BASE_TITLE_URI + filename;

            // stage 2 ---

            string requestXml = null;
            using (WebClient client = new WebClient())
            {
                client.Headers.Add(HttpRequestHeader.UserAgent, "daily-desktop/0.0 (https://github.com/goodtrailer/daily-desktop)");
                requestXml = HttpUtility.HtmlDecode(client.DownloadString(BASE_API_URI + filename));
            }

            string imageUri = Regex.Match(requestXml, IMAGE_URI_PATTERN).Value;
            if (string.IsNullOrWhiteSpace(imageUri))
                throw new ProviderException("Didn't find an image URI.");

            string author = Regex.Match(requestXml, AUTHOR_PATTERN).Value;
            string authorUri = Regex.Match(requestXml, AUTHOR_URI_PATTERN).Value;

            return new WallpaperInfo
            {
                ImageUri = imageUri,
                Date = DateTime.Now,
                Author = author,
                AuthorUri = authorUri,
                Title = title,
                TitleUri = titleUri,
                Description = description,
            };
        }
    }
}
