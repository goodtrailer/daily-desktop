// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

namespace DailyDesktop.Core.Configuration
{
    /// <summary>
    /// Public read-write interface to information about a downloaded wallpaper ("configuration" may be slightly misleading here).
    /// </summary>
    public interface IPublicWallpaperConfiguration : IReadOnlyWallpaperConfiguration, IPublicNullableConfiguration
    {
        /// <summary>
        /// The URI of the image file. Cannot be null because there must be an image is necessary.
        /// </summary>
        new string ImageUri { get; set; }

        /// <summary>
        /// The author of the work (i.e. the illustrator, photographer, painter, etc.)
        /// </summary>
        new string? Author { get; set; }

        /// <summary>
        /// A URI to the <see cref="Author"/>. Usually a URL to the author's website or profile page.
        /// </summary>
        new string? AuthorUri { get; set; }

        /// <summary>
        /// The title of the work.
        /// </summary>
        new string? Title { get; set; }

        /// <summary>
        /// A URI to the work. Usually a URL to the image's page on the source website where it was downloaded from.
        /// </summary>
        new string? TitleUri { get; set; }

        /// <summary>
        /// A description for the work.
        /// </summary>
        new string? Description { get; set; }
    }
}
