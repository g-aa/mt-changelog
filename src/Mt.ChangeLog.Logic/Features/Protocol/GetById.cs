﻿using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Mt.ChangeLog.Context;
using Mt.ChangeLog.Entities.Extensions.Tables;
using Mt.ChangeLog.Logic.Models;
using Mt.ChangeLog.TransferObjects.Other;
using Mt.ChangeLog.TransferObjects.Protocol;
using Mt.Entities.Abstractions.Extensions;
using Mt.Utilities;
using System.Threading;
using System.Threading.Tasks;

namespace Mt.ChangeLog.Logic.Features.Protocol
{
    /// <summary>
    /// Запрос на получение перечня моделий данных для таблиц <see cref="ProtocolModel"/>.
    /// </summary>
    public static class GetById
    {
        /// <inheritdoc />
        public sealed class Query : MtQuery<BaseModel, ProtocolModel>
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
                return $"{base.ToString()} - получение сущности вида {nameof(ProtocolModel)}.";
            }
        }

        /// <summary>
        /// Валидатор модели <see cref="Query"/>.
        /// </summary>
        public sealed class QueryValidator : AbstractValidator<Query>
        {
            /// <summary>
            /// Инициализация экземпляра <see cref="QueryValidator"/>.
            /// </summary>
            public QueryValidator(BaseModelValidator validator)
            {
                this.RuleFor(e => e.Model)
                    .SetValidator(Check.NotNull(validator, nameof(validator)));
            }
        }

        /// <inheritdoc />
        public sealed class Handler : IRequestHandler<Query, ProtocolModel>
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
            public async Task<ProtocolModel> Handle(Query request, CancellationToken cancellationToken)
            {
                Check.NotNull(request, nameof(request));
                this.logger.LogInformation(request.ToString());

                var result = this.context.Protocols.AsNoTracking()
                    .Include(e => e.Communications)
                    .Search(request.Model.Id)
                    .ToModel();

                return result;
            }
        }
    }
}