using Microsoft.EntityFrameworkCore;
using Mt.ChangeLog.DataContext;

namespace Mt.ChangeLog.Logic.Test;

/// <summary>
/// Вспомогательные методы для тестирования приложения.
/// </summary>
public static class MtContextHelper
{
    /// <summary>
    /// Создание контекста данных <see cref="MtContext"/> для нужд тестирования.
    /// </summary>
    /// <returns>Сконфигурированный контекст данных.</returns>
    public static MtContext CreateInMemoryDbContext()
    {
        var optionsBuilder = new DbContextOptionsBuilder<MtContext>();
        optionsBuilder.UseInMemoryDatabase($"notif_{Guid.NewGuid()}");
        var appDbContext = new MtContext(optionsBuilder.Options, null!);

        SeedDatabase(appDbContext);

        return appDbContext;
    }

    /// <summary>
    /// Инициализация базы данных тестовым дампом.
    /// </summary>
    /// <param name="context">Контекст базы данных.</param>
    private static void SeedDatabase(MtContext context)
    {
        context.SaveChanges();
    }
}