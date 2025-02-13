using MediatR;

using Microsoft.Extensions.Logging;

using Mt.ChangeLog.Logic.Extensions;

namespace Mt.ChangeLog.Logic.Pipelines;

/// <summary>
/// Декоратор журнала логирования.
/// </summary>
/// <typeparam name="TRequest">Тип запроса.</typeparam>
/// <typeparam name="TResponse">Тип ответа.</typeparam>
public sealed class LoggingScopePipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    private readonly ILogger<LoggingScopePipelineBehavior<TRequest, TResponse>> _logger;

    private readonly IMtUser _user;

    /// <summary>
    /// Инициализация нового экземпляра класса <see cref="LoggingScopePipelineBehavior{TRequest, TResponse}"/>.
    /// </summary>
    /// <param name="logger">Журнал логирования.</param>
    /// <param name="user">Пользователь системы МТ.</param>
    public LoggingScopePipelineBehavior(
        ILogger<LoggingScopePipelineBehavior<TRequest, TResponse>> logger,
        IMtUser user)
    {
        _logger = logger;
        _user = user;
    }

    /// <inheritdoc/>
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        using var scope = _logger.BeginWithMtUserScope(_user);
        var result = await next.Invoke();
        return result;
    }
}