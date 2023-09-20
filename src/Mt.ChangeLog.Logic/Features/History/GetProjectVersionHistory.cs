using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Mt.ChangeLog.DataContext;
using Mt.ChangeLog.Logic.Mappers;
using Mt.ChangeLog.TransferObjects.Historical;
using Mt.ChangeLog.TransferObjects.Other;

namespace Mt.ChangeLog.Logic.Features.History;

/// <summary>
/// Запрос на получение истории изменения для версии проекта.
/// </summary>
public static class GetProjectVersionHistory
{
    /// <inheritdoc />
    public sealed record Query(BaseModel Model) : IRequest<ProjectVersionHistoryModel>
    {
    }

    /// <summary>
    /// Валидатор модели <see cref="Query"/>.
    /// </summary>
    public sealed class Validator : AbstractValidator<Query>
    {
        /// <summary>
        /// Инициализация экземпляра <see cref="Validator"/>.
        /// </summary>
        /// <param name="validator">Base model validator.</param>
        public Validator(IValidator<BaseModel> validator)
        {
            this.RuleFor(e => e.Model).SetValidator(validator);
        }
    }

    /// <inheritdoc />
    public sealed class Handler : IRequestHandler<Query, ProjectVersionHistoryModel>
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
        public Task<ProjectVersionHistoryModel> Handle(Query request, CancellationToken cancellationToken)
        {
            this.logger.LogDebug("Получен запрос на предоставление истории изменения для версии проекта '{Id}'.", request.Model.Id);

            var result = new ProjectVersionHistoryModel();

            var query = this.context.ProjectRevisions.AsNoTracking()
                .Include(e => e.ArmEdit)
                .Include(e => e.Authors)
                .Include(e => e.ProjectVersion!.AnalogModule)
                .Include(e => e.ProjectVersion!.Platform)
                .Include(e => e.Communication!.Protocols)
                .Include(e => e.RelayAlgorithms)
                .AsSingleQuery();

            var entity = query.Where(pr => pr.ProjectVersion!.Id == request.Model.Id)
                .OrderByDescending(pr => pr.Revision)
                .FirstOrDefault();

            if (entity != null)
            {
                result.Title = entity.ProjectVersion!.ToShortModel().ToString();
                do
                {
                    result.History.Add(entity.ToHistoryModel());
                }
                while ((entity = query.FirstOrDefault(pr => pr.Id == entity.ParentRevisionId)) != null);
            }

            return Task.FromResult(result);
        }
    }
}