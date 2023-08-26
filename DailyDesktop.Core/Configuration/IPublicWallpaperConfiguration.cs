// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using System.Threading;
using System.Threading.Tasks;

namespace DailyDesktop.Core.Configuration
{
    /// <summary>
    /// Public read-write interface to information about a downloaded wallpaper ("configuration" may be slightly misleading here).
    /// </summary>
    public interface IPublicWallpaperConfiguration : IReadOnlyWallpaperConfiguration, IPublicNullableConfiguration
    {
        /// <summary>
        /// Asynchronously sets the URI of the image file. Cannot be null because there must be an image is necessary.
        /// </summary>
        /// <param name="imageUri">The value to set as.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
        Task SetImageUriAsync(string imageUri, CancellationToken cancellationToken);

        /// <summary>
        /// Asynchronously sets the author of the work (i.e. the illustrator, photographer, painter, etc.)
        /// </summary>
        /// <param name="author">The value to set as.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
        Task SetAuthorAsync(string? author, CancellationToken cancellationToken);

        /// <summary>
        /// Asynchronously sets the URI to the <see cref="IReadOnlyWallpaperConfiguration.Author"/>. Usually a URL to the author's website or profile page.
        /// </summary>
        /// <param name="authorUri">The value to set as.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
        Task SetAuthorUriAsync(string? authorUri, CancellationToken cancellationToken);

        /// <summary>
        /// Asynchronously sets the title of the work.
        /// </summary>
        /// <param name="title">The value to set as.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
        Task SetTitleAsync(string? title, CancellationToken cancellationToken);

        /// <summary>
        /// Asynchronously sets the URI to the work. Usually a URL to the image's page on the source website where it was downloaded from.
        /// </summary>
        /// <param name="titleUri">The value to set as.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
        Task SetTitleUriAsync(string? titleUri, CancellationToken cancellationToken);

        /// <summary>
        /// Asynchronously sets the description for the work.
        /// </summary>
        /// <param name="description">The value to set as.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
        Task SetDescriptionAsync(string? description, CancellationToken cancellationToken);
    }
}
