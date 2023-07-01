using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Mt.Utilities;
using Npgsql;
using System.Data;

namespace Mt.ChangeLog.DataAccess
{
    /// <summary>
    /// Фабрика по созданию подключения к БД.
    /// </summary>
    internal sealed class NpgsqlConnectionFactory
    {
        /// <summary>
        /// Ключ.
        /// </summary>
        private const string key = "ConnectionStrings:NpgSqlDb";

        /// <summary>
        /// Журнал логирования.
        /// </summary>
        private readonly ILogger<NpgsqlConnectionFactory> logger;

        /// <summary>
        /// Строка подключения к БД.
        /// </summary>
        private readonly string connectionString;

        /// <summary>
        /// Инициализация экземпляра класса <see cref="NpgsqlConnectionFactory"/>.
        /// </summary>
        /// <param name="logger">Журнал логирования.</param>
        /// <param name="configuration">Конфигурация приложения.</param>
        public NpgsqlConnectionFactory(ILogger<NpgsqlConnectionFactory> logger, IConfiguration configuration)
        {
            this.logger = logger;
            this.connectionString = Check.NotNull(configuration[key], $"В файле 'appsettings.json' в разделе '{key}' не указана строка подключения к БД.");
        }

        /// <summary>
        /// Создать подключение.
        /// </summary>
        /// <returns>Экземпляр обьекта.</returns>
        public IDbConnection CreateConnection()
        {
            this.logger.LogDebug("Создание нового подключения к базе данных PostgreSQL.");
            return new NpgsqlConnection(this.connectionString);
        }
    }
}