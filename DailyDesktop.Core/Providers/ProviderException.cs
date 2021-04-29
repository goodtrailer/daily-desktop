// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using System;

namespace DailyDesktop.Core.Providers
{
    public class ProviderException : Exception
    {
        public ProviderException()
        {
        }

        public ProviderException(string msg)
            : base(msg)
        {
        }
    }
}
