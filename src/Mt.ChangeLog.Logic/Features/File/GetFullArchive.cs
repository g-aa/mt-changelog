using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Mt.ChangeLog.Context;
using Mt.ChangeLog.Entities.Extensions.Tables;
using Mt.ChangeLog.Logic.Models;
using Mt.ChangeLog.TransferObjects.Historical;
using Mt.ChangeLog.TransferObjects.Other;
using Mt.Utilities;
using System.IO.Compression;

namespace Mt.ChangeLog.Logic.Features.File
{
    /// <summary>
    /// Запрос на получение полного архива логов изменения проектов.
    /// </summary>
    public static class GetFullArchive
    {
        /// <inheritdoc />
        public sealed class Query : MtQuery<Unit, FileModel>
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
                return $"{base.ToString()} - получение полного архива логов изменения проектов.";
            }
        }

        /// <inheritdoc />
        public sealed class Handler : IRequestHandler<Query, FileModel>
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
            public async Task<FileModel> Handle(Query request, CancellationToken cancellationToken)
            {
                Check.NotNull(request, nameof(request));
                this.logger.LogInformation(request.ToString());

                // получить полный перечень ID-s всех проектов.
                var projectIds = await this.context.ProjectVersions.AsNoTracking()
                    .Select(e => e.Id).ToListAsync(cancellationToken);

                FileModel result = null;
                using (MemoryStream outStream = new MemoryStream())
                {
                    using (ZipArchive archive = new ZipArchive(outStream, ZipArchiveMode.Create, true))
                    {
                        foreach (var id in projectIds)
                        {
                            var projectFile = new ProjectHistoryFileModel(await this.GetProjectVersionHistory(id, cancellationToken));
                            ZipArchiveEntry entry = archive.CreateEntry(projectFile.Title);
                            using (var entryStream = entry.Open())
                            {
                                using (MemoryStream writer = new MemoryStream(projectFile.Bytes.ToArray()))
                                {
                                    writer.CopyTo(entryStream);
                                }
                            }
                        }
                    }
                    result = new ZipFileModel("ChangeLog", outStream.ToArray());
                }

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
                    .Include(e => e.ProjectVersion.AnalogModule)
                    .Include(e => e.ProjectVersion.Platform)
                    .Include(e => e.Communication.Protocols)
                    .Include(e => e.RelayAlgorithms)
                    .AsSingleQuery();

                var entity = await query.Where(pr => pr.ProjectVersion.Id == guid)
                    .OrderByDescending(pr => pr.Revision)
                    .FirstAsync(cancellationToken);

                var result = new ProjectVersionHistoryModel()
                {
                    Title = $"{entity.ProjectVersion.Prefix}-{entity.ProjectVersion.Title}-{entity.ProjectVersion.Version}",
                };

                do
                {
                    result.History.Add(entity.ToHistoryModel());
                } while ((entity = await query.FirstOrDefaultAsync(pr => pr.Id == entity.ParentRevisionId, cancellationToken)) != null);

                return result;
            }
        }
    }
}