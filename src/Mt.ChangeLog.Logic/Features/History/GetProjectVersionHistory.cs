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
            RuleFor(e => e.Model).SetValidator(validator);
        }
    }

    /// <inheritdoc />
    public sealed class Handler : IRequestHandler<Query, ProjectVersionHistoryModel>
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
        public Task<ProjectVersionHistoryModel> Handle(Query request, CancellationToken cancellationToken)
        {
            var model = request.Model;
            _logger.LogDebug("Получен запрос на предоставление истории изменения для версии проекта '{Model}'.", model);

            var query = _context.ProjectRevisions.AsNoTracking()
                .Include(e => e.ArmEdit)
                .Include(e => e.Authors)
                .Include(e => e.ProjectVersion!.AnalogModule)
                .Include(e => e.ProjectVersion!.Platform)
                .Include(e => e.Communication!.Protocols)
                .Include(e => e.RelayAlgorithms)
                .AsSingleQuery();

            var entity = query.Where(pr => pr.ProjectVersion!.Id == model.Id)
                .OrderByDescending(pr => pr.Revision)
                .FirstOrDefault();

            var result = new ProjectVersionHistoryModel();
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