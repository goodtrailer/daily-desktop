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
        public string ImageUri { get => imageUri; init => imageUri = value; }

        /// <inheritdoc/>
        public string? Author { get => author; init => author = value; }

        /// <inheritdoc/>
        public string? AuthorUri { get => authorUri; init => authorUri = value; }

        /// <inheritdoc/>
        public string? Title { get => title; init => title = value; }

        /// <inheritdoc/>
        public string? TitleUri { get => titleUri; init => titleUri = value; }

        /// <inheritdoc/>
        public string? Description { get => description; init => description = value; }

        /// <inheritdoc/>
        public DateTime Date { get => date; init => date = value; }

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
            imageUri = other.ImageUri;
            author = other.Author;
            authorUri = other.AuthorUri;
            title = other.Title;
            titleUri = other.TitleUri;
            description = other.Description;
            date = other.Date;
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
