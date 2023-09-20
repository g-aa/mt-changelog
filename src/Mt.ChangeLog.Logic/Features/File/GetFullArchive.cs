using System.IO.Compression;

using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Mt.ChangeLog.DataContext;
using Mt.ChangeLog.Logic.Mappers;
using Mt.ChangeLog.TransferObjects.Historical;
using Mt.ChangeLog.TransferObjects.Other;

namespace Mt.ChangeLog.Logic.Features.File;

/// <summary>
/// Запрос на получение полного архива логов изменения проектов.
/// </summary>
public static class GetFullArchive
{
    /// <inheritdoc />
    public sealed class Query : IRequest<FileModel>
    {
    }

    /// <inheritdoc />
    public sealed class Handler : IRequestHandler<Query, FileModel>
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
        public async Task<FileModel> Handle(Query request, CancellationToken cancellationToken)
        {
            this.logger.LogDebug("Получен запрос на предоставление полного архива логов изменения проектов.");

            // получить полный перечень ID-s всех проектов.
            var projectIds = await this.context.ProjectVersions.AsNoTracking()
                .Select(e => e.Id).ToListAsync(cancellationToken);

            FileModel result;
            using (var outStream = new MemoryStream())
            {
                using (var archive = new ZipArchive(outStream, ZipArchiveMode.Create, true))
                {
                    foreach (var id in projectIds)
                    {
                        var projectFile = new ProjectHistoryFileModel(await this.GetProjectVersionHistory(id, cancellationToken));
                        var entry = archive.CreateEntry(projectFile.Title);
                        using (var entryStream = entry.Open())
                        {
                            using (var writer = new MemoryStream(projectFile.Bytes.ToArray()))
                            {
                                writer.CopyTo(entryStream);
                            }
                        }
                    }
                }

                result = new ZipFileModel("ChangeLog", outStream.ToArray());
            }

            this.logger.LogDebug("Запрос на предоставление полного архива логов изменения проектов выполнен успешно, количество проектов '{Count}'.", projectIds.Count);
            return result;
        }

        /// <summary>
        /// Получить полную историю версии проекта.
        /// </summary>
        /// <param name="guid">ИД версии проекта.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Результат.</returns>
        private async Task<ProjectVersionHistoryModel> GetProjectVersionHistory(Guid guid, CancellationToken cancellationToken)
        {
            var query = this.context.ProjectRevisions.AsNoTracking()
                .Include(e => e.ArmEdit)
                .Include(e => e.Authors)
                .Include(e => e.ProjectVersion!.AnalogModule)
                .Include(e => e.ProjectVersion!.Platform)
                .Include(e => e.Communication!.Protocols)
                .Include(e => e.RelayAlgorithms)
                .AsSingleQuery();

            var entity = await query.Where(pr => pr.ProjectVersion!.Id == guid)
                .OrderByDescending(pr => pr.Revision)
                .FirstAsync(cancellationToken);

            var result = new ProjectVersionHistoryModel()
            {
                Title = $"{entity.ProjectVersion!.Prefix}-{entity.ProjectVersion.Title}-{entity.ProjectVersion.Version}",
            };

            do
            {
                result.History.Add(entity.ToHistoryModel());
            }
            while ((entity = await query.FirstOrDefaultAsync(pr => pr.Id == entity.ParentRevisionId, cancellationToken)) != null);

            return result;
        }
    }
}