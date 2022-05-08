namespace InteractiveHelper.CategoryService;

using Microsoft.Extensions.DependencyInjection;

public static class Bootstrapper
{
    public static IServiceCollection AddCategoryService(this IServiceCollection services)
    {
        services.AddSingleton<ICategoryService, CategoryService>();

        return services;
    }
}
