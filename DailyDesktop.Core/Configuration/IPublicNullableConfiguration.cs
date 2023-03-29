// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

namespace DailyDesktop.Core.Configuration
{
    /// <summary>
    /// A public read-write interface to a configuration that contains
    /// nullable <see cref="string"/> properties.
    /// </summary>
    public interface IPublicNullableConfiguration
    {
        /// <summary>
        /// Nullifies all <see cref="string"/> properties that satisfy
        /// <see cref="string.IsNullOrWhiteSpace(string?)"/>.
        /// </summary>
        /// <returns>The number of properties that were nullified.</returns>
        int NullifyWhitespace();
    }
}
