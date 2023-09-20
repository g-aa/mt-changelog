using System.Data;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Npgsql;

namespace Mt.ChangeLog.DataAccess;

/// <summary>
/// Фабрика по созданию подключения к БД.
/// </summary>
public sealed class NpgsqlConnectionFactory
{
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
        this.connectionString = configuration.GetConnectionString("NpgSqlDb")!;
    }

    /// <summary>
    /// Создать подключение.
    /// </summary>
    /// <returns>Экземпляр объекта.</returns>
    public IDbConnection CreateConnection()
    {
        this.logger.LogDebug("Создание нового подключения к базе данных PostgreSQL.");
        return new NpgsqlConnection(this.connectionString);
    }
}