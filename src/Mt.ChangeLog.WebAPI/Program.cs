using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
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
            AppName = $"MT ChangeLog: v{fvi.FileVersion}";
        }

        /// <summary>
        /// ����� ����� � ����������.
        /// </summary>
        /// <param name="args">��������� ������� ����������.</param>
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// ������������� ����������.
        /// </summary>
        /// <param name="args">��������� ������� ����������.</param>
        /// <returns>��������� ����������.</returns>
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