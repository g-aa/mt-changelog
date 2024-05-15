using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;

using Mt.ChangeLog.WebAPI.Infrastructure;

using System.Net;

namespace Mt.ChangeLog.WebAPI.Test.Infrastructure;

/// <summary>
/// Набор тестов для <see cref="DiagnosticApplicationBuilderExtensions"/>.
/// </summary>
[TestFixture]
public class DiagnosticApplicationBuilderExtensionsTest
{
    private static readonly List<Dictionary<string, HealthReportEntry>> Entries = new()
    {
        new Dictionary<string, HealthReportEntry>(),
        new Dictionary<string, HealthReportEntry>
        {
            ["test"] = new(
                HealthStatus.Healthy,
                "description",
                1.Seconds(),
                null,
                new Dictionary<string, object>()),
        },
        new Dictionary<string, HealthReportEntry>
        {
            ["test"] = new(
                HealthStatus.Healthy,
                "description",
                1.Seconds(),
                null,
                new Dictionary<string, object>
                {
                    { "param.1", "1111" },
                    { "param.2", "2222" },
                }),
        },
    };

    /// <summary>
    /// Положительный тест для <see cref="DiagnosticApplicationBuilderExtensions.UseDiagnostics(IApplicationBuilder)"/>.
    /// </summary>
    /// <param name="uri">Адреса health check.</param>
    [TestCase("/health/live")]
    [TestCase("/health/ready")]
    [TestCase("/health/status")]
    public async Task UseDiagnosticsPositiveTest(Uri uri)
    {
        // arrange
        using var host = await new HostBuilder()
            .ConfigureWebHost(
                webBuilder => webBuilder
                    .UseTestServer()
                    .ConfigureServices(
                        services => services
                            .AddHealthChecks()
                            .AddGcInfoCheck())
                    .Configure(
                        builder => builder.UseDiagnostics()))
            .StartAsync();

        // act
        var response = await host.GetTestClient().GetAsync(uri);

        // assert
        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    /// <summary>
    /// Положительный тест для <see cref="DiagnosticApplicationBuilderExtensions.HealthReportFormatter(HealthReport)"/>.
    /// </summary>
    /// <param name="entries">Набор записей для отчета.</param>
    [TestCaseSource(nameof(Entries))]
    public void HealthReportFormatterPositiveTest(Dictionary<string, HealthReportEntry> entries)
    {
        // arrange
        var report = new HealthReport(entries, 5.Seconds());

        // act
        var result = DiagnosticApplicationBuilderExtensions.HealthReportFormatter(report);

        // assert
        result.Should().NotBeNullOrEmpty();
    }

    /// <summary>
    /// Положительный тест для <see cref="DiagnosticApplicationBuilderExtensions.HealthCheckResponseWriterAsync(HttpContext, HealthReport)"/>.
    /// </summary>
    /// <param name="entries">Набор записей для отчета.</param>
    /// <returns>Результат выполнения асинхронной задачи.</returns>
    [TestCaseSource(nameof(Entries))]
    public async Task HealthCheckResponseWriterAsyncPositiveTest(Dictionary<string, HealthReportEntry> entries)
    {
        // arrange
        var report = new HealthReport(entries, 5.Seconds());
        var context = new DefaultHttpContext();

        // act
        var func = () => DiagnosticApplicationBuilderExtensions.HealthCheckResponseWriterAsync(context, report);

        // assert
        await func.Should().NotThrowAsync();
    }
}