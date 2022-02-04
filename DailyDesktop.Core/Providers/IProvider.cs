// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using System;
using System.Net;

namespace DailyDesktop.Core.Providers
{
    /// <summary>
    /// Interface for providing desktop wallpaper image URIs and some extra
    /// useful information to display to the user. Should be compiled into DLL
    /// modules and loaded by a <see cref="ProviderStore"/>.
    /// </summary>
    public interface IProvider
    {
        /// <summary>
        /// The name that will appear in the Provider combobox.
        /// </summary>
        string DisplayName { get; }

        /// <summary>
        /// The description that will appear in the Provider description box.
        /// </summary>
        string Description { get; }

        /// <summary>
        /// The source URI that the images are coming from. Usually, this is the
        /// website the images are hosted on.
        /// </summary>
        string SourceUri { get; }

        /// <summary>
        /// Configures a WebClient to be used for the
        /// <see cref="WallpaperInfo.ImageUri"/> returned by
        /// <see cref="GetWallpaperInfo"/>. Used by
        /// <see cref="IProviderExtensions.CreateWebClient(IProvider)"/>.
        /// </summary>
        /// <param name="client">The <see cref="WebClient"/> to configure.</param>
        void ConfigureWebClient(WebClient client) { }

        /// <summary>
        /// Gets an up-to-date <see cref="WallpaperInfo"/>.
        /// </summary>
        /// <returns>The up-to-date <see cref="WallpaperInfo"/>.</returns>
        WallpaperInfo GetWallpaperInfo();

        /// <summary>
        /// Instantiates an <see cref="IProvider"/> given a <see cref="Type"/>.
        /// </summary>
        /// <param name="type">
        /// The type of the <see cref="IProvider"/> to instantiate.
        /// </param>
        /// <returns>The instance of the <see cref="IProvider"/>.</returns>
        /// <exception cref="ProviderException" />
        static IProvider Instantiate(Type type)
        {
            IProvider provider = Activator.CreateInstance(type) as IProvider;

            if (provider == null)
                throw new ProviderException("Failed to instantiate an IProvider from the assembly.");

            return provider;
        }
    }

    /// <summary>
    /// Extension methods for <see cref="IProvider"/>.
    /// </summary>
    public static class IProviderExtensions
    {
        /// <summary>
        /// Creates a <see cref="WebClient"/> with a proper User-Agent header and
        /// configured by <see cref="IProvider.ConfigureWebClient(WebClient)"/>.
        /// </summary>
        /// <param name="provider">
        /// The <see cref="IProvider"/> configuring the returned
        /// <see cref="WebClient"/>.
        /// </param>
        /// <returns></returns>
        public static WebClient CreateWebClient(this IProvider provider)
        {
            WebClient client = new WebClient();
            client.Headers.Add(HttpRequestHeader.UserAgent, "daily-desktop/0.0 (https://github.com/goodtrailer/daily-desktop)");
            provider.ConfigureWebClient(client);
            return client;
        }
    }
}
