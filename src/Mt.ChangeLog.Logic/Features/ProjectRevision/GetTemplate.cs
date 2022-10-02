using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Mt.ChangeLog.Context;
using Mt.ChangeLog.Entities.Extensions.Tables;
using Mt.ChangeLog.Logic.Models;
using Mt.ChangeLog.TransferObjects.Other;
using Mt.ChangeLog.TransferObjects.ProjectRevision;
using Mt.Entities.Abstractions.Extensions;
using Mt.Utilities;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Mt.ChangeLog.Logic.Features.ProjectRevision
{
    /// <summary>
    /// Запрос на получение шаблона <see cref="ProjectRevisionModel"/>.
    /// </summary>
    public static class GetTemplate
    {
        /// <inheritdoc />
        public sealed class Query : MtQuery<BaseModel, ProjectRevisionModel>
        {
            /// <summary>
            /// Инициализация нового экземпляра класса <see cref="Query"/>.
            /// </summary>
            /// <param name="model">Модель данных.</param>
            public Query(BaseModel model) : base(model)
            {
            }

            /// <inheritdoc />
            public override string ToString()
            {
                return $"{base.ToString()} - получение шаблона сущности вида {nameof(ProjectRevisionModel)}.";
            }
        }

        /// <inheritdoc />
        public sealed class Handler : IRequestHandler<Query, ProjectRevisionModel>
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
            public async Task<ProjectRevisionModel> Handle(Query request, CancellationToken cancellationToken)
            {
                var model = Check.NotNull(request, nameof(request)).Model;
                this.logger.LogInformation(request.ToString());

                var project = this.context.ProjectVersions
                    .Include(e => e.AnalogModule)
                    .Search(model.Id)
                    .ToShortModel();

                var lastRevision = this.context.ProjectRevisions
                    .Include(e => e.Communication)
                    .Include(e => e.Authors)
                    .Include(e => e.RelayAlgorithms)
                    .Where(e => e.ProjectVersionId == model.Id)
                    .OrderByDescending(e => e.Revision)
                    .FirstOrDefault();

                var armEdit = this.context.ArmEdits
                    .OrderByDescending(e => e.Version)
                    .FirstOrDefault()
                    .ToShortModel();

                var communications = lastRevision?.Communication.ToShortModel();
                if (communications is null)
                {
                    communications = this.context.Communications
                        .OrderByDescending(e => e.Title)
                        .FirstOrDefault()
                        .ToShortModel();
                }

                var revision = lastRevision is null ? "00" : (int.Parse(lastRevision.Revision) + 1).ToString("D2");

                var algorithms = lastRevision?.RelayAlgorithms
                    .Select(e => e.ToShortModel());

                var authors = lastRevision?.Authors
                    .Select(e => e.ToShortModel());

                return await Task.FromResult(new ProjectRevisionModel()
                {
                    ParentRevision = lastRevision?.ToShortModel(),
                    ProjectVersion = project,
                    Revision = revision,
                    ArmEdit = armEdit,
                    Communication = communications,
                    RelayAlgorithms = algorithms,
                    Authors = authors,
                });
            }
        }
    }
}