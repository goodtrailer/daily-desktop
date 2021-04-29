// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using DailyDesktop.Core.Providers;

namespace DailyDesktop.Desktop
{
    public class ProviderComboboxItem
    {
        public IProvider Provider;

        public ProviderComboboxItem(IProvider provider)
        {
            this.Provider = provider;
        }

        public override string ToString()
        {
            return Provider.DisplayName;
        }
    }
}
