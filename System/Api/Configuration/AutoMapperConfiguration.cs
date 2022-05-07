namespace InteractiveHelper.Api.Configuration;

using InteractiveHelper.Common.Helpers;

/// <summary>
/// AutoMapper configuration class
/// </summary>
public static class AutoMapperConfiguration
{
    /// <summary>
    /// Add Automapper to the service collection
    /// </summary>
    /// <param name="services">Service collection</param>
    /// <returns>Service collection</returns>
    public static IServiceCollection AddAppAutoMapper(this IServiceCollection services)
    {
        AutoMapperRegisterHelper.Register(services);

        return services;
    }
}