using FluentValidation;

using MediatR;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using Mt.ChangeLog.DataContext;
using Mt.ChangeLog.Logic.Mappers;
using Mt.ChangeLog.TransferObjects.Historical;
using Mt.FluentValidation;

namespace Mt.ChangeLog.Logic.Features.History;

/// <summary>
/// Перечень моделей для дерева изменений.
/// </summary>
public static class GetProjectTree
{
    /// <inheritdoc />
    public sealed record Query(string Title) : IRequest<IReadOnlyCollection<ProjectRevisionTreeModel>>
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
        public Validator()
        {
            RuleFor(e => e.Title)
                .Length(2, 16)
                .IsTrim();
        }
    }

    /// <inheritdoc />
    public sealed class Handler : IRequestHandler<Query, IReadOnlyCollection<ProjectRevisionTreeModel>>
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
        public async Task<IReadOnlyCollection<ProjectRevisionTreeModel>> Handle(Query request, CancellationToken cancellationToken)
        {
            var title = request.Title;
            _logger.LogDebug("Получен запрос на предоставление перечня моделей для дерева изменений '{Title}'.", title);

            var result = await _context.ProjectRevisions.AsNoTracking()
                .Include(pr => pr.ArmEdit)
                .Include(pr => pr.ProjectVersion!.AnalogModule)
                .Include(pr => pr.ProjectVersion!.Platform)
                .Where(pr => pr.ProjectVersion!.Title == title)
                .Select(pr => pr.ToTreeModel())
                .ToListAsync(cancellationToken);

            return result;
        }
    }
}