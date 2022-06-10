namespace InteractiveHelper.CatalogServices;

using InteractiveHelper.CatalogServices.Brands;
using InteractiveHelper.CatalogServices.Categories;
using InteractiveHelper.CatalogServices.Characteristics;
using InteractiveHelper.CatalogServices.Items;
using Microsoft.Extensions.DependencyInjection;

public static class Bootstrapper
{
    public static IServiceCollection AddCatalogServices(this IServiceCollection services)
    {
        services.AddSingleton<IBrandService, BrandService>();
        services.AddSingleton<ICategoryService, CategoryService>();
        services.AddSingleton<ICharacteristicService, CharacteristicService>();
        services.AddSingleton<IItemService, ItemService>();

        return services;
    }
}
