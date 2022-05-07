namespace InteractiveHelper.Api.Configuration;

/// <summary>
/// CORS configuration class
/// </summary>
public static class CorsConfiguration
{
    /// <summary>
    /// Adds CORS to the service collection
    /// </summary>
    /// <param name="services">App service collection</param>
    public static IServiceCollection AddAppCors(this IServiceCollection services)
    {
        return services.AddCors(builder =>
        {
            builder.AddDefaultPolicy(pol =>
            {
                pol.AllowAnyHeader();
                pol.AllowAnyMethod();
                pol.AllowAnyOrigin();
            });
        });
    }

    /// <summary>
    /// Use CORS service
    /// </summary>
    /// <param name="app">Application</param>
    public static IApplicationBuilder UseAppCors(this IApplicationBuilder app)
    {
        return app.UseCors();
    }
}
