using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Mt.ChangeLog.DataContext;
using Mt.ChangeLog.Logic.Mappers;
using Mt.ChangeLog.TransferObjects.Historical;
using Mt.ChangeLog.TransferObjects.Other;
using Mt.Entities.Abstractions.Extensions;

namespace Mt.ChangeLog.Logic.Features.History;

/// <summary>
/// Запрос на получение истории изменения для редакции проекта.
/// </summary>
public static class GetProjectRevisionHistory
{
    /// <inheritdoc />
    public sealed record Query(BaseModel Model) : IRequest<ProjectRevisionHistoryModel>
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
    public sealed class Handler : IRequestHandler<Query, ProjectRevisionHistoryModel>
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
        public Task<ProjectRevisionHistoryModel> Handle(Query request, CancellationToken cancellationToken)
        {
            this.logger.LogDebug("Получен запрос на предоставление истории изменения для редакции проекта '{Id}'.", request.Model.Id);

            var result = this.context.ProjectRevisions.AsNoTracking()
                .Include(e => e.ArmEdit)
                .Include(e => e.Authors)
                .Include(e => e.ProjectVersion!.AnalogModule)
                .Include(e => e.ProjectVersion!.Platform)
                .Include(e => e.Communication!.Protocols)
                .Include(e => e.RelayAlgorithms)
                .AsSingleQuery()
                .Search(request.Model.Id)
                .ToHistoryModel();

            return Task.FromResult(result);
        }
    }
}