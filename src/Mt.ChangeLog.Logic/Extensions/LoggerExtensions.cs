using Microsoft.Extensions.Logging;

namespace Mt.ChangeLog.Logic.Extensions;

/// <summary>
/// Методы расширения для <see cref="ILogger"/>.
/// </summary>
public static class LoggerExtensions
{
    /// <summary>
    /// Добавить компоненты <see cref="IMtUser"/> к <see cref="ILogger"/>.
    /// </summary>
    /// <typeparam name="TUser">Тип пользователя.</typeparam>
    /// <param name="logger">Журнал логирования.</param>
    /// <param name="user">Пользователь используемый в логике приложения МТ.</param>
    /// <returns><see cref="IDisposable"/> используемый для освобождения ресурсов.</returns>
    public static IDisposable? BeginWithMtUserScope<TUser>(this ILogger logger, TUser user)
        where TUser : notnull, IMtUser
    {
        return logger.BeginScope(new[]
        {
            new KeyValuePair<string, object>("UserName", user.Name),
        });
    }
}