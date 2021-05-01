
using System.Net;
using System.Text.RegularExpressions;

namespace DailyDesktop.Core.Providers.Unsplash
{
    public class UnsplashProvider : IProvider
    {
        private const string TRUE_SOURCE_URI = "https://unsplash.com/";
        private const string IMAGE_URI_PATTERN = "(?<=<source srcSet=\")(.*?)(?=\\?)";

        public string Key => "UNSPLASH";
        public string DisplayName => "Unsplash";
        public string Description => "Nabs the Photo of the Day that is currently being displayed on the front page of the website Unsplash, an online source for high-quality and freely-usable images.\r\n" +
            "An archive of previously showcased Photos of the Day is linked below.";
        public string SourceUri => "https://unsplash.com/collections/1459961/photo-of-the-day-(archive)";

        public string GetImageUri()
        {
            string pageHtml = string.Empty;
            using (WebClient client = new WebClient())
            {
                pageHtml = client.DownloadString(TRUE_SOURCE_URI);
            }

            Match imageUriMatch = Regex.Match(pageHtml, IMAGE_URI_PATTERN);
            string imageUri = imageUriMatch.Value;
            if (string.IsNullOrWhiteSpace(imageUri))
                throw new ProviderException("Didn't find an image URI.");

            return imageUri;
        }
    }
}
