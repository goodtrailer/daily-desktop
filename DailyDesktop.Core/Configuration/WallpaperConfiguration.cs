// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using System;
using System.Threading;
using System.Threading.Tasks;

namespace DailyDesktop.Core.Configuration
{
    /// <summary>
    /// Contains information about a downloaded wallpaper ("configuration" may be slightly misleading here).
    /// </summary>
    public class WallpaperConfiguration : AbstractConfiguration<WallpaperConfiguration>, IPublicWallpaperConfiguration
    {
        /// <inheritdoc/>
        public WallpaperConfiguration(string jsonPath = "")
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

            Update();

            return count;
        }

        /// <inheritdoc/>
        public async Task SetImageUriAsync(string imageUri, CancellationToken cancellationToken)
        {
            if (this.imageUri == imageUri)
                return;

            this.imageUri = imageUri;
            await UpdateAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public async Task SetAuthorAsync(string? author, CancellationToken cancellationToken)
        {
            if (this.author == author)
                return;

            this.author = author;
            await UpdateAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public async Task SetAuthorUriAsync(string? authorUri, CancellationToken cancellationToken)
        {
            if (this.authorUri == authorUri)
                return;

            this.authorUri = authorUri;
            await UpdateAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public async Task SetTitleAsync(string? title, CancellationToken cancellationToken)
        {
            if (this.title == title)
                return;

            this.title = title;
            await UpdateAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public async Task SetTitleUriAsync(string? titleUri, CancellationToken cancellationToken)
        {
            if (this.titleUri == titleUri)
                return;

            this.titleUri = titleUri;
            await UpdateAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public async Task SetDescriptionAsync(string? description, CancellationToken cancellationToken)
        {
            if (this.description == description)
                return;

            this.description = description;
            await UpdateAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public async Task<int> NullifyWhitespaceAsync(CancellationToken cancellationToken)
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

            await UpdateAsync(cancellationToken);

            return count;
        }

        /// <inheritdoc/>
        protected override void LoadImpl(WallpaperConfiguration other)
        {
            imageUri = other.imageUri;
            author = other.author;
            authorUri = other.authorUri;
            title = other.title;
            titleUri = other.titleUri;
            description = other.description;
            date = other.date;
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
