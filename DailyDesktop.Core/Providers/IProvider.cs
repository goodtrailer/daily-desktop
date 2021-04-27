namespace DailyDesktop.Core.Providers
{
    public interface IProvider
    {
        /// <summary>
        /// Any length string that <i>cannot</i> contain whitespace that serves as a unique identifier.
        /// </summary>
        string Key { get; }

        /// <summary>
        /// The name that will appear in the Provider combobox.
        /// </summary>
        string DisplayName { get; }

        /// <summary>
        /// The description that will appear in the Provider description box.
        /// </summary>
        string Description { get; }

        /// <summary>
        /// Gets the image URI to download from and set as the desktop wallpaper.
        /// </summary>
        /// <returns>The image URI to download from.</returns>
        string GetImageUri();
    }
}
