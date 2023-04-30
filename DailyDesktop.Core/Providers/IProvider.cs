// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using DailyDesktop.Core.Configuration;
using DailyDesktop.Core.Util;

namespace DailyDesktop.Core.Providers
{
    /// <summary>
    /// Interface to providing desktop wallpaper image URIs and some extra
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
        /// <see cref="WallpaperConfiguration.ImageUri"/> returned by
        /// <see cref="ConfigureWallpaperAsync"/>. Used by
        /// <see cref="IProviderExtensions.ConfigureWallpaperAsync(IProvider, IPublicWallpaperConfiguration, CancellationToken)"/>.
        /// </summary>
        /// <param name="headers">The <see cref="HttpRequestHeaders"/> to configure.</param>
        void ConfigureHttpRequestHeaders(HttpRequestHeaders headers) { }

        /// <summary>
        /// Asynchronously onfigures a <see cref="IPublicWallpaperConfiguration"/> using a given
        /// pre-configured <see cref="HttpClient"/>.
        /// </summary>
        /// <param name="client">A pre-configured <see cref="HttpClient"/>.</param>
        /// <param name="wallpaperConfig">The <see cref="IPublicWallpaperConfiguration"/> to configure.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
        Task ConfigureWallpaperAsync(HttpClient client, IPublicWallpaperConfiguration wallpaperConfig, CancellationToken cancellationToken);

        /// <summary>
        /// Instantiates an <see cref="IProvider"/> given a <see cref="Type"/>.
        /// </summary>
        /// <param name="type">The type of the <see cref="IProvider"/> to instantiate.</param>
        /// <returns>The instance of the <see cref="IProvider"/>.</returns>
        /// <exception cref="ProviderException" />
        static IProvider Instantiate(Type type) => Activator.CreateInstance(type) as IProvider ?? throw new ProviderException("Failed to instantiate an IProvider from the assembly.");
    }

    /// <summary>
    /// Extension methods for <see cref="IProvider"/>.
    /// </summary>
    public static class IProviderExtensions
    {
        /// <summary>
        /// Asynchronously configures a <see cref="WallpaperConfiguration"/>.
        /// </summary>
        /// <param name="provider">The <see cref="IProvider"/> configuring the <see cref="WallpaperConfiguration"/>.</param>
        /// <param name="wallpaperConfig">The <see cref="IPublicWallpaperConfiguration"/> to configure.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
        /// <returns>The up-to-date <see cref="WallpaperConfiguration"/>.</returns>
        public static async Task ConfigureWallpaperAsync(this IProvider provider, IPublicWallpaperConfiguration wallpaperConfig, CancellationToken cancellationToken)
        {
            HttpUtils.ResetRequestHeaders();
            provider.ConfigureHttpRequestHeaders(HttpUtils.Client.DefaultRequestHeaders);
            await provider.ConfigureWallpaperAsync(HttpUtils.Client, wallpaperConfig, cancellationToken);

            wallpaperConfig.NullifyWhitespace();
        }
    }
}
