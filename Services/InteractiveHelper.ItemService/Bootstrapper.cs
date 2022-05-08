namespace InteractiveHelper.ItemService;

using Microsoft.Extensions.DependencyInjection;

public static class Bootstrapper
{
    public static IServiceCollection AddItemService(this IServiceCollection services)
    {
        services.AddSingleton<IItemService, ItemService>();

        return services;
    }
}
