using NLog;
using NLog.Web;
using System.Diagnostics;
using System.Reflection;

namespace Mt.ChangeLog.WebAPI
{
    /// <summary>
    /// ������� ����� ����������.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// ������ ����������.
        /// </summary>
        public static string CurrentVersion { get; private set; }

        /// <summary>
        /// ������������ ���������� � ��� ������.
        /// </summary>
        public static string AppName { get; private set; }

        /// <summary>
        /// ������������� ����������� ���������� ������ <see cref="Program"/>.
        /// </summary>
        static Program()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
            CurrentVersion = $"v{fvi.FileVersion}";
            AppName = $"Mt-ChangeLog: v{fvi.FileVersion}";
        }

        /// <summary>
        /// ����� ����� � ����������.
        /// </summary>
        /// <param name="args">��������� ������� ����������.</param>
        public static void Main(string[] args)
        {
            var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();

            try
            {
                logger.Debug($"'{AppName}' - �������� �� ����������...");
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception exception)
            {
                logger.Error(exception, $"'{AppName}' - ����������� �� �� ��������� �� ��������������� ����������!");
            }
            finally
            {
                logger.Debug($"'{AppName}' - �����������.");
                LogManager.Shutdown();
            }
        }

        /// <summary>
        /// ������������� ����������.
        /// </summary>
        /// <param name="args">��������� ������� ����������.</param>
        /// <returns>��������� ����������.</returns>
        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureLogging((context, logging) =>
                {
                    /*
                    * var nLogSection = context.Configuration.GetSection("NLog"); // ���� ������
                    * LogManager.Configuration = new NLogLoggingConfiguration(nLogSection); // ���� ������
                    */
                    logging.ClearProviders();
                    logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
                    /*
                     * logging.AddNLogWeb(); // ���� ������
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