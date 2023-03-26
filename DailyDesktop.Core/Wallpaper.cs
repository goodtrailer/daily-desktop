﻿// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using System;

namespace DailyDesktop.Core
{
    /// <summary>
    /// Contains information about a downloaded wallpaper.
    /// </summary>
    public struct Wallpaper
    {
        /// <summary>
        /// The URI of the image file.
        /// </summary>
        public string? ImageUri { get; set; }

        /// <summary>
        /// The date when the image was downloaded.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// The author of the work (i.e. the illustrator, photographer, painter, etc.)
        /// </summary>
        public string? Author { get; set; }

        /// <summary>
        /// A URI to the <see cref="Author"/>. Usually a URL to the author's website or profile page.
        /// </summary>
        public string? AuthorUri { get; set; }

        /// <summary>
        /// The title of the work.
        /// </summary>
        public string? Title { get; set; }

        /// <summary>
        /// A URI to the work. Usually a URL to the image's page on the source website where it was downloaded from.
        /// </summary>
        public string? TitleUri { get; set; }

        /// <summary>
        /// A description for the work.
        /// </summary>
        public string? Description { get; set; }
    }
}
