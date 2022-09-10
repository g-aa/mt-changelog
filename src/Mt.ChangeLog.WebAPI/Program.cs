using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
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
            AppName = $"MT ChangeLog: v{fvi.FileVersion}";
        }

        /// <summary>
        /// Точка входа в приложение.
        /// </summary>
        /// <param name="args">Аргументы запуска приложения.</param>
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// Инициализация приложения.
        /// </summary>
        /// <param name="args">Аргументы запуска приложения.</param>
        /// <returns>Строитель приложения.</returns>
        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureLogging(logging =>
                {
                    logging.AddConsole();
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
        }
    }
}