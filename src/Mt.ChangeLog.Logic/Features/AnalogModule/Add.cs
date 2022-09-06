﻿using MediatR;
using Microsoft.Extensions.Logging;
using Mt.Entities.Abstractions.Extensions;
using Mt.ChangeLog.Context;
using Mt.ChangeLog.Entities.Extensions.Tables;
using Mt.ChangeLog.Logic.Models;
using Mt.ChangeLog.TransferObjects.AnalogModule;
using Mt.Utilities;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using FluentValidation;

namespace Mt.ChangeLog.Logic.Features.AnalogModule
{
    /// <summary>
    /// Запрос на добавления сущности <see cref="AnalogModuleModel"/>.
    /// </summary>
    public static class Add
    {
        /// <inheritdoc />
        public sealed class Command : MtCommand<AnalogModuleModel, Unit>
        {
            /// <summary>
            /// Инициализация нового экземпляра класса <see cref="Command"/>.
            /// </summary>
            /// <param name="model">Базовая модель.</param>
            public Command(AnalogModuleModel model) : base(model)
            {
            }

            /// <inheritdoc />
            public override string ToString()
            {
                return $"{base.ToString()} - добавление сущности вида {nameof(AnalogModuleModel)}.";
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
            public CommandValidator(AnalogModuleModelValidator validator)
            {
                this.RuleFor(e => e.Model)
                    .SetValidator(Check.NotNull(validator, nameof(validator)));
            }
        }

        /// <inheritdoc />
        public sealed class Handler : IRequestHandler<Command, Unit>
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
            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                Check.NotNull(request, nameof(request));
                this.logger.LogInformation(request.ToString());

                var dbPlatforms = this.context.Platforms
                .SearchManyOrDefault(request.Model.Platforms.Select(e => e.Id));
                
                var dbAnalogModule = AnalogModuleBuilder.GetBuilder()
                    .SetAttributes(request.Model)
                    .SetPlatforms(dbPlatforms)
                    .Build();

                if (this.context.AnalogModules.IsContained(dbAnalogModule))
                {
                    throw new ArgumentException($"Сущность \"{request.Model}\" уже содержится в БД");
                }

                await this.context.AnalogModules.AddAsync(dbAnalogModule);
                await this.context.SaveChangesAsync();

                return Unit.Value;
            }
        }
    }
}