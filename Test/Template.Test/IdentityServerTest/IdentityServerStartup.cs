namespace InteractiveHelper.Test.ApiServerTest;

using InteractiveHelper.Identity.Configuration;
using InteractiveHelper.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

public class IdentityServerStartup
{
    private readonly IConfiguration configuration;
    private readonly Action<IServiceCollection>? configurator;

    public IdentityServerStartup(
        IConfiguration configuration, Action<IServiceCollection>? configurator = null
    )
    {
        this.configuration = configuration;
        this.configurator = configurator;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        var settings = new IS4Settings(new SettingsSource(configuration));

        services.AddAppCors();
        services.AddAppDbContext(settings.Db);
        services.AddRouting();
        services.AddHttpContextAccessor();
        services.AddSettings();
        services.AddIS4();

        configurator?.Invoke(services); // kinda same as builder.Build(). All services are actually registered only here
    }

    public void Configure(IApplicationBuilder app)
    {
        app.UseAppCors();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseAppDbContext();
        app.UseIS4();
    }
}

