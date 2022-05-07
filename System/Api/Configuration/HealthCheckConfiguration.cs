namespace InteractiveHelper.Api.Configuration;

using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Text.Json;

/// <summary>
/// Health check service registration class
/// </summary>
public static class HealthCheckConfiguration
{
    /// <summary>
    /// Adds health check service to the service collection
    /// </summary>
    /// <param name="services">App service collection</param>
    public static IServiceCollection AddAppHealthChecks(this IServiceCollection services)
    {
        services.AddHealthChecks()
            .AddCheck<ExampleHealthCheck>("Api");

        return services;
    }

    /// <summary>
    /// Use health check service
    /// </summary>
    /// <param name="app">Application</param>
    public static IApplicationBuilder UseAppHealthChecks(this WebApplication app)
    {
        app.MapHealthChecks("/health");

        app.MapHealthChecks("/health/detail", new HealthCheckOptions()
        {
            ResponseWriter = WriteHealthCheckResponse,
            AllowCachingResponses = false,
        });

        return app;
    }

    private static async Task WriteHealthCheckResponse(HttpContext httpContext, HealthReport report)
    {
        httpContext.Response.ContentType = "application/json";
        var response = new HealthCheckResponse()
        {
            OverallStatus = report.Status.ToString(),
            TotalDuration = report.TotalDuration.TotalSeconds.ToString("0:0.00"),
            HealthChecks = report.Entries.Select(x => new HealthCheck
            {
                Status = x.Value.Status.ToString(),
                Component = x.Key,
                Description = x.Value.Description == null ? "" : x.Value.Description,
                Duration = x.Value.Duration.TotalSeconds.ToString("0:0.00")
            }),

        };

        await httpContext.Response.WriteAsync(text: JsonSerializer.Serialize(response, new JsonSerializerOptions { WriteIndented = true }));
    }

    private class HealthCheck
    {
        public string Status { get; set; } = string.Empty;
        public string Component { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Duration { get; set; } = string.Empty;
    }

    private class HealthCheckResponse
    {
        public string OverallStatus { get; set; } = string.Empty;
        public IEnumerable<HealthCheck>? HealthChecks { get; set; }
        public string TotalDuration { get; set; } = string.Empty;
    }
}
