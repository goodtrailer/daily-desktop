// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

namespace DailyDesktop.Core.Providers
{
    public class ProviderWrapper
    {
        public readonly IProvider Provider;
        public readonly string DllPath;

        public ProviderWrapper(string dllPath, IProvider provider)
        {
            Provider = provider;
            DllPath = dllPath;
        }

        public override string ToString()
        {
            return Provider.DisplayName;
        }
    }
}
