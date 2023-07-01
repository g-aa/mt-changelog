using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Mt.ChangeLog.Context;
using Mt.ChangeLog.Entities.Extensions.Tables;
using Mt.ChangeLog.Logic.Models;
using Mt.ChangeLog.TransferObjects.Historical;
using Mt.ChangeLog.TransferObjects.Other;
using Mt.Utilities;

namespace Mt.ChangeLog.Logic.Features.History
{
    /// <summary>
    /// Запрос на получение истории изменения для версии проета.
    /// </summary>
    public static class GetProjectVersionHistory
    {
        /// <inheritdoc />
        public sealed class Query : MtQuery<BaseModel, ProjectVersionHistoryModel>, IValidatedRequest
        {
            /// <summary>
            /// Инициализация нового экземпляра класса <see cref="Query"/>.
            /// </summary>
            /// <param name="model">Базовая модель.</param>
            public Query(BaseModel model) : base(model)
            {
            }

            /// <inheritdoc />
            public override string ToString()
            {
                return $"{base.ToString()} - получить историю изменения для версии проета.";
            }
        }

        /// <summary>
        /// Валидатор модели <see cref="Query"/>.
        /// </summary>
        public sealed class QueryValidator : AbstractValidator<Query>
        {
            /// <summary>
            /// Инициализация экземпляра <see cref="QueryValidator"/>.
            /// </summary>
            public QueryValidator(BaseModelValidator validator)
            {
                this.RuleFor(e => e.Model)
                    .SetValidator(Check.NotNull(validator, nameof(validator)));
            }
        }

        /// <inheritdoc />
        public sealed class Handler : IRequestHandler<Query, ProjectVersionHistoryModel>
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
            public async Task<ProjectVersionHistoryModel> Handle(Query request, CancellationToken cancellationToken)
            {
                Check.NotNull(request, nameof(request));
                this.logger.LogInformation(request.ToString());

                var result = new ProjectVersionHistoryModel();

                var query = this.context.ProjectRevisions.AsNoTracking()
                    .Include(e => e.ArmEdit)
                    .Include(e => e.Authors)
                    .Include(e => e.ProjectVersion.AnalogModule)
                    .Include(e => e.ProjectVersion.Platform)
                    .Include(e => e.Communication.Protocols)
                    .Include(e => e.RelayAlgorithms)
                    .AsSingleQuery();

                var entity = query.Where(pr => pr.ProjectVersion.Id == request.Model.Id)
                    .OrderByDescending(pr => pr.Revision)
                    .FirstOrDefault();

                if (entity != null)
                {
                    result.Title = entity.ProjectVersion.ToShortModel().ToString();
                    do
                    {
                        result.History.Add(entity.ToHistoryModel());
                    } while ((entity = query.FirstOrDefault(pr => pr.Id == entity.ParentRevisionId)) != null);
                }

                return await Task.FromResult(result);
            }
        }
    }
}