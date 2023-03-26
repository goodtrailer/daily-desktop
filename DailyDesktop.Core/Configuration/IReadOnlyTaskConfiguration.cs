﻿// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using DailyDesktop.Core.Providers;
using System;

namespace DailyDesktop.Core.Configuration
{
    /// <summary>
    /// Contains read-only task settings for <see cref="DailyDesktopCore"/> that is meant to be
    /// serialized.
    /// </summary>
    public interface IReadOnlyTaskConfiguration : IReadOnlyConfiguration
    {
        /// <summary>
        /// Gets the path of the <see cref="IProvider"/> DLL module.
        /// </summary>
        public string Dll { get; }

        /// <summary>
        /// Gets whether wallpaper update <see cref="Microsoft.Win32.TaskScheduler.Task"/> triggers are
        /// enabled or disabled.
        /// </summary>
        public bool IsEnabled { get; }

        /// <summary>
        /// Gets the time at which the daily wallpaper update trigger
        /// executes. Only applies if <see cref="IsEnabled"/> is set to <c>true</c>.
        /// </summary>
        public DateTime UpdateTime { get; }

        /// <summary>
        /// Gets whether or not to apply resize to wallpaper images to screen resolution, if larger.
        /// </summary>
        public bool DoResize { get; }

        /// <summary>
        /// Gets whether or not to apply blurred-fit to wallpaper images.
        /// </summary>
        public bool DoBlurredFit { get; }

        /// <summary>
        /// Gets the blur strength for wallpaper images. Only applies if
        /// <see cref="DoBlurredFit"/> is set to <c>true</c>.
        /// </summary>
        public int BlurStrength { get; }
    }
}
