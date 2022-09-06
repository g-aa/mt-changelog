﻿using MediatR;
using Microsoft.Extensions.Logging;
using Mt.ChangeLog.Logic.Models;
using Mt.ChangeLog.Repositories.Abstractions.Interfaces;
using Mt.ChangeLog.TransferObjects.AnalogModule;
using Mt.ChangeLog.TransferObjects.Other;
using Mt.Utilities;
using System.Threading.Tasks;
using System.Threading;

namespace Mt.ChangeLog.Logic.Features.AnalogModule
{
    /// <summary>
    /// Запрос на получение перечня моделий данных для таблиц <see cref="AnalogModuleModel"/>.
    /// </summary>
    public static class GetById
    {
        /// <inheritdoc />
        public sealed class Query : MtQuery<BaseModel, AnalogModuleModel>
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
                return $"{base.ToString()} - получение полной модели вида {nameof(AnalogModuleModel)}.";
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
            /// Репозиторий для доступа к данным.
            /// </summary>
            private readonly IAnalogModuleRepository repository;

            /// <summary>
            /// Инициализация нового экземпляра класса <see cref="Handler"/>.
            /// </summary>
            /// <param name="logger">Журнал логирования.</param>
            /// <param name="repository">Репозиторий для доступа к данным.</param>
            public Handler(ILogger<Handler> logger, IAnalogModuleRepository repository)
            {
                this.logger = Check.NotNull(logger, nameof(logger));
                this.repository = Check.NotNull(repository, nameof(repository));
            }

            /// <inheritdoc />
            public async Task<AnalogModuleModel> Handle(Query request, CancellationToken cancellationToken)
            {
                Check.NotNull(request, nameof(request));
                this.logger.LogInformation(request.ToString());
                var result = await this.repository.GetEntityAsync(request.Model.Id);
                return result;
            }
        }
    }
}