﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Mt.ChangeLog.Context;
using Mt.ChangeLog.Entities.Extensions.Tables;
using Mt.ChangeLog.Logic.Models;
using Mt.ChangeLog.TransferObjects.AnalogModule;
using Mt.Utilities;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

namespace Mt.ChangeLog.Logic.Features.AnalogModule
{
    /// <summary>
    /// Запрос на получение шаблона <see cref="AnalogModuleModel"/>.
    /// </summary>
    public static class GetTemplate
    {
        /// <inheritdoc />
        public sealed class Query : MtQuery<Unit, AnalogModuleModel>
        {
            /// <summary>
            /// Инициализация нового экземпляра класса <see cref="Query"/>.
            /// </summary>
            /// <param name="unit">Тип void.</param>
            public Query(Unit unit) : base(unit)
            {
            }

            /// <inheritdoc />
            public override string ToString()
            {
                return $"{base.ToString()} - получение шаблона модели вида {nameof(AnalogModuleModel)}.";
            }
        }

        /// <inheritdoc />
        public sealed class Handler : IRequestHandler<Query, AnalogModuleModel>
        {
            /// <summary>
            /// Журнал логирования.
            /// </summary>
            private readonly ILogger<Handler> logger;

            /// <summary>
            /// Контекст данных.
            /// </summary>
            private readonly ApplicationContext context;

            /// <summary>
            /// Инициализация нового экземпляра класса <see cref="Handler"/>.
            /// </summary>
            /// <param name="logger">Журнал логирования.</param>
            /// <param name="context">Контекст данных.</param>
            public Handler(ILogger<Handler> logger, ApplicationContext context)
            {
                this.logger = Check.NotNull(logger, nameof(logger));
                this.context = Check.NotNull(context, nameof(context));
            }

            /// <inheritdoc />
            public async Task<AnalogModuleModel> Handle(Query request, CancellationToken cancellationToken)
            {
                Check.NotNull(request, nameof(request));
                this.logger.LogInformation(request.ToString());

                var platforms = this.context.Platforms.AsNoTracking()
                    .Where(p => p.Default)
                    .Select(p => p.ToShortModel());

                return await Task.FromResult(new AnalogModuleModel()
                {
                    Platforms = platforms
                });
            }
        }
    }
}