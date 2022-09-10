using Dapper;
using Mt.ChangeLog.DataAccess.Abstractions;
using Mt.ChangeLog.TransferObjects.AnalogModule;
using Mt.ChangeLog.TransferObjects.Platform;
using Mt.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Mt.ChangeLog.DataAccess
{
    /// <summary>
    /// Репозиторий для работы с аналоговоыми модулями.
    /// </summary>
    internal sealed class AnalogModuleRepository : IAnalogModuleRepository
    {
        /// <summary>
        /// Подключение к базе данных.
        /// </summary>
        private readonly IDbConnection connection;

        /// <summary>
        /// Инициализация экземпляра класса <see cref="AnalogModuleRepository"/>.
        /// </summary>
        /// <param name="connection">Подключение к базе данных.</param>
        /// <exception cref="ArgumentNullException">Срабатывает если подключение к базе данных равно null.</exception>
        public AnalogModuleRepository(IDbConnection connection)
        {
            this.connection = Check.NotNull(connection, nameof(connection));
        }

        /// <inheritdoc />
        public async Task<AnalogModuleModel> GetEntityAsync(Guid guid)
        {
            var qSql = $@"SELECT * FROM ""MT"".""ConcretAnalogModule""(@guid);
                          SELECT * FROM ""MT"".""AnalogModulePlatforms""(@guid);";
            var qMultiple = await this.connection.QueryMultipleAsync(qSql, new { guid });
            var module = await qMultiple.ReadSingleAsync<AnalogModuleModel>();
            module.Platforms = await qMultiple.ReadAsync<PlatformShortModel>();

            return module;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<AnalogModuleShortModel>> GetShortEntitiesAsync()
        {
            var qSql = @$"SELECT * FROM ""MT"".""AnalogModuleShorts""();";
            var result = await this.connection.QueryAsync<AnalogModuleShortModel>(qSql);

            return result;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<AnalogModuleTableModel>> GetTableEntitiesAsync()
        {
            var qSql = $@"SELECT * FROM ""MT"".""AnalogModulesForTable""();";
            var result = await this.connection.QueryAsync<AnalogModuleTableModel>(qSql);

            return result;
        }
    }
}