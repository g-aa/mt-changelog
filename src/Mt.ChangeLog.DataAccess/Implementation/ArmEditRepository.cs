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
        var result = await Connection.QuerySingleAsync<ArmEditModel>(qSql);
        return result;
    }

    /// <inheritdoc />
    public async Task<ArmEditModel> GetEntityAsync(Guid guid)
    {
        var qSql = @$"SELECT * FROM ""{Schema}"".""get_ArmEdit""(@guid);";
        var result = await Connection.QuerySingleAsync<ArmEditModel>(qSql, new { guid });
        return result;
    }

    /// <inheritdoc />
    public async Task<IReadOnlyCollection<ArmEditShortModel>> GetShortEntitiesAsync()
    {
        var qSql = @$"SELECT * FROM ""{Schema}"".""get_ShortArmEdits""();";
        var result = await Connection.QueryAsync<ArmEditShortModel>(qSql);
        return result.ToList();
    }

    /// <inheritdoc />
    public async Task<IReadOnlyCollection<ArmEditTableModel>> GetTableEntitiesAsync()
    {
        var qSql = @$"SELECT * FROM ""{Schema}"".""get_TableArmEdits""();";
        var result = await Connection.QueryAsync<ArmEditTableModel>(qSql);
        return result.ToList();
    }
}