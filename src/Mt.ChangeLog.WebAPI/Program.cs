using NLog;
using NLog.Web;
using System.Diagnostics;
using System.Reflection;

namespace Mt.ChangeLog.WebAPI
{
    /// <summary>
    /// Базовый класс приложения.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// Версия приложения.
        /// </summary>
        public static string CurrentVersion { get; private set; }

        /// <summary>
        /// Наименование приложения и его версия.
        /// </summary>
        public static string AppName { get; private set; }

        /// <summary>
        /// Инициализация статических параметров класса <see cref="Program"/>.
        /// </summary>
        static Program()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
            CurrentVersion = $"v{fvi.FileVersion}";
            AppName = $"Mt-ChangeLog: v{fvi.FileVersion}";
        }

        /// <summary>
        /// Точка входа в приложение.
        /// </summary>
        /// <param name="args">Аргументы запуска приложения.</param>
        public static void Main(string[] args)
        {
            var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();

            try
            {
                logger.Debug($"'{AppName}' - запущено на выполнение...");
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception exception)
            {
                logger.Error(exception, $"'{AppName}' - остановлено из за перехвата не необработанного исключения!");
            }
            finally
            {
                logger.Debug($"'{AppName}' - остановлено.");
                LogManager.Shutdown();
            }
        }

        /// <summary>
        /// Инициализация приложения.
        /// </summary>
        /// <param name="args">Аргументы запуска приложения.</param>
        /// <returns>Строитель приложения.</returns>
        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureLogging((context, logging) =>
                {
                    /*
                    * var nLogSection = context.Configuration.GetSection("NLog"); // пока уберем
                    * LogManager.Configuration = new NLogLoggingConfiguration(nLogSection); // пока уберем
                    */
                    logging.ClearProviders();
                    logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
                    /*
                     * logging.AddNLogWeb(); // пока уберем
                     */
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .UseNLog();
        }
    }
}