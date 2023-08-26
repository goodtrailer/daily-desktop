// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using System;
using System.Threading;
using System.Threading.Tasks;
using DailyDesktop.Core.Providers;

namespace DailyDesktop.Core.Configuration
{
    /// <summary>
    /// Public read-write interface to task settings for <see cref="DailyDesktopCore"/> that is meant to be serialized.
    /// </summary>
    public interface IPublicTaskConfiguration : IReadOnlyTaskConfiguration
    {
        /// <summary>
        /// Sets the path of the <see cref="IProvider"/> DLL module.
        /// </summary>
        /// <param name="dll">The value to set as.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
        Task SetDllAsync(string dll, CancellationToken cancellationToken);

        /// <summary>
        /// Sets whether wallpaper update <see cref="Microsoft.Win32.TaskScheduler.Task"/> triggers are
        /// enabled or disabled.
        /// </summary>
        /// <param name="isEnabled">The value to set as.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
        Task SetIsEnabledAsync(bool isEnabled, CancellationToken cancellationToken);

        /// <summary>
        /// Sets the time at which the daily wallpaper update trigger executes. Only applies if
        /// <see cref="IReadOnlyTaskConfiguration.IsEnabled"/> is <c>true</c>.
        /// </summary>
        /// <param name="updateTime">The value to set as.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
        Task SetUpdateTimeAsync(DateTime updateTime, CancellationToken cancellationToken);

        /// <summary>
        /// Sets whether or not to apply resize to wallpaper images to screen resolution, if larger.
        /// </summary>
        /// <param name="doResize">The value to set as.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
        Task SetDoResizeAsync(bool doResize, CancellationToken cancellationToken);

        /// <summary>
        /// Sets whether or not to apply blurred-fit to wallpaper images.
        /// </summary>
        /// <param name="doBlurredFit">The value to set as.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
        Task SetDoBlurredFitAsync(bool doBlurredFit, CancellationToken cancellationToken);

        /// <summary>
        /// Sets the blur strength for wallpaper images. Only applies if
        /// <see cref="IReadOnlyTaskConfiguration.DoBlurredFit"/> is set to <c>true</c>.
        /// </summary>
        /// <param name="blurStrength">The value to set as.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
        Task SetBlurStrengthAsync(int blurStrength, CancellationToken cancellationToken);
    }
}
