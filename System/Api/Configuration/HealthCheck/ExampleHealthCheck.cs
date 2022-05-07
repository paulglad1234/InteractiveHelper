namespace InteractiveHelper.Api.Configuration;

using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Reflection;

/// <summary>
/// A basic api health check
/// </summary>
public class ExampleHealthCheck : IHealthCheck
{
    /// <summary>
    /// Asynchronous health check
    /// </summary>
    /// <param name="context"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>Healthy or Unhealthy</returns>
    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default(CancellationToken))
    {
        var assembly = Assembly.Load("InteractiveHelper.Api");
        var versionNumber = assembly.GetName().Version;

        return HealthCheckResult.Healthy(description: $"Build {versionNumber}");
    }
}
