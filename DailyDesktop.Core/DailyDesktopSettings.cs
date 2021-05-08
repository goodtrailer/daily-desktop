// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using System;
using DailyDesktop.Core.Providers;

namespace DailyDesktop.Core
{
    /// <summary>
    /// Contains settings for <see cref="DailyDesktopCore"/> that is meant to be
    /// serialized.
    /// </summary>
    public struct DailyDesktopSettings
    {
        /// <summary>
        /// Gets or sets the path of the <see cref="IProvider"/> DLL module.
        /// </summary>
        public string DllPath { get; set; }

        /// <summary>
        /// Gets or sets whether wallpaper update <see cref="Microsoft.Win32.TaskScheduler.Task"/> triggers are
        /// enabled or disabled.
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// Gets or sets the time at which the daily wallpaper update trigger
        /// executes. Only applies if <see cref="Enabled"/> is set to <c>true</c>.
        /// </summary>
        public DateTime UpdateTime { get; set; }

        /// <summary>
        /// Gets or sets whether or not to apply blurred-fit to wallpaper images.
        /// </summary>
        public bool DoBlurredFit { get; set; }

        /// <summary>
        /// Gets or sets the blur strength for wallpaper images. Only applies if
        /// <see cref="DoBlurredFit"/> is set to <c>true</c>.
        /// </summary>
        public int BlurStrength { get; set; }

        /// <summary>
        /// A <see cref="DailyDesktopSettings"/> with default values (default as
        /// in non-zero, default settings for the end-user, i.e.
        /// <see cref="BlurStrength"/> = 40).
        /// </summary>
        public readonly static DailyDesktopSettings Default = new DailyDesktopSettings
        {
            DllPath = string.Empty,
            Enabled = true,
            UpdateTime = DateTime.Parse("12:00 AM"),
            DoBlurredFit = true,
            BlurStrength = 40,
        };
    }
}
