// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using System;

namespace DailyDesktop.Core.Providers
{
    /// <summary>
    /// <see cref="Exception"/> subclass meant for exceptions pertaining to
    /// <see cref="IProvider"/>s.
    /// </summary>
    public class ProviderException : Exception
    {
        /// <summary>
        /// Constructs an empty <see cref="ProviderException"/>.
        /// </summary>
        public ProviderException()
        {
        }

        /// <summary>
        /// Constructs a <see cref="ProviderException"/> with a message.
        /// </summary>
        /// <param name="msg">The exception message.</param>
        public ProviderException(string msg)
            : base(msg)
        {
        }
    }
}
