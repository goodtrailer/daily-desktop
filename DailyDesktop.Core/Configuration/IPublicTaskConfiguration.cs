// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using System;
using DailyDesktop.Core.Providers;

namespace DailyDesktop.Core.Configuration
{
    /// <summary>
    /// Public read-write interface to task settings for <see cref="DailyDesktopCore"/> that is meant to be serialized.
    /// </summary>
    public interface IPublicTaskConfiguration : IReadOnlyTaskConfiguration
    {
        /// <summary>
        /// The path of the <see cref="IProvider"/> DLL module.
        /// </summary>
        new string Dll { get; set; }

        /// <summary>
        /// Whether wallpaper update <see cref="Microsoft.Win32.TaskScheduler.Task"/> triggers are
        /// enabled or disabled.
        /// </summary>
        new bool IsEnabled { get; set; }

        /// <summary>
        /// The time at which the daily wallpaper update trigger executes. Only applies if
        /// <see cref="IsEnabled"/> is set to <c>true</c>.
        /// </summary>
        new DateTime UpdateTime { get; set; }

        /// <summary>
        /// Whether or not to apply resize to wallpaper images to screen resolution, if larger.
        /// </summary>
        new bool DoResize { get; set; }

        /// <summary>
        /// Whether or not to apply blurred-fit to wallpaper images.
        /// </summary>
        new bool DoBlurredFit { get; set; }

        /// <summary>
        /// The blur strength for wallpaper images. Only applies if
        /// <see cref="DoBlurredFit"/> is set to <c>true</c>.
        /// </summary>
        new int BlurStrength { get; set; }
    }
}
