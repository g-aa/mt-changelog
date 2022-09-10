using Dapper;
using Mt.ChangeLog.DataAccess.Abstractions;
using Mt.ChangeLog.TransferObjects.ArmEdit;
using Mt.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Mt.ChangeLog.DataAccess
{
    /// <summary>
    /// Репозиторий для работы с ArmEdit.
    /// </summary>
    internal sealed class ArmEditRepository : IArmEditRepository
    {
        /// <summary>
        /// Подключение к базе данных.
        /// </summary>
        private readonly IDbConnection connection;

        /// <summary>
        /// Инициализация экземпляра класса <see cref="ArmEditRepository"/>.
        /// </summary>
        /// <param name="connection">Подключение к базе данных.</param>
        /// <exception cref="ArgumentNullException">Срабатывает если подключение к базе данных равно null.</exception>
        public ArmEditRepository(IDbConnection connection)
        {
            this.connection = Check.NotNull(connection, nameof(connection));
        }

        /// <inheritdoc />
        public Task<ArmEditModel> GetActualAsync()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public async Task<ArmEditModel> GetEntityAsync(Guid guid)
        {
            var qSql = @$"SELECT arm.""Id"",
                                 arm.""Version"",
                                 arm.""DIVG"",
                                 arm.""Date"",                    
                                 arm.""Description""
                          FROM ""MT"".""ArmEdit"" arm
                          WHERE arm.""Id""= @guid;";
            var result = await this.connection.QuerySingleAsync<ArmEditModel>(qSql, new { guid });
            return result;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<ArmEditShortModel>> GetShortEntitiesAsync()
        {
            var qSql = @$"SELECT arm.""Id"", 
                                 arm.""Version""
                          FROM ""MT"".""ArmEdit"" arm";
            var result = await this.connection.QueryAsync<ArmEditShortModel>(qSql);
            return result;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<ArmEditTableModel>> GetTableEntitiesAsync()
        {
            var qSql = @$"SELECT arm.""Id"",
                                 arm.""Version"",
                                 arm.""DIVG"",
                                 arm.""Date"",                    
                                 arm.""Description""
                          FROM ""MT"".""ArmEdit"" arm";
            var result = await this.connection.QueryAsync<ArmEditTableModel>(qSql);
            return result;
        }
    }
}