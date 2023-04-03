// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using System.Net.Http;

namespace DailyDesktop.Core.Util
{
    /// <summary>
    /// Utilities for Http logic.
    /// </summary>
    public static class HttpUtils
    {
        /// <summary>
        /// An <see cref="HttpClient"/> singleton for use everywhere.
        /// </summary>
        public static HttpClient Client => client ??= new HttpClient();
        private static HttpClient? client;

        /// <summary>
        /// Resets <see cref="Client"/>'s request headers, which includes adding
        /// a user agent header that points to Daily Desktop.
        /// </summary>
        public static void ResetRequestHeaders()
        {
            Client.DefaultRequestHeaders.Clear();
            Client.DefaultRequestHeaders.Add("User-Agent", "daily-desktop/0.0 (https://github.com/goodtrailer/daily-desktop)");
        }
    }
}
