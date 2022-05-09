namespace InteractiveHelper.Db.Context.Factories;

using InteractiveHelper.Db.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<MainDbContext>
{
    public MainDbContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
             .AddJsonFile("appsettings.design.json")
             .Build();

        var options = new DbContextOptionsBuilder<MainDbContext>()
                      .UseLazyLoadingProxies()
                      .UseSqlServer(configuration.GetConnectionString("MainDbContext"), opts =>
                      {
                          //opts.CommandTimeout((int)TimeSpan.FromMinutes(10).TotalSeconds)
                      }).Options;

        return new MainDbContextFactory(options).CreateDbContext();
    }
}
