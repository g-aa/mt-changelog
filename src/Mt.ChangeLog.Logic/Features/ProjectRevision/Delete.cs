﻿using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Mt.ChangeLog.Context;
using Mt.ChangeLog.Logic.Models;
using Mt.ChangeLog.TransferObjects.Other;
using Mt.ChangeLog.TransferObjects.ProjectRevision;
using Mt.Entities.Abstractions.Extensions;
using Mt.Utilities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Mt.ChangeLog.Logic.Features.ProjectRevision
{
    /// <summary>
    /// Запрос на удаления модели аналогового модуля из системы <see cref="ProjectRevisionModel"/>.
    /// </summary>
    public static class Delete
    {
        /// <inheritdoc />
        public sealed class Command : MtCommand<BaseModel, StatusModel>
        {
            /// <summary>
            /// Инициализация нового экземпляра класса <see cref="Command"/>.
            /// </summary>
            /// <param name="model">Базовая модель.</param>
            public Command(BaseModel model) : base(model)
            {
            }

            /// <inheritdoc />
            public override string ToString()
            {
                return $"{base.ToString()} - удаление сущности вида {nameof(ProjectRevisionModel)}.";
            }
        }

        /// <summary>
        /// Валидатор модели <see cref="Command"/>.
        /// </summary>
        public sealed class CommandValidator : AbstractValidator<Command>
        {
            /// <summary>
            /// Инициализация экземпляра <see cref="CommandValidator"/>.
            /// </summary>
            public CommandValidator(BaseModelValidator validator)
            {
                this.RuleFor(e => e.Model)
                    .SetValidator(Check.NotNull(validator, nameof(validator)));
            }
        }

        /// <inheritdoc />
        public sealed class Handler : IRequestHandler<Command, StatusModel>
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
            public async Task<StatusModel> Handle(Command request, CancellationToken cancellationToken)
            {
                Check.NotNull(request, nameof(request));
                this.logger.LogInformation(request.ToString());

                var dbRemovable = this.context.ProjectRevisions
                    .Include(e => e.ProjectVersion)
                    .Search(request.Model.Id);

                throw new NotImplementedException($"Удаление редакций проекта '{dbRemovable}' не доступно, поддерживается только функционал по удалению версии проекта со всеми редакциями");
            }
        }
    }
}