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
    private readonly ILogger<NpgsqlConnectionFactory> _logger;

    /// <summary>
    /// Строка подключения к БД.
    /// </summary>
    private readonly string _connectionString;

    /// <summary>
    /// Инициализация экземпляра класса <see cref="NpgsqlConnectionFactory"/>.
    /// </summary>
    /// <param name="logger">Журнал логирования.</param>
    /// <param name="configuration">Конфигурация приложения.</param>
    public NpgsqlConnectionFactory(ILogger<NpgsqlConnectionFactory> logger, IConfiguration configuration)
    {
        _logger = logger;
        _connectionString = configuration.GetConnectionString("NpgSqlDb")!;
    }

    /// <summary>
    /// Создать подключение.
    /// </summary>
    /// <returns>Экземпляр объекта.</returns>
    public IDbConnection CreateConnection()
    {
        _logger.LogDebug("Создание нового подключения к базе данных PostgreSQL.");
        return new NpgsqlConnection(_connectionString);
    }
}