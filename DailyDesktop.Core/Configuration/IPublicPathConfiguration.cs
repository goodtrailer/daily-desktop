// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using DailyDesktop.Core.Providers;

namespace DailyDesktop.Core.Configuration
{
    /// <summary>
    /// Public read-write interface to path settings for <see cref="DailyDesktopCore"/>.
    /// </summary>
    public interface IPublicPathConfiguration : IReadOnlyPathConfiguration
    {
        /// <summary>
        /// The assembly directory (e.g. for the task executable).
        /// </summary>
        new string AssemblyDir { get; set; }

        /// <summary>
        /// The providers directory (e.g. for <see cref="IProvider"/> DLL modules).
        /// </summary>
        new string ProvidersDir { get; set; }

        /// <summary>
        /// The serialization directory (e.g. for the <see cref="TaskConfiguration"/> JSON).
        /// </summary>
        new string SerializationDir { get; set; }
    }
}
