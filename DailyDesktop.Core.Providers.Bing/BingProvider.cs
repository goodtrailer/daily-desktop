// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using System.Net;
using System.Text.RegularExpressions;

namespace DailyDesktop.Core.Providers.Bing
{
    public class BingProvider : IProvider
    {
        private const string RESOLUTION_PATTERN = "(?<=(_))([0-9x]*)(?=(\\.))";
        private const string RESOLUTION_REPLACEMENT = "1920x1080";
        private const string IMAGE_URI_PATTERN = "(?<=(<a href=\"))(/th\\?id=)(.*?)(?=(&))";
        private const string TITLE_PATTERN = "(?<=(<div class=\"vs_bs_title\">))(.*?)(?=(</div>))";
        private const string DESCRIPTION_PATTERN = "(?<=(<span(.*?)id=\"iotd_desc\">))(.*?)(?=(</span>))";

        public string Key => "BING";
        public string DisplayName => "Bing";
        public string Description
        {
            get
            {
                string pageHtml = string.Empty;
                using (WebClient client = new WebClient())
                {
                    pageHtml = client.DownloadString(SourceUri);
                }

                Match titleMatch = Regex.Match(pageHtml, TITLE_PATTERN);
                string title = titleMatch.Value;
                if (string.IsNullOrWhiteSpace(title))
                    title = "No Title";

                Match descMatch = Regex.Match(pageHtml, DESCRIPTION_PATTERN);
                string desc = descMatch.Value;
                if (string.IsNullOrWhiteSpace(desc))
                    desc = "No description.";

                string ret = "Grabs Bing's featured Image of the Day, which can be found on Bing's home page.\r\n" +
                    "\r\n" +
                    "TODAY ON BING.\r\n" +
                    title + "\r\n" +
                    "\r\n" +
                    desc;

                return ret;
            }
        }

        public string SourceUri => "https://www.bing.com/";

        public string GetImageUri()
        {
            string pageHtml = string.Empty;
            using (WebClient client = new WebClient())
            {
                pageHtml = client.DownloadString(SourceUri);
            }
            Match relativeImageUriMatch = Regex.Match(pageHtml, IMAGE_URI_PATTERN);
            string relativeImageUri = relativeImageUriMatch.Value;
            if (string.IsNullOrWhiteSpace(relativeImageUri))
                throw new ProviderException("Didn't find a relative image URI.");

            relativeImageUri = Regex.Replace(relativeImageUri, RESOLUTION_PATTERN, RESOLUTION_REPLACEMENT);
            string imageUri = SourceUri + relativeImageUri;
            return imageUri;
        }
    }
}
