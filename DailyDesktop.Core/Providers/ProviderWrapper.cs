﻿// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

namespace DailyDesktop.Core.Providers
{
    /// <summary>
    /// Wrapper for an <see cref="IProvider"/> that additionally stores
    /// <see cref="Dll"/> and implements <see cref="ToString"/>.
    /// </summary>
    public class ProviderWrapper
    {
        /// <summary>
        /// A wrapper around a null <see cref="IProvider"/>.
        /// </summary>
        public static readonly ProviderWrapper Null = new ProviderWrapper("", null);

        /// <summary>
        /// The wrapped <see cref="IProvider"/>.
        /// </summary>
        public readonly IProvider? Provider;

        /// <summary>
        /// The path of the DLL module containing the <see cref="IProvider"/>
        /// implementation for <see cref="Provider"/>.
        /// </summary>
        public readonly string Dll;

        /// <summary>
        /// Constructs a <see cref="ProviderWrapper"/>.
        /// </summary>
        /// <param name="dll">The path of the DLL module containing the
        /// <see cref="IProvider"/> implementation.</param>
        /// <param name="provider">An instance of the <see cref="IProvider"/>
        /// implementation found in the DLL module.</param>
        public ProviderWrapper(string dll, IProvider? provider)
        {
            Provider = provider;
            Dll = dll;
        }

        /// <summary>
        /// Returns the <see cref="IProvider.DisplayName"/> of
        /// <see cref="Provider"/>.
        /// </summary>
        /// <returns><see cref="Provider"/>'s <see cref="IProvider.DisplayName"/>
        /// member.</returns>
        public override string ToString() => Provider?.DisplayName ?? "";
    }
}
