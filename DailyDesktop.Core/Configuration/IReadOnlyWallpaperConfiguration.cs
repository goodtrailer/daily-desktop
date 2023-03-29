// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using System;

namespace DailyDesktop.Core.Configuration
{
    /// <summary>
    /// Read-only interface to information about a downloaded wallpaper ("configuration" may be slightly misleading here).
    /// </summary>
    public interface IReadOnlyWallpaperConfiguration : IReadOnlyConfiguration
    {
        /// <summary>
        /// The URI of the image file. Cannot be null because there must be an image is necessary.
        /// </summary>
        string ImageUri { get; }

        /// <summary>
        /// The date when the image was downloaded.
        /// </summary>
        DateTime Date { get; }

        /// <summary>
        /// The author of the work (i.e. the illustrator, photographer, painter, etc.)
        /// </summary>
        string? Author { get; }

        /// <summary>
        /// A URI to the <see cref="Author"/>. Usually a URL to the author's website or profile page.
        /// </summary>
        string? AuthorUri { get; }

        /// <summary>
        /// The title of the work.
        /// </summary>
        string? Title { get; }

        /// <summary>
        /// A URI to the work. Usually a URL to the image's page on the source website where it was downloaded from.
        /// </summary>
        string? TitleUri { get; }

        /// <summary>
        /// A description for the work.
        /// </summary>
        string? Description { get; }
    }
}
