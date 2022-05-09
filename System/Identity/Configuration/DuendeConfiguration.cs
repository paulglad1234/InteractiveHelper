namespace InteractiveHelper.Identity.Configuration;

using InteractiveHelper.Db.Context;
using InteractiveHelper.Db.Entities;
using InteractiveHelper.Identity.Configuration.IS4;
using Microsoft.AspNetCore.Identity;
using System.Security.Cryptography.X509Certificates;

public static class DuendeConfiguration
{
    public static IServiceCollection AddDuende(this IServiceCollection services)
    {
        services
            .AddIdentity<User, IdentityRole<Guid>>(opt =>
            {
                opt.Password.RequiredLength = 0;
                opt.Password.RequireDigit = false;
                opt.Password.RequireLowercase = false;
                opt.Password.RequireUppercase = false;
                opt.Password.RequireNonAlphanumeric = false;
            })
            .AddEntityFrameworkStores<MainDbContext>()
            .AddUserManager<UserManager<User>>()
            .AddDefaultTokenProviders();

        services
            .AddIdentityServer()
            .AddAspNetIdentity<User>()
            .AddInMemoryApiScopes(AppApiScopes.ApiScopes)
            .AddInMemoryClients(AppClients.Clients)
            .AddInMemoryApiResources(AppResources.Resources)
            .AddInMemoryIdentityResources(AppIdentityResources.Resources)

            .AddDeveloperSigningCredential();
            //.AddSigningCredential(new X509Certificate2(
            //  Configuration["Jwt:Secret"],
            //  Configuration["Jwt:Certificate"],
            //  X509KeyStorageFlags.MachineKeySet |
            //  X509KeyStorageFlags.PersistKeySet |
            //  X509KeyStorageFlags.Exportable
            //));

        return services;
    }

    public static IApplicationBuilder UseIS4(this IApplicationBuilder app)
    {
        app.UseIdentityServer();

        return app;
    }
}
