// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using System;
using System.Net;
using System.Text.RegularExpressions;

namespace DailyDesktop.Core.Providers.WikimediaCommons
{
    public class WikimediaCommonsProvider : IProvider
    {
        private const string BASE_URI = "https://commons.wikimedia.org/";
        private const string IMAGE_URI_PATTERN = "(?<=(<div class=\"fullMedia\">)(.*?)(href=\"))(.*?)(?=\")";
        private const string AUTHOR_PATTERN = "(?<=(Author</td>([\\S\\s]*)User:(.*)\">))(.*?)(?=(</a>))";
        private const string AUTHOR_RELATIVE_URI_PATTERN = "(?<=(Author</td>[\\S\\s]*?<a href=\"))/wiki/User:(.*?)(?=(\"))";
        private const string TITLE = "Picture";
        private const string TITLE_RELATIVE_URI_PATTERN = "(?<=<a href=\")(.*?)(?=\" class=\"image\">)";
        private const string DESCRIPTION_PATTERN = "(?<=(<div(.*?)lang=\"en\"><span(.*?)><b>(.*?)</b></span>))(.*?)(?=(</div>))";

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
            string potdHtml = string.Empty;
            using (WebClient client = new WebClient())
            {
                client.Headers.Add(HttpRequestHeader.UserAgent, "daily-desktop/0.0 (https://github.com/goodtrailer/daily-desktop)");
                potdHtml = client.DownloadString(SourceUri);
            }
            Match titleRelativeUriMatch = Regex.Match(potdHtml, TITLE_RELATIVE_URI_PATTERN);
            string titleRelativeUri = titleRelativeUriMatch.Value;
            if (string.IsNullOrWhiteSpace(titleRelativeUri))
                throw new ProviderException("Didn't find a relative image page URI.");
            string titleUri = BASE_URI + titleRelativeUri;

            string imagePageHtml = string.Empty;
            using (WebClient client = new WebClient())
            {
                client.Headers.Add(HttpRequestHeader.UserAgent, "daily-desktop/0.0 (https://github.com/goodtrailer/daily-desktop)");
                imagePageHtml = client.DownloadString(titleUri);
            }
            Match imageUriMatch = Regex.Match(imagePageHtml, IMAGE_URI_PATTERN);
            string imageUri = imageUriMatch.Value;
            if (string.IsNullOrWhiteSpace(imageUri))
                throw new ProviderException("Didn't find an image URI.");

            Match authorMatch = Regex.Match(imagePageHtml, AUTHOR_PATTERN);
            string author = authorMatch.Value;

            Match authorRelativeUriMatch = Regex.Match(imagePageHtml, AUTHOR_RELATIVE_URI_PATTERN);
            string authorRelativeUri = authorRelativeUriMatch.Value;
            string authorUri = string.IsNullOrWhiteSpace(authorRelativeUri) ? null : BASE_URI + authorRelativeUri;

            Match descriptionMatch = Regex.Match(imagePageHtml, DESCRIPTION_PATTERN);
            string description = descriptionMatch.Value;
            description = Regex.Replace(description, "<([^<>]*?)>", "");

            WallpaperInfo wallpaper = new WallpaperInfo
            {
                ImageUri = imageUri,
                Date = DateTime.Now,
                Author = author,
                AuthorUri = authorUri,
                Title = TITLE,
                TitleUri = titleUri,
                Description = description,
            };

            return wallpaper;
        }
    }
}
