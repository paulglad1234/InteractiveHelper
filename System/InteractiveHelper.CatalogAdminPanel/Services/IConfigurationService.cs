namespace InteractiveHelper.CatalogAdminPanel;

public interface IConfigurationService
{
    Task<bool> GetDarkMode();
    Task SetDarkMode(bool value);

    Task<bool> GetNavigationMenuVisible();
    Task SetNavigationMenuVisible(bool value);
}
