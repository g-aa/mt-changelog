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
        public async Task<ProjectVersionModel> Handle(Query request, CancellationToken cancellationToken)
        {
            this.logger.LogDebug("Получен запрос на создание шаблона версии проекта.");

            var status = await this.context.ProjectStatuses.AsNoTracking()
                .FirstAsync(e => e.Default, cancellationToken);

            var platform = await this.context.Platforms.AsNoTracking()
                .FirstAsync(e => e.Default, cancellationToken);

            var module = await this.context.AnalogModules.AsNoTracking()
                .FirstAsync(e => e.Default, cancellationToken);

            var result = new ProjectVersionModel
            {
                ProjectStatus = status.ToShortModel(),
                Platform = platform.ToShortModel(),
                AnalogModule = module.ToShortModel(),
            };

            this.logger.LogDebug("Запрос на создание шаблона версии проекта '{Result}' выполнен успешно.", result);
            return result;
        }
    }
}