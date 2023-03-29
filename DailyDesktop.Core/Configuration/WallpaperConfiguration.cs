// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using System;

namespace DailyDesktop.Core.Configuration
{
    /// <summary>
    /// Contains information about a downloaded wallpaper ("configuration" may be slightly misleading here).
    /// </summary>
    public class WallpaperConfiguration : AbstractConfiguration<WallpaperConfiguration>, IPublicWallpaperConfiguration
    {
        /// <inheritdoc/>
        public WallpaperConfiguration(string jsonPath)
            : base(jsonPath)
        {
        }

        /// <inheritdoc/>
        public string ImageUri
        {
            get => imageUri;
            set
            {
                if (imageUri == value)
                    return;

                imageUri = value;
                Update();
            }
        }

        /// <inheritdoc/>
        public string? Author
        {
            get => author;
            set
            {
                if (author == value)
                    return;

                author = value;
                Update();
            }
        }

        /// <inheritdoc/>
        public string? AuthorUri
        {
            get => authorUri;
            set
            {
                if (authorUri == value)
                    return;

                authorUri = value;
                Update();
            }
        }

        /// <inheritdoc/>
        public string? Title
        {
            get => title;
            set
            {
                if (title == value)
                    return;

                title = value;
                Update();
            }
        }

        /// <inheritdoc/>
        public string? TitleUri
        {
            get => titleUri;
            set
            {
                if (titleUri == value)
                    return;

                titleUri = value;
                Update();
            }
        }

        /// <inheritdoc/>
        public string? Description
        {
            get => description;
            set
            {
                if (description == value)
                    return;

                description = value;
                Update();
            }
        }

        /// <inheritdoc/>
        public DateTime Date
        {
            get => date;
            set
            {
                if (date == value)
                    return;

                date = value;
                Update();
            }
        }

        /// <inheritdoc/>
        public override void Load(WallpaperConfiguration other)
        {
            ImageUri = other.ImageUri;
            Author = other.Author;
            AuthorUri = other.AuthorUri;
            Title = other.Title;
            TitleUri = other.TitleUri;
            Description = other.Description;
            Date = other.Date;
        }

        /// <inheritdoc/>
        public int NullifyWhitespace()
        {
            int count = 0;

            if (string.IsNullOrWhiteSpace(author))
            {
                author = null;
                count++;
            }

            if (string.IsNullOrWhiteSpace(authorUri))
            {
                authorUri = null;
                count++;
            }

            if (string.IsNullOrWhiteSpace(title))
            {
                title = null;
                count++;
            }

            if (string.IsNullOrWhiteSpace(titleUri))
            {
                titleUri = null;
                count++;
            }

            if (string.IsNullOrWhiteSpace(description))
            {
                description = null;
                count++;
            }

            return count;
        }

        private string imageUri = "";
        private string? author;
        private string? authorUri;
        private string? title;
        private string? titleUri;
        private string? description;
        private DateTime date = DateTime.Now;
    }
}
