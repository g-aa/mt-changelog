using Dapper;
using Microsoft.Extensions.Logging;
using Mt.ChangeLog.DataAccess.Abstractions;
using Mt.ChangeLog.TransferObjects.ArmEdit;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Mt.ChangeLog.DataAccess
{
    /// <summary>
    /// Репозиторий для работы с ArmEdit.
    /// </summary>
    internal sealed class ArmEditRepository : AbstractRepository, IArmEditRepository
    {
        /// <summary>
        /// Инициализация экземпляра класса <see cref="ArmEditRepository"/>.
        /// </summary>
        /// <param name="logger">Журнал логирования.</param>
        /// <param name="connection">Подключение к базе данных.</param>
        public ArmEditRepository(ILogger<ArmEditRepository> logger, IDbConnection connection)
            : base(logger, connection)
        {
        }

        /// <inheritdoc />
        public async Task<ArmEditModel> GetActualAsync()
        {
            var qSql = @$"SELECT * FROM ""{Schema}"".""get_ActualArmEdit""();";
            var result = await this.connection.QuerySingleAsync<ArmEditModel>(qSql);
            return result;
        }

        /// <inheritdoc />
        public async Task<ArmEditModel> GetEntityAsync(Guid guid)
        {
            var qSql = @$"SELECT * FROM ""{Schema}"".""get_ArmEdit""(@guid);";
            var result = await this.connection.QuerySingleAsync<ArmEditModel>(qSql, new { guid });
            return result;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<ArmEditShortModel>> GetShortEntitiesAsync()
        {
            var qSql = @$"SELECT * FROM ""{Schema}"".""get_ShortArmEdits""();";
            var result = await this.connection.QueryAsync<ArmEditShortModel>(qSql);
            return result;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<ArmEditTableModel>> GetTableEntitiesAsync()
        {
            var qSql = @$"SELECT * FROM ""{Schema}"".""get_TableArmEdits""();";
            var result = await this.connection.QueryAsync<ArmEditTableModel>(qSql);
            return result;
        }
    }
}