using InteractiveHelper.Db.Context;
using InteractiveHelper.Db.Context.Factories;
using InteractiveHelper.Db.Context.Setup;
using InteractiveHelper.Settings;

namespace InteractiveHelper.Api.Configuration;

/// <summary>
/// Database configuration class
/// </summary>
public static class DbConfiguration
{
    /// <summary>
    /// Add DB context factory to the service colection
    /// </summary>
    /// <param name="services">Service collection</param>
    /// <param name="settings">App settings</param>
    public static IServiceCollection AddAppDbContext(this IServiceCollection services, IApiSettings settings)
    {
        var dbOptionsDelegate = DbContextOptionsFactory.Configure(settings.Db.ConnectionString);

        return services.AddDbContextFactory<MainDbContext>(dbOptionsDelegate, ServiceLifetime.Singleton);
    }

    /// <summary>
    /// Use database in the app
    /// </summary>
    /// <param name="app">Application</param>
    public static IApplicationBuilder UseAppDbContext(this IApplicationBuilder app)
    {
        DbInit.Execute(app.ApplicationServices);
        DbSeed.Execute(app.ApplicationServices);

        return app;
    }
}
