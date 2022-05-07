using Microsoft.EntityFrameworkCore;

namespace InteractiveHelper.Db.Context.Factories;

public class MainDbContextFactory
{
    private readonly DbContextOptions<MainDbContext> options;

    public MainDbContextFactory(DbContextOptions<MainDbContext> options)
    {
        this.options = options;
    }

    public MainDbContext CreateDbContext()
    {
        return new MainDbContext(options);
    }
}
