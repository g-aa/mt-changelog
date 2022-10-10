using Microsoft.Extensions.Logging;
using Mt.Utilities;
using System;
using System.Data;

namespace Mt.ChangeLog.DataAccess.Abstractions
{
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
        /// Журнал логирования.
        /// </summary>
        protected readonly ILogger logger;

        /// <summary>
        /// Подключение к базе данных.
        /// </summary>
        protected readonly IDbConnection connection;

        /// <summary>
        /// Инициализация экземпляра класса <see cref="AbstractRepository"/>.
        /// </summary>
        /// <param name="logger">Журнал логирования.</param>
        /// <param name="connection">Подключение к базе данных.</param>
        /// <exception cref="ArgumentNullException">Срабатывает если подключение к базе данных равно null.</exception>
        protected AbstractRepository(ILogger logger, IDbConnection connection)
        {
            this.logger = Check.NotNull(logger, nameof(logger));
            this.connection = Check.NotNull(connection, nameof(connection));
        }
    }
}