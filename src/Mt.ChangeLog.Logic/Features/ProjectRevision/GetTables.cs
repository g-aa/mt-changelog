using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Mt.ChangeLog.Context;
using Mt.ChangeLog.Entities.Extensions.Tables;
using Mt.ChangeLog.Logic.Models;
using Mt.ChangeLog.TransferObjects.ProjectRevision;
using Mt.Utilities;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Mt.ChangeLog.Logic.Features.ProjectRevision
{
    /// <summary>
    /// Запрос на получение перечня моделий данных для таблиц <see cref="ProjectRevisionTableModel"/>.
    /// </summary>
    public static class GetTables
    {
        /// <inheritdoc />
        public sealed class Query : MtQuery<Unit, IEnumerable<ProjectRevisionTableModel>>
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
                return $"{base.ToString()} - получение перечня сущностей вида {nameof(ProjectRevisionTableModel)}.";
            }
        }

        /// <inheritdoc />
        public sealed class Handler : IRequestHandler<Query, IEnumerable<ProjectRevisionTableModel>>
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
            public async Task<IEnumerable<ProjectRevisionTableModel>> Handle(Query request, CancellationToken cancellationToken)
            {
                Check.NotNull(request, nameof(request));
                this.logger.LogInformation(request.ToString());

                var result = await this.context.ProjectRevisions.AsNoTracking()
                    .Include(pr => pr.ArmEdit)
                    .Include(pr => pr.ProjectVersion.AnalogModule)
                    .OrderByDescending(pr => pr.Date).ThenByDescending(pr => pr.ArmEdit.Version)
                    .Select(pr => pr.ToTableModel())
                    .ToListAsync(cancellationToken);

                return result;
            }
        }
    }
}