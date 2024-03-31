using System.Diagnostics;
using System.Reflection;

using Microsoft.AspNetCore;

using Mt.ChangeLog.DataContext;

using NLog;
using NLog.Web;

namespace Mt.ChangeLog.WebAPI;

/// <summary>
/// Базовый класс приложения.
/// </summary>
public static class Program
{
    /// <summary>
    /// Инициализация статических параметров класса <see cref="Program"/>.
    /// </summary>
    static Program()
    {
        var assembly = Assembly.GetExecutingAssembly();
        CurrentVersion = $"v{FileVersionInfo.GetVersionInfo(assembly.Location).FileVersion}";
    }

    /// <summary>
    /// Версия приложения.
    /// </summary>
    public static string CurrentVersion { get; private set; }

    /// <summary>
    /// Точка входа в приложение.
    /// </summary>
    /// <param name="args">Аргументы запуска приложения.</param>
    public static void Main(string[] args)
    {
        var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
        try
        {
            logger.Debug("Приложение запущено на выполнение...");
            var host = CreateHostBuilder(args).Build();
            using (var scope = host.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<MtContext>();
                if (context.Database.EnsureCreated())
                {
                    context.CreateDefaultEntities();
                    context.CreateViews();
                    context.CreateSqlFunctions();
                    context.SaveChanges();
                }
            }

            host.Run();
        }
        catch (Exception exception)
        {
            logger.Error(exception, "Приложение остановлено из за перехвата не необработанного исключения!");
        }
        finally
        {
            logger.Debug("Приложение остановлено.");
            LogManager.Shutdown();
        }
    }

    /// <summary>
    /// Инициализация приложения.
    /// </summary>
    /// <param name="args">Аргументы запуска приложения.</param>
    /// <returns>Строитель приложения.</returns>
    private static IWebHostBuilder CreateHostBuilder(string[] args)
    {
        return WebHost
            .CreateDefaultBuilder<Startup>(args)
            .ConfigureLogging((_, logging) =>
            {
                /***
                 * var nLogSection = context.Configuration.GetSection("NLog");
                 * LogManager.Configuration = new NLogLoggingConfiguration(nLogSection);
                 */
                logging.ClearProviders();
                logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
                /***
                 * logging.AddNLogWeb();
                 */
            })
            .UseNLog();
    }
}