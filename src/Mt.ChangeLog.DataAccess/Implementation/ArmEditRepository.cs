using System.Data;

using Dapper;
using Microsoft.Extensions.Logging;
using Mt.ChangeLog.DataAccess.Abstraction;
using Mt.ChangeLog.TransferObjects.ArmEdit;

namespace Mt.ChangeLog.DataAccess.Implementation;

/// <summary>
/// Репозиторий для работы с ArmEdit.
/// </summary>
public sealed class ArmEditRepository : AbstractRepository, IArmEditRepository
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
        var result = await this.Connection.QuerySingleAsync<ArmEditModel>(qSql);
        return result;
    }

    /// <inheritdoc />
    public async Task<ArmEditModel> GetEntityAsync(Guid guid)
    {
        var qSql = @$"SELECT * FROM ""{Schema}"".""get_ArmEdit""(@guid);";
        var result = await this.Connection.QuerySingleAsync<ArmEditModel>(qSql, new { guid });
        return result;
    }

    /// <inheritdoc />
    public async Task<IEnumerable<ArmEditShortModel>> GetShortEntitiesAsync()
    {
        var qSql = @$"SELECT * FROM ""{Schema}"".""get_ShortArmEdits""();";
        var result = await this.Connection.QueryAsync<ArmEditShortModel>(qSql);
        return result;
    }

    /// <inheritdoc />
    public async Task<IEnumerable<ArmEditTableModel>> GetTableEntitiesAsync()
    {
        var qSql = @$"SELECT * FROM ""{Schema}"".""get_TableArmEdits""();";
        var result = await this.Connection.QueryAsync<ArmEditTableModel>(qSql);
        return result;
    }
}