using System.Globalization;

using MediatR;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using Mt.ChangeLog.DataContext;
using Mt.ChangeLog.Logic.Mappers;
using Mt.ChangeLog.TransferObjects.Author;
using Mt.ChangeLog.TransferObjects.Other;
using Mt.ChangeLog.TransferObjects.ProjectRevision;
using Mt.ChangeLog.TransferObjects.RelayAlgorithm;
using Mt.Entities.Abstractions.Extensions;

namespace Mt.ChangeLog.Logic.Features.ProjectRevision;

/// <summary>
/// Запрос на получение шаблона <see cref="ProjectRevisionModel"/>.
/// </summary>
public static class GetTemplate
{
    /// <inheritdoc />
    public sealed record Query(BaseModel Model) : IRequest<ProjectRevisionModel>
    {
    }

    /// <inheritdoc />
    public sealed class Handler : IRequestHandler<Query, ProjectRevisionModel>
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
        public Task<ProjectRevisionModel> Handle(Query request, CancellationToken cancellationToken)
        {
            _logger.LogDebug("Получен запрос на создание шаблона редакции проекта.");

            var model = request.Model;

            var project = _context.ProjectVersions
                .Include(e => e.AnalogModule)
                .Search(model.Id)
                .ToShortModel();

            var lastRevision = _context.ProjectRevisions
                .Include(e => e.Communication)
                .Include(e => e.Authors)
                .Include(e => e.RelayAlgorithms)
                .Where(e => e.ProjectVersionId == model.Id)
                .OrderByDescending(e => e.Revision)
                .FirstOrDefault();

            var armEdit = _context.ArmEdits
                .OrderByDescending(e => e.Version)
                .First()
                .ToShortModel();

            var communication = lastRevision?.Communication?.ToShortModel();
            if (communication is null)
            {
                communication = _context.Communications
                    .OrderByDescending(e => e.Title)
                    .First()
                    .ToShortModel();
            }

            var revision = lastRevision == null
                ? "00"
                : (int.Parse(lastRevision.Revision, CultureInfo.InvariantCulture) + 1).ToString("D2", CultureInfo.InvariantCulture);

            var algorithms = lastRevision?.RelayAlgorithms
                .Select(e => e.ToShortModel()).ToList() ?? new List<RelayAlgorithmShortModel>();

            var authors = lastRevision?.Authors
                .Select(e => e.ToShortModel()).ToList() ?? new List<AuthorShortModel>();

            var result = new ProjectRevisionModel
            {
                ParentRevision = lastRevision?.ToShortModel(),
                ProjectVersion = project,
                Revision = revision,
                ArmEdit = armEdit,
                Communication = communication,
                RelayAlgorithms = algorithms,
                Authors = authors,
            };

            _logger.LogDebug("Запрос на создание шаблона редакции проекта выполнен успешно.");
            return Task.FromResult(result);
        }
    }
}