using MediatR;
using Microsoft.Extensions.Logging;
using Mt.ChangeLog.Context;
using Mt.ChangeLog.Logic.Models;
using Mt.Utilities;
using Mt.Utilities.IO;
using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Mt.ChangeLog.Entities.Extensions.Views;
using Microsoft.EntityFrameworkCore;

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

                var projects = await this.context.LastProjectRevisions
                    .OrderBy(e => e.Prefix).ThenBy(e => e.Title).ThenBy(e => e.Version)
                    .Select(e => e.ToProjectVersionShortModel()).ToListAsync();

                //FileModel result = null;
                //using (MemoryStream outStream = new MemoryStream())
                //{
                //    using (ZipArchive archive = new ZipArchive(outStream, ZipArchiveMode.Create, true))
                //    {
                //        foreach (var project in projects)
                //        {
                //            var projectFile = new FileModel(this.service.GetProjectVersionHistory(project.Id));
                //            ZipArchiveEntry entry = archive.CreateEntry(projectFile.Title);
                //            using (var entryStream = entry.Open())
                //            {
                //                using (MemoryStream writer = new MemoryStream(projectFile.Bytes.ToArray()))
                //                {
                //                    writer.CopyTo(entryStream);
                //                }
                //            }
                //        }
                //    }
                //    result = new FileModel("ChangeLog.zip", outStream.ToArray());
                //}

                //return await Task.FromResult(result);
                throw new Exception();
            }
        }
    }
}