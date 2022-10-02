﻿using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Mt.ChangeLog.Context;
using Mt.ChangeLog.Entities.Extensions.Tables;
using Mt.ChangeLog.Logic.Models;
using Mt.ChangeLog.TransferObjects.Other;
using Mt.ChangeLog.TransferObjects.ProjectVersion;
using Mt.Entities.Abstractions.Extensions;
using Mt.Utilities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Mt.ChangeLog.Logic.Features.ProjectVersion
{
    /// <summary>
    /// Запрос на добавления сущности <see cref="ProjectVersionModel"/>.
    /// </summary>
    public static class Add
    {
        /// <inheritdoc />
        public sealed class Command : MtCommand<ProjectVersionModel, BaseModel>, IValidatedRequest
        {
            /// <summary>
            /// Инициализация нового экземпляра класса <see cref="Command"/>.
            /// </summary>
            /// <param name="model">Модель данных.</param>
            public Command(ProjectVersionModel model) : base(model)
            {
            }

            /// <inheritdoc />
            public override string ToString()
            {
                return $"{base.ToString()} - добавление сущности вида {nameof(ProjectVersionModel)}.";
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
            public CommandValidator(ProjectVersionModelValidator validator)
            {
                this.RuleFor(e => e.Model)
                    .SetValidator(Check.NotNull(validator, nameof(validator)));
            }
        }

        /// <inheritdoc />
        public sealed class Handler : IRequestHandler<Command, BaseModel>
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
            public async Task<BaseModel> Handle(Command request, CancellationToken cancellationToken)
            {
                var model = Check.NotNull(request, nameof(request)).Model;
                this.logger.LogInformation(request.ToString());

                var dbStatus = this.context.ProjectStatuses
                    .SearchOrDefault(model.ProjectStatus.Id);

                var dbPlatform = this.context.Platforms
                    .Include(e => e.AnalogModules)
                    .SearchOrDefault(model.Platform.Id);

                var dbAnalogModule = dbPlatform.AnalogModules
                    .Search(model.AnalogModule.Id);

                var dbProjectVersion = ProjectVersionBuilder.GetBuilder()
                    .SetAttributes(model)
                    .SetProjectStatus(dbStatus)
                    .SetPlatform(dbPlatform)
                    .SetAnalogModule(dbAnalogModule)
                    .Build();

                if (this.context.ProjectVersions.IsContained(dbProjectVersion))
                {
                    throw new ArgumentException($"Сущность '{dbProjectVersion}' уже содержится в системе.");
                }

                await this.context.ProjectVersions.AddAsync(dbProjectVersion);
                await this.context.SaveChangesAsync();

                return new BaseModel()
                {
                    Id = dbProjectVersion.Id,
                };
            }
        }
    }
}