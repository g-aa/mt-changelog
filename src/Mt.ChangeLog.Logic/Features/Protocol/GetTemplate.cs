using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Mt.ChangeLog.DataContext;
using Mt.ChangeLog.Logic.Mappers;
using Mt.ChangeLog.TransferObjects.Protocol;

namespace Mt.ChangeLog.Logic.Features.Protocol;

/// <summary>
/// Запрос на получение шаблона <see cref="ProtocolModel"/>.
/// </summary>
public static class GetTemplate
{
    /// <inheritdoc />
    public sealed class Query : IRequest<ProtocolModel>
    {
    }

    /// <inheritdoc />
    public sealed class Handler : IRequestHandler<Query, ProtocolModel>
    {
        private readonly ILogger<Handler> logger;

        private readonly MtContext context;

        /// <summary>
        /// Инициализация нового экземпляра класса <see cref="Handler"/>.
        /// </summary>
        /// <param name="logger">Журнал логирования.</param>
        /// <param name="context">Контекст данных.</param>
        public Handler(ILogger<Handler> logger, MtContext context)
        {
            this.logger = logger;
            this.context = context;
        }

        /// <inheritdoc />
        public async Task<ProtocolModel> Handle(Query request, CancellationToken cancellationToken)
        {
            this.logger.LogDebug("Получен запрос на создание шаблона протокола.");

            var communications = await this.context.Communications.AsNoTracking()
                .Where(e => e.Default)
                .Select(e => e.ToShortModel())
                .ToListAsync(cancellationToken);

            var result = new ProtocolModel
            {
                Communications = communications,
            };

            this.logger.LogDebug("Запрос на создание шаблона протокола '{Result}' выполнен успешно.", result);
            return result;
        }
    }
}