using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Mt.ChangeLog.DataContext;
using Mt.ChangeLog.Logic.Mappers;
using Mt.ChangeLog.TransferObjects.AnalogModule;

namespace Mt.ChangeLog.Logic.Features.AnalogModule;

/// <summary>
/// Запрос на получение шаблона <see cref="AnalogModuleModel"/>.
/// </summary>
public static class GetTemplate
{
    /// <inheritdoc />
    public sealed class Query : IRequest<AnalogModuleModel>
    {
    }

    /// <inheritdoc />
    public sealed class Handler : IRequestHandler<Query, AnalogModuleModel>
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
        public async Task<AnalogModuleModel> Handle(Query request, CancellationToken cancellationToken)
        {
            this.logger.LogDebug("Получен запрос на создание шаблона аналогового модуля.");

            var platforms = await this.context.Platforms.AsNoTracking()
                .Where(p => p.Default)
                .Select(p => p.ToShortModel())
                .ToListAsync(cancellationToken);

            var model = new AnalogModuleModel
            {
                Platforms = platforms,
            };

            this.logger.LogDebug("Запрос на создание шаблона аналогового модуля '{Model}' выполнен успешно.", model);
            return model;
        }
    }
}