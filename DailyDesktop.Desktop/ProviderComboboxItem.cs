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
