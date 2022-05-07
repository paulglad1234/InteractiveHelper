namespace InteractiveHelper.Api.Configuration;

using Microsoft.AspNetCore.Mvc;

/// <summary>
/// Versioning configuration class
/// </summary>
public static class VersioningConfiguration
{
    /// <summary>
    /// Adds versioning to the service collection
    /// </summary>
    /// <param name="services">Service collection</param>
    public static IServiceCollection AddAppVersions(this IServiceCollection services)
    {
        services.AddApiVersioning(opt =>
        {
            opt.ReportApiVersions = true;
            opt.DefaultApiVersion = new ApiVersion(1, 0);
        });

        services.AddVersionedApiExplorer(opt =>
        {
            opt.GroupNameFormat = "'v'VVV";
            opt.SubstituteApiVersionInUrl = true;
        });

        return services;
    }
}
