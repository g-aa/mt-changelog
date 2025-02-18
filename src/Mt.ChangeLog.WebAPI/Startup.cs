using System.Reflection;

using Mt.ChangeLog.DataAccess;
using Mt.ChangeLog.DataContext;
using Mt.ChangeLog.Entities;
using Mt.ChangeLog.Logic;
using Mt.ChangeLog.TransferObjects;
using Mt.ChangeLog.WebAPI.Infrastructure;

using OpenTelemetry.Exporter;
using OpenTelemetry.Metrics;

namespace Mt.ChangeLog.WebAPI;

/// <summary>
/// Класс инициализирующий приложение.
/// </summary>
public sealed class Startup
{
    /// <summary>
    /// Инициализация экземпляра класса <see cref="Startup"/>.
    /// </summary>
    /// <param name="configuration">Набор свойств конфигурации приложения.</param>
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
        Assemblies = new[]
        {
            typeof(DataAccessLayer).Assembly,
            typeof(DataContextLayer).Assembly,
            typeof(EntityLayer).Assembly,
            typeof(LogicLayer).Assembly,
            typeof(ModelLayer).Assembly,
            typeof(ServiceLayer).Assembly,
        };
    }

    /// <inheritdoc cref="IConfiguration"/>
    public IConfiguration Configuration { get; init; }

    /// <summary>
    /// Перечень сборок проекта.
    /// </summary>
    public IReadOnlyCollection<Assembly> Assemblies { get; init; }

    /// <summary>
    /// Метод конфигурации сервисов приложения.
    /// </summary>
    /// <param name="services">Коллекция сервисов.</param>
    public void ConfigureServices(IServiceCollection services)
    {
        services
            .AddApplicationContext()
            .AddDataAccess()
            .AddLogic(Assemblies)
            .AddScoped<IMtUser, MtUser>()
            .AddApiVersioningSupport()
            .AddSwaggerDocumentation(Assemblies)
            .AddControllers(options => options.Filters.Add<ApiExceptionFilter>());

        services
            .AddHealthChecks()
            .AddDbContextCheck<MtContext>();

        services
            .AddOpenTelemetry()
            .WithMetrics(metrics => metrics
                .AddRuntimeInstrumentation()
                .AddAspNetCoreInstrumentation()
                .AddPrometheusExporter());

        services
            .Configure<PrometheusAspNetCoreOptions>(options => options.DisableTotalNameSuffixForCounters = true);
    }

    /// <summary>
    /// Метод настройки конвейера HTTP-запросов.
    /// </summary>
    /// <param name="builder">Строитель приложения.</param>
#pragma warning disable S2325 // Methods and properties that don't access instance data should be static
    public void Configure(IApplicationBuilder builder)
    {
        builder
            .UseDiagnostics()
            .UseRouting()
            .UseAuthorization()
            .UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapGet("/", context =>
                {
                    context.Response.Redirect("/swagger");
                    return Task.CompletedTask;
                });
            })
            .UseSwaggerDocumentation();
    }
}