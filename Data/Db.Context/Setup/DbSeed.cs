using InteractiveHelper.Db.Entities.Catalog;
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

        //context.ItemCharacteristics.RemoveRange(context.ItemCharacteristics);
        //context.Items.RemoveRange(context.Items);
        //context.Characteristics.RemoveRange(context.Characteristics);
        //context.Brands.RemoveRange(context.Brands);
        //context.Categories.RemoveRange(context.Categories);

        if (context.Brands.Any() || context.Categories.Any() || context.Items.Any())
            return;

        var brands = new List<Brand> { 
            new Brand { Name = "AMD" },
            new Brand { Name = "Intel"}
        };
        var category = new Category { Title = "CPU" };

        var item1 = new Item
        {
            Brand = brands[0],
            Category = category,
            Name = "AMD Ryzen 9 5900x",
            Description = "no description",
            Price = 44000.00f,
            Image = Array.Empty<byte>()
        };
        var item2 = new Item
        {
            Brand = brands[1],
            Category = category,
            Name = "Intel Core i9-12900k",
            Description = "no description",
            Price = 60000.00f,
            Image = Array.Empty<byte>()
        };

        var characteristics = new List<Characteristic>
        {
            new Characteristic { Name = "Frequency" },
            new Characteristic { Name = "Cores" },
            new Characteristic { Name = "Threads" }
        };
        characteristics.ForEach(x => x.Category = category);

        var ics = new List<ItemCharacteristic>
        {
            new ItemCharacteristic { Characteristic = characteristics[0], Item = item1, Value = "3.7 GHz" },
            new ItemCharacteristic { Characteristic = characteristics[1], Item = item1, Value = "12" },
            new ItemCharacteristic { Characteristic = characteristics[2], Item = item1, Value = "24" },
            new ItemCharacteristic { Characteristic = characteristics[0], Item = item2, Value = "3.7 GHz" },
            new ItemCharacteristic { Characteristic = characteristics[1], Item = item2, Value = "16" },
            new ItemCharacteristic { Characteristic = characteristics[2], Item = item2, Value = "24" },
        };

        context.Brands.AddRange(brands);
        context.Categories.AddRange(category);
        context.Items.AddRange(item1, item2);
        context.Characteristics.AddRange(characteristics);
        context.ItemCharacteristics.AddRange(ics);
        context.SaveChanges();
    }
}
