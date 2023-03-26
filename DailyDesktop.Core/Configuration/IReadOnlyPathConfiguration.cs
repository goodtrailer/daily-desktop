// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using DailyDesktop.Core.Providers;

namespace DailyDesktop.Core.Configuration
{
    /// <summary>
    /// Read-only interface to path settings for <see cref="DailyDesktopCore"/>.
    /// </summary>
    public interface IReadOnlyPathConfiguration : IReadOnlyConfiguration
    {
        /// <summary>
        /// The assembly directory (e.g. for the task executable).
        /// </summary>
        public string AssemblyDir { get; }

        /// <summary>
        /// The providers directory (e.g. for <see cref="IProvider"/> DLL modules).
        /// </summary>
        public string ProvidersDir { get; }

        /// <summary>
        /// The serialization directory (e.g. for the <see cref="TaskConfiguration"/> JSON).
        /// </summary>
        public string SerializationDir { get; }

        /// <summary>
        /// The task executable path.
        /// </summary>
        public string TaskExecutable { get; }

        /// <summary>
        /// The task configuration JSON file (serialization output) path.
        /// </summary>
        public string TaskConfigJson { get; }

        /// <summary>
        /// The wallpaper JSON file path.
        /// </summary>
        public string WallpaperJson { get; }
    }
}
