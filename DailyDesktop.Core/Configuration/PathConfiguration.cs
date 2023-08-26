// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using System.IO;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace DailyDesktop.Core.Configuration
{
    /// <summary>
    /// Contains path settings for <see cref="DailyDesktopCore"/>.
    /// </summary>
    public class PathConfiguration : AbstractConfiguration<PathConfiguration>, IPublicPathConfiguration
    {
        /// <inheritdoc/>
        public PathConfiguration(string jsonPath = "")
            : base(jsonPath)
        {
        }

        /// <inheritdoc/>
        public string AssemblyDir { get => assemblyDir; init => assemblyDir = value; }

        /// <inheritdoc/>
        public string ProvidersDir { get => providersDir; init => providersDir = value; }

        /// <inheritdoc/>
        public string SerializationDir { get => serializationDir; init => serializationDir = value; }

        /// <inheritdoc/>
        [JsonIgnore]
        public string TaskExecutable => HttpUtility.UrlDecode(Path.Combine(AssemblyDir, "DailyDesktop.Task.exe"));

        /// <inheritdoc/>
        [JsonIgnore]
        public string TaskConfigJson => HttpUtility.UrlDecode(Path.Combine(SerializationDir, "settings.json"));

        /// <inheritdoc/>
        [JsonIgnore]
        public string WallpaperJson => HttpUtility.UrlDecode(Path.Combine(SerializationDir, "wallpaper.json"));

        /// <inheritdoc/>
        public async Task SetAssemblyDirAsync(string assemblyDir, CancellationToken cancellationToken)
        {
            if (this.assemblyDir == assemblyDir)
                return;

            Directory.CreateDirectory(assemblyDir);
            this.assemblyDir = assemblyDir;
            await UpdateAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public async Task SetProvidersDirAsync(string providersDir, CancellationToken cancellationToken)
        {
            if (this.providersDir == providersDir)
                return;

            Directory.CreateDirectory(providersDir);
            this.providersDir = providersDir;
            await UpdateAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public async Task SetSerializationDirAsync(string serializationDir, CancellationToken cancellationToken)
        {
            if (this.serializationDir == serializationDir)
                return;

            Directory.CreateDirectory(serializationDir);
            this.serializationDir = serializationDir;
            await UpdateAsync(cancellationToken);
        }

        /// <inheritdoc/>
        protected override void LoadImpl(PathConfiguration other)
        {
            assemblyDir = other.assemblyDir;
            providersDir = other.providersDir;
            serializationDir = other.serializationDir;
        }

        private string assemblyDir = "";
        private string providersDir = "";
        private string serializationDir = "";
    }
}
