// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using System.Threading;

namespace DailyDesktop.Core.Util
{
    /// <summary>
    /// Utilities for async logic.
    /// </summary>
    public static class AsyncUtils
    {
        /// <summary>
        /// Creates a <see cref="CancellationToken"/> from a <see cref="CancellationTokenSource"/>
        /// that will be cancelled in a given number of milliseconds. By default, the token
        /// will be cancelled after 1,000 milliseconds.
        /// </summary>
        /// <param name="milliseconds">The duration before cancelling.</param>
        /// <returns>The <see cref="CancellationToken"/> that will be cancelled.</returns>
        public static CancellationToken TimedCancel(int milliseconds = 1_000) => new CancellationTokenSource(milliseconds).Token;

        /// <summary>
        /// Identical to <see cref="TimedCancel(int)"/>, except with a different default argument
        /// of 30,000 milliseconds.
        /// </summary>
        /// <param name="milliseconds">The duration before cancelling.</param>
        /// <returns>The <see cref="CancellationToken"/> that will be cancelled.</returns>
        public static CancellationToken LongCancel(int milliseconds = 30_000) => TimedCancel(milliseconds);
    }
}
