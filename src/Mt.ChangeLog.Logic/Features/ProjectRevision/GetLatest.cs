using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Mt.ChangeLog.DataContext;
using Mt.ChangeLog.Logic.Mappers;
using Mt.ChangeLog.TransferObjects.ProjectRevision;

namespace Mt.ChangeLog.Logic.Features.ProjectRevision;

/// <summary>
/// Запрос на получение перечня кратких моделей данных <see cref="LastProjectRevisionModel"/>.
/// </summary>
public static class GetLatest
{
    /// <inheritdoc />
    public sealed class Query : IRequest<IEnumerable<LastProjectRevisionModel>>
    {
    }

    /// <inheritdoc />
    public sealed class Handler : IRequestHandler<Query, IEnumerable<LastProjectRevisionModel>>
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
        public async Task<IEnumerable<LastProjectRevisionModel>> Handle(Query request, CancellationToken cancellationToken)
        {
            this.logger.LogDebug("Получен запрос на предоставление данных о последних редакциях проектов.");

            var result = await this.context.LastProjectRevisions.Select(e => e.ToModel())
                .ToArrayAsync(cancellationToken);

            this.logger.LogDebug("Запрос на получение данных о последних редакциях проектов '{Result}' выполнен успешно.", result);
            return result;
        }
    }
}