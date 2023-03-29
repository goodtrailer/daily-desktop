// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using System.IO;
using System.Text.Json.Serialization;
using System.Web;

namespace DailyDesktop.Core.Configuration
{
    /// <summary>
    /// Contains path settings for <see cref="DailyDesktopCore"/>.
    /// </summary>
    public class PathConfiguration : AbstractConfiguration<PathConfiguration>, IPublicPathConfiguration
    {
        /// <inheritdoc/>
        public PathConfiguration(string jsonPath)
            : base(jsonPath)
        {
        }

        /// <inheritdoc/>
        public string AssemblyDir
        {
            get => assemblyDir;
            set
            {
                if (assemblyDir == value)
                    return;

                Directory.CreateDirectory(value);
                assemblyDir = value;
                Update();
            }
        }

        /// <inheritdoc/>
        public string ProvidersDir
        {
            get => providersDir;
            set
            {
                if (providersDir == value)
                    return;

                Directory.CreateDirectory(value);
                providersDir = value;
                Update();
            }
        }

        /// <inheritdoc/>
        public string SerializationDir
        {
            get => serializationDir;
            set
            {
                if (serializationDir == value)
                    return;

                Directory.CreateDirectory(value);
                serializationDir = value;
                Update();
            }
        }

        /// <inheritdoc/>
        [JsonIgnore]
        public string TaskExecutable => HttpUtility.UrlDecode(Path.Combine(assemblyDir, "DailyDesktop.Task.exe"));

        /// <inheritdoc/>
        [JsonIgnore]
        public string TaskConfigJson => HttpUtility.UrlDecode(Path.Combine(serializationDir, "settings.json"));

        /// <inheritdoc/>
        [JsonIgnore]
        public string WallpaperJson => HttpUtility.UrlDecode(Path.Combine(serializationDir, "wallpaper.json"));

        /// <inheritdoc/>
        public override void Load(PathConfiguration other)
        {
            AssemblyDir = other.AssemblyDir;
            ProvidersDir = other.ProvidersDir;
            SerializationDir = other.SerializationDir;
        }

        private string assemblyDir = "";
        private string providersDir = "";
        private string serializationDir = "";
    }
}
