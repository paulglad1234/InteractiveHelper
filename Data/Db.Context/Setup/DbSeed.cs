using InteractiveHelper.Db.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace InteractiveHelper.Db.Context.Setup;

public static class DbSeed
{
    public static void Execute(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.GetService<IServiceScopeFactory>()?.CreateScope();
        ArgumentNullException.ThrowIfNull(scope);

        var factory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<MainDbContext>>();
        using var context = factory.CreateDbContext();

        if (context.Items.Any() || context.Brands.Any() || context.Categories.Any())
            return;

        var brand = new Brand { Name = "MSI" };
        var category = new Category { Title = "GPU" };
        var item = new Item
        {
            Brand = brand,
            Category = category,
            Name = "GPU MSI RTX 3080",
            Description = "no description",
            Price = 100000.00f,
            Image = Array.Empty<byte>()
        };

        context.Brands.Add(brand);
        context.Categories.Add(category);
        context.Items.Add(item);
        context.SaveChanges();
    }
}
