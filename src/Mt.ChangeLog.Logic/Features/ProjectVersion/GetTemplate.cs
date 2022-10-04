using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Mt.ChangeLog.Context;
using Mt.ChangeLog.Entities.Extensions.Tables;
using Mt.ChangeLog.Logic.Models;
using Mt.ChangeLog.TransferObjects.ProjectVersion;
using Mt.Utilities;
using System.Threading;
using System.Threading.Tasks;

namespace Mt.ChangeLog.Logic.Features.ProjectVersion
{
    /// <summary>
    /// Запрос на получение шаблона <see cref="ProjectVersionModel"/>.
    /// </summary>
    public static class GetTemplate
    {
        /// <inheritdoc />
        public sealed class Query : MtQuery<Unit, ProjectVersionModel>
        {
            /// <summary>
            /// Инициализация нового экземпляра класса <see cref="Query"/>.
            /// </summary>
            public Query() : base(Unit.Value)
            {
            }

            /// <inheritdoc />
            public override string ToString()
            {
                return $"{base.ToString()} - получение шаблона сущности вида {nameof(ProjectVersionModel)}.";
            }
        }

        /// <inheritdoc />
        public sealed class Handler : IRequestHandler<Query, ProjectVersionModel>
        {
            /// <summary>
            /// Журнал логирования.
            /// </summary>
            private readonly ILogger<Handler> logger;

            /// <summary>
            /// Контекст данных.
            /// </summary>
            private readonly MtContext context;

            /// <summary>
            /// Инициализация нового экземпляра класса <see cref="Handler"/>.
            /// </summary>
            /// <param name="logger">Журнал логирования.</param>
            /// <param name="context">Контекст данных.</param>
            public Handler(ILogger<Handler> logger, MtContext context)
            {
                this.logger = Check.NotNull(logger, nameof(logger));
                this.context = Check.NotNull(context, nameof(context));
            }

            /// <inheritdoc />
            public async Task<ProjectVersionModel> Handle(Query request, CancellationToken cancellationToken)
            {
                Check.NotNull(request, nameof(request));
                this.logger.LogInformation(request.ToString());

                var status = await this.context.ProjectStatuses.AsNoTracking()
                    .FirstAsync(e => e.Default, cancellationToken);

                var platform = await this.context.Platforms.AsNoTracking()
                    .FirstAsync(e => e.Default, cancellationToken);

                var module = await this.context.AnalogModules.AsNoTracking()
                    .FirstAsync(e => e.Default, cancellationToken);

                var result = new ProjectVersionModel()
                {
                    ProjectStatus = status.ToShortModel(),
                    Platform = platform.ToShortModel(),
                    AnalogModule = module.ToShortModel(),
                };

                return result;
            }
        }
    }
}