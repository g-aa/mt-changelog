using System.Data;

using Microsoft.Extensions.Logging;

namespace Mt.ChangeLog.DataAccess.Abstraction;

/// <summary>
/// Абстрактный репозиторий.
/// </summary>
public abstract class AbstractRepository
{
    /// <summary>
    /// Схема базы данных: ChangeLog.
    /// </summary>
    public const string Schema = "changelog";

    /// <summary>
    /// Инициализация экземпляра класса <see cref="AbstractRepository"/>.
    /// </summary>
    /// <param name="logger">Журнал логирования.</param>
    /// <param name="connection">Подключение к базе данных.</param>
    /// <exception cref="ArgumentNullException">Срабатывает если подключение к базе данных равно null.</exception>
    protected AbstractRepository(ILogger logger, IDbConnection connection)
    {
        this.Logger = logger;
        this.Connection = connection;
    }

    /// <summary>
    /// Журнал логирования.
    /// </summary>
    protected ILogger Logger { get; init; }

    /// <summary>
    /// Подключение к базе данных.
    /// </summary>
    protected IDbConnection Connection { get; init; }
}