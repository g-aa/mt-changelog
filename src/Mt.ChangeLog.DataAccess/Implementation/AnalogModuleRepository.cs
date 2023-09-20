using System.Data;

using Dapper;
using Microsoft.Extensions.Logging;
using Mt.ChangeLog.DataAccess.Abstraction;
using Mt.ChangeLog.TransferObjects.AnalogModule;
using Mt.ChangeLog.TransferObjects.Platform;

namespace Mt.ChangeLog.DataAccess.Implementation;

/// <summary>
/// Репозиторий для работы с аналоговыми модулями.
/// </summary>
public sealed class AnalogModuleRepository : AbstractRepository, IAnalogModuleRepository
{
    /// <summary>
    /// Инициализация экземпляра класса <see cref="AnalogModuleRepository"/>.
    /// </summary>
    /// <param name="logger">Журнал логирования.</param>
    /// <param name="connection">Подключение к базе данных.</param>
    public AnalogModuleRepository(ILogger<AnalogModuleRepository> logger, IDbConnection connection)
        : base(logger, connection)
    {
    }

    /// <inheritdoc />
    public async Task<AnalogModuleModel> GetEntityAsync(Guid guid)
    {
        var qSql = $@"SELECT * FROM ""{Schema}"".""get_AnalogModule""(@guid);
                          SELECT * FROM ""{Schema}"".""get_PlatformsForAnalogModule""(@guid);";
        var qMultiple = await this.Connection.QueryMultipleAsync(qSql, new { guid });
        var module = await qMultiple.ReadSingleAsync<AnalogModuleModel>();
        module.Platforms = await qMultiple.ReadAsync<PlatformShortModel>();
        return module;
    }

    /// <inheritdoc />
    public async Task<IEnumerable<AnalogModuleShortModel>> GetShortEntitiesAsync()
    {
        var qSql = @$"SELECT * FROM ""{Schema}"".""get_ShortAnalogModules""();";
        var result = await this.Connection.QueryAsync<AnalogModuleShortModel>(qSql);
        return result;
    }

    /// <inheritdoc />
    public async Task<IEnumerable<AnalogModuleTableModel>> GetTableEntitiesAsync()
    {
        var qSql = $@"SELECT * FROM ""{Schema}"".""get_TableAnalogModules""();";
        var result = await this.Connection.QueryAsync<AnalogModuleTableModel>(qSql);
        return result;
    }
}