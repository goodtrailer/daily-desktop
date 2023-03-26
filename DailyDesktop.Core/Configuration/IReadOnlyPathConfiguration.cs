﻿// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using DailyDesktop.Core.Providers;

namespace DailyDesktop.Core.Configuration
{
    /// <summary>
    /// Contains read-only directory settings for <see cref="DailyDesktopCore"/>.
    /// </summary>
    public interface IReadOnlyPathConfiguration : IReadOnlyConfiguration
    {
        /// <summary>
        /// Gets the assembly directory (e.g. for the task executable).
        /// </summary>
        public string AssemblyDir { get; }

        /// <summary>
        /// Gets where <see cref="IProvider"/> DLL modules are automatically scanned from.
        /// </summary>
        public string ProvidersDir { get; }

        /// <summary>
        /// Gets the serialization directory (e.g. for the <see cref="TaskConfiguration"/> JSON).
        /// </summary>
        public string SerializationDir { get; }

        public string TaskExecutable { get; }

        public string TaskConfigJson { get; }

        public string WallpaperJson { get; }
    }
}
