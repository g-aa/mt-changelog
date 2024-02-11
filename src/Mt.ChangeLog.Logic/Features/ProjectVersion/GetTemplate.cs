using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Mt.ChangeLog.DataContext;
using Mt.ChangeLog.Logic.Mappers;
using Mt.ChangeLog.TransferObjects.ProjectVersion;

namespace Mt.ChangeLog.Logic.Features.ProjectVersion;

/// <summary>
/// Запрос на получение шаблона <see cref="ProjectVersionModel"/>.
/// </summary>
public static class GetTemplate
{
    /// <inheritdoc />
    public sealed class Query : IRequest<ProjectVersionModel>
    {
    }

    /// <inheritdoc />
    public sealed class Handler : IRequestHandler<Query, ProjectVersionModel>
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
        public async Task<ProjectVersionModel> Handle(Query request, CancellationToken cancellationToken)
        {
            _logger.LogDebug("Получен запрос на создание шаблона версии проекта.");

            var status = await _context.ProjectStatuses.AsNoTracking()
                .FirstAsync(e => e.Default, cancellationToken);

            var platform = await _context.Platforms.AsNoTracking()
                .FirstAsync(e => e.Default, cancellationToken);

            var module = await _context.AnalogModules.AsNoTracking()
                .FirstAsync(e => e.Default, cancellationToken);

            var result = new ProjectVersionModel
            {
                ProjectStatus = status.ToShortModel(),
                Platform = platform.ToShortModel(),
                AnalogModule = module.ToShortModel(),
            };

            _logger.LogDebug("Запрос на создание шаблона версии проекта '{Result}' выполнен успешно.", result);
            return result;
        }
    }
}