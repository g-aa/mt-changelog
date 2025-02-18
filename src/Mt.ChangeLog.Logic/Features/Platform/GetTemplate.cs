using MediatR;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using Mt.ChangeLog.DataContext;
using Mt.ChangeLog.Logic.Mappers;
using Mt.ChangeLog.TransferObjects.Platform;

namespace Mt.ChangeLog.Logic.Features.Platform;

/// <summary>
/// Запрос на получение шаблона <see cref="PlatformModel"/>.
/// </summary>
public static class GetTemplate
{
    /// <inheritdoc />
    public sealed class Query : IRequest<PlatformModel>
    {
    }

    /// <inheritdoc />
    public sealed class Handler : IRequestHandler<Query, PlatformModel>
    {
        private readonly ILogger<Handler> _logger;

        private readonly MtContext _context;

        /// <summary>
        /// Инициализация нового экземпляра класса <see cref="Handler"/>.
        /// </summary>
        /// <param name="logger">Журнал логирования.</param>
        /// <param name="context">Контекст данных.</param>
        public Handler(ILogger<Handler> logger, MtContext context)
        {
            _logger = logger;
            _context = context;
        }

        /// <inheritdoc />
        public async Task<PlatformModel> Handle(Query request, CancellationToken cancellationToken)
        {
            _logger.LogDebug("Получен запрос на создание шаблона платформы.");

            var modules = await _context.AnalogModules.AsNoTracking()
                .Where(e => e.Default)
                .Select(e => e.ToShortModel())
                .ToListAsync(cancellationToken);

            var model = new PlatformModel
            {
                AnalogModules = modules,
            };

            _logger.LogDebug("Запрос на создание шаблона платформы '{Model}' выполнен успешно.", model);
            return model;
        }
    }
}