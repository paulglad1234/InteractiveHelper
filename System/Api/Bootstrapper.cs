using InteractiveHelper.BrandService;
using InteractiveHelper.CategoryService;
using InteractiveHelper.ItemService;
using InteractiveHelper.Settings;

namespace InteractiveHelper.Api;

/// <summary>
/// 
/// </summary>
public static class Bootstrapper
{
    /// <summary>
    /// Add business layer services to the service collection
    /// </summary>
    /// <param name="services">Service collection</param>
    /// <returns>Service collection</returns>
    public static IServiceCollection AddAppServices(this IServiceCollection services)
    {
        services
            .AddSettings()
            .AddItemService()
            .AddBrandService()
            .AddCategoryService();

        return services;
    }
}
