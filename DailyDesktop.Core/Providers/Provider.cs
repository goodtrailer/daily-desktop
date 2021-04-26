namespace DailyDesktop.Core.Providers
{
    public interface IProvider
    {
        string Key { get; }
        string DisplayName { get; }
        string Description { get; }

        string GetImageURL();
    }
}
