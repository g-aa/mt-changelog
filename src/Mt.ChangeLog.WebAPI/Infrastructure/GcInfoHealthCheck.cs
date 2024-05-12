using System.Diagnostics.CodeAnalysis;

using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Mt.ChangeLog.WebAPI.Infrastructure;

/// <summary>
/// GC information health check.
/// </summary>
[ExcludeFromCodeCoverage]
public sealed class GcInfoHealthCheck() : IHealthCheck
{
    /// <summary>
    /// Наименование.
    /// </summary>
    public const string Name = "GcCheck";

    /// <inheritdoc />
    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        var data = new Dictionary<string, object>
        {
            { "AllocatedBytes", GC.GetTotalMemory(forceFullCollection: false) },
        };

        return Task.FromResult(new HealthCheckResult(HealthStatus.Healthy, data: data));
    }
}