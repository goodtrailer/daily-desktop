// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using System;

namespace DailyDesktop.Core
{
    /// <summary>
    /// Contains information about a downloaded wallpaper.
    /// </summary>
    public struct WallpaperInfo
    {
        /// <summary>
        /// Gets or sets the URI of the image file.
        /// </summary>
        public string ImageUri { get; set; }

        /// <summary>
        /// Gets or sets the date when the image was downloaded.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets the author of the work (i.e. the illustrator,
        /// photographer, painter, etc.)
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// Gets or sets a URI to the <see cref="Author"/>. Usually a URL to
        /// the author's website or profile page.
        /// </summary>
        public string AuthorUri { get; set; }

        /// <summary>
        /// Gets or sets the title of the work.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets a URI to the work. Usually a URL to the image's page on
        /// the source website where it was downloaded from.
        /// </summary>
        public string TitleUri { get; set; }

        /// <summary>
        /// Gets or sets a description for the work.
        /// </summary>
        public string Description { get; set; }
    }
}
