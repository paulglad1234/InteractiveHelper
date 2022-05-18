namespace InteractiveHelper.Api.Configuration;

using InteractiveHelper.Db.Context;
using InteractiveHelper.Db.Entities.User;
using InteractiveHelper.Settings;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using InteractiveHelper.Common.Security;

/// <summary>
/// Authentication and authorization configuration class
/// </summary>
public static class AuthConfiguration
{
    /// <summary>
    /// Configure authentication and authorization and add them to the service collection
    /// </summary>
    /// <param name="services">Service collection</param>
    /// <param name="settings">Settings</param>
    /// <returns></returns>
    public static IServiceCollection AddAppAuth(this IServiceCollection services, IApiSettings settings)
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

        services.AddAuthentication(options =>
        {
            options.DefaultScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
            options.DefaultAuthenticateScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
        })
            .AddJwtBearer(IdentityServerAuthenticationDefaults.AuthenticationScheme, options =>
            {
                options.RequireHttpsMetadata = settings.IdentityServer.RequireHttps;
                options.Authority = settings.IdentityServer.Url;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = false,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    RequireExpirationTime = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
                options.Audience = "api";
            });


        services.AddAuthorization(options =>
        {
            options.AddPolicy(AppScopes.AdminCatalog, policy => policy.RequireClaim("scope", AppScopes.AdminCatalog));
            options.AddPolicy(AppScopes.AdminQuiz, policy => policy.RequireClaim("scope", AppScopes.AdminQuiz));
            options.AddPolicy(AppScopes.SupportOrders, policy => policy.RequireClaim("scope", AppScopes.SupportOrders));
            options.AddPolicy(AppScopes.AuthenticatedUser, policy => policy.RequireClaim("scope", AppScopes.AuthenticatedUser));
        });

        return services;
    }

    /// <summary>
    /// Enable authentication and authorization
    /// </summary>
    public static IApplicationBuilder UseAppAuth(this IApplicationBuilder app)
    {
        app.UseAuthentication();

        app.UseAuthorization();

        return app;
    }
}