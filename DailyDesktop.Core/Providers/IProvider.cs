// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using System;
using System.Net.Http;
using System.Threading.Tasks;

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
        /// Configures an <see cref="HttpClient"/> to be used for the
        /// <see cref="WallpaperInfo.ImageUri"/> returned by
        /// <see cref="GetWallpaperInfo"/>. Used by
        /// <see cref="IProviderExtensions.CreateHttpClient(IProvider)"/>.
        /// </summary>
        /// <param name="client">The <see cref="HttpClient"/> to configure.</param>
        void ConfigureHttpClient(HttpClient client) { }

        /// <summary>
        /// Gets an up-to-date <see cref="WallpaperInfo"/> using a given
        /// pre-configured <see cref="HttpClient"/>.
        /// </summary>
        /// <param name="client">A pre-configured <see cref="HttpClient"/>.</param>
        /// <returns>The up-to-date <see cref="WallpaperInfo"/>.</returns>
        Task<WallpaperInfo> GetWallpaperInfo(HttpClient client);

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
        /// Creates an <see cref="HttpClient"/> with a proper User-Agent header and
        /// configured by <see cref="IProvider.ConfigureHttpClient(HttpClient)"/>.
        /// </summary>
        /// <param name="provider">
        /// The <see cref="IProvider"/> configuring the returned <see cref="HttpClient"/>.
        /// </param>
        /// <returns>The pre-configured <see cref="HttpClient"/>.</returns>
        public static HttpClient CreateHttpClient(this IProvider provider)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("User-Agent", "daily-desktop/0.0 (https://github.com/goodtrailer/daily-desktop)");
            provider.ConfigureHttpClient(client);

            return client;
        }

        /// <summary>
        /// Gets an up-to-date <see cref="WallpaperInfo"/> using a pre-configured
        /// <see cref="HttpClient"/> created by <see cref="CreateHttpClient(IProvider)"/>.
        /// </summary>
        /// <param name="provider">
        /// The <see cref="IProvider"/> getting the up-to-date <see cref="WallpaperInfo"/>.
        /// </param>
        /// <returns>The up-to-date <see cref="WallpaperInfo"/>.</returns>
        public static async Task<WallpaperInfo> GetWallpaperInfo(this IProvider provider)
        {
            using (var client = provider.CreateHttpClient())
                return await provider.GetWallpaperInfo(client);
        }
    }
}
