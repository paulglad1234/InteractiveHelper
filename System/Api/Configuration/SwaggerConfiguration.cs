namespace InteractiveHelper.Api.Configuration;

using IdentityServer4.AccessTokenValidation;
using InteractiveHelper.Common.Security;
using InteractiveHelper.Settings;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;

/// <summary>
/// OpenAPI configuration class
/// </summary>
public static class SwaggerConfiguration
{
    private static string AppTitle = "InteractiveHelper";

    /// <summary>
    /// Enables OpenAPI for the project
    /// </summary>
    /// <param name="services">Service collection</param>
    /// <param name="settings">API settings provider</param>
    public static IServiceCollection AddAppSwagger(this IServiceCollection services, IApiSettings settings)
    {
        if (!settings.General.SwaggerVisible)
            return services;

        services.AddOptions<SwaggerGenOptions>()
            .Configure<IApiVersionDescriptionProvider>((options, provider) =>
            {
                foreach (var avd in provider.ApiVersionDescriptions)
                {
                    options.SwaggerDoc(avd.GroupName, new OpenApiInfo
                    {
                        Version = avd.GroupName,
                        Title = $"{AppTitle}"
                    });
                }
            });

        return services.AddSwaggerGen(options =>
        {
            options.SupportNonNullableReferenceTypes();

            options.UseInlineDefinitionsForEnums();

            options.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());

            options.OperationFilter<SwaggerDefaultValues>();

            var xmlFile = "api.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            options.IncludeXmlComments(xmlPath);

            options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
            {
                Name = IdentityServerAuthenticationDefaults.AuthenticationScheme,
                Type = SecuritySchemeType.OAuth2,
                Scheme = "oauth2",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Flows = new OpenApiOAuthFlows
                {
                    ClientCredentials = new OpenApiOAuthFlow
                    {
                        TokenUrl = new Uri($"{settings.IdentityServer.Url}/connect/token"),
                        Scopes = new Dictionary<string, string>
                        {
                            { AppScopes.AdminCatalog, "AdminCatalog" },
                            { AppScopes.AdminQuiz, "AdminQuiz" },
                            { AppScopes.SupportOrders, "SupportOrders" },
                            { AppScopes.AuthenticatedUser, "AuthenticatedUser" }
                        }
                    },
                    Password = new OpenApiOAuthFlow
                    {
                        TokenUrl = new Uri($"{settings.IdentityServer.Url}/connect/token"),
                        Scopes = new Dictionary<string, string>
                        {
                            { AppScopes.AdminCatalog, "AdminCatalog" },
                            { AppScopes.AdminQuiz, "AdminQuiz" },
                            { AppScopes.SupportOrders, "SupportOrders" },
                            { AppScopes.AuthenticatedUser, "AuthenticatedUser" }
                        }
                    }
                }
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "oauth2"
                            }
                        },
                        new List<string>()
                    }
                });
        });
    }

    /// <summary>
    /// Enables OpenAPI for the app
    /// </summary>
    /// <param name="app">Applocation</param>
    public static IApplicationBuilder UseAppSwagger(this WebApplication app)
    {
        var settings = app.Services.GetService<IApiSettings>();
        var provider = app.Services.GetService<IApiVersionDescriptionProvider>();

        if (!settings.General.SwaggerVisible)
            return app;

        return app.UseSwagger(options =>
        {
            options.RouteTemplate = "api/{documentname}/api.yaml";
        })
            .UseSwaggerUI(
            options =>
            {
                options.RoutePrefix = "swagger";
                provider.ApiVersionDescriptions.ToList().ForEach(
                    description => options.SwaggerEndpoint($"/api/{description.GroupName}/api.yaml", description.GroupName.ToUpperInvariant())
                );

                options.DocExpansion(DocExpansion.List);
                options.DefaultModelsExpandDepth(-1);
                options.OAuthAppName(AppTitle);

                options.OAuthClientId(settings.IdentityServer.ClientId);
                options.OAuthClientSecret(settings.IdentityServer.ClientSecret);
            }
        );
    }

    private class SwaggerDefaultValues : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var apiDescription = context.ApiDescription;

            operation.Deprecated |= apiDescription.IsDeprecated();

            if (operation.Parameters == null)
                return;

            foreach (var parameter in operation.Parameters)
            {
                var description = apiDescription.ParameterDescriptions.First(p => p.Name == parameter.Name);

                parameter.Description ??= description.ModelMetadata?.Description;

                if (parameter.Schema.Default == null && description.DefaultValue != null)
                    parameter.Schema.Default = new OpenApiString(description.DefaultValue.ToString());

                parameter.Required |= description.IsRequired;
            }
        }
    }
}
