using System;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;

namespace DailyDesktop.Core.Providers.WikimediaCommons
{
    public class WikimediaCommonsProvider : IProvider
    {
        private const string BASE_URI = "https://commons.wikimedia.org/";
        private const string RELATIVE_IMAGE_PAGE_URI_PATTERN = "(?<=<a href=\")(.*?)(?=\" class=\"image\">)";
        private const string IMAGE_URI_PATTERN = "(?<=(<div class=\"fullMedia\">)(.*?)(href=\"))(.*?)(?=\")";

        public string Key => "COMMONS";
        public string DisplayName => "Wikimedia Commons";
        public string Description => "Takes the Picture of the day that has " +
            "been selected for display on the front page of Wikimedia Commons, " +
            "a repository of free-use images and related media. Pictures of " +
            "the day are chosen from Featured pictures of Commons, the finest " +
            "images on Wikimedia Commons selected by community consensus from " +
            "a collection of Featured picture candidates.";
        public string SourceUri => "https://commons.wikimedia.org/wiki/Commons:POTD";

        public string GetImageUri()
        {
            string potdHtml = string.Empty;
            using (WebClient client = new WebClient())
            {
                potdHtml = client.DownloadString(SourceUri);
            }
            Match relativeImagePageUriMatch = Regex.Match(potdHtml, RELATIVE_IMAGE_PAGE_URI_PATTERN);
            string relativeImagePageUri = relativeImagePageUriMatch.Value;
            if (string.IsNullOrWhiteSpace(relativeImagePageUri))
                throw new ProviderException("Didn't find a relative image page URI.");
            string imagePageUri = BASE_URI + relativeImagePageUri;

            string imagePageHtml = string.Empty;
            using (WebClient client = new WebClient())
            {
                imagePageHtml = client.DownloadString(imagePageUri);
            }
            Match imageUriMatch = Regex.Match(imagePageHtml, IMAGE_URI_PATTERN);
            string imageUri = imageUriMatch.Value;
            if (string.IsNullOrWhiteSpace(imageUri))
                throw new ProviderException("Didn't find an image URI.");

            return imageUri;
        }
    }
}
