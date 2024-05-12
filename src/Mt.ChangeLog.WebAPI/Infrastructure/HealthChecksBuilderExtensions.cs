using System.Diagnostics.CodeAnalysis;

using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Mt.ChangeLog.WebAPI.Infrastructure;

/// <summary>
/// Методы расширения для <see cref="IHealthChecksBuilder"/>.
/// </summary>
[ExcludeFromCodeCoverage]
public static class HealthChecksBuilderExtensions
{
    /// <summary>
    /// dfgjhlkdfgh.
    /// </summary>
    /// <param name="builder">dgfdg.</param>
    /// <param name="name">dfgdg.</param>
    /// <param name="failureStatus">dgdfg.</param>
    /// <param name="tags">sdfsdfsdf.</param>
    /// <returns>dgfgdfg.</returns>
    public static IHealthChecksBuilder AddGcInfoCheck(
        this IHealthChecksBuilder builder,
        string name = GcInfoHealthCheck.Name,
        HealthStatus? failureStatus = null,
        IEnumerable<string>? tags = null)
    {
        return builder
            .AddCheck<GcInfoHealthCheck>(name, failureStatus ?? HealthStatus.Degraded, tags ?? Enumerable.Empty<string>());
    }
}