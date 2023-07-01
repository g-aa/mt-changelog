using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Mt.ChangeLog.Context;
using Mt.ChangeLog.Entities.Extensions.Tables;
using Mt.ChangeLog.Logic.Models;
using Mt.ChangeLog.TransferObjects.ProjectVersion;
using Mt.Entities.Abstractions.Extensions;
using Mt.Utilities;

namespace Mt.ChangeLog.Logic.Features.ProjectVersion
{
    /// <summary>
    /// Запрос на обновление сущности <see cref="ProjectVersionModel"/>.
    /// </summary>
    public static class Update
    {
        /// <inheritdoc />
        public sealed class Command : MtCommand<ProjectVersionModel, string>, IValidatedRequest
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
                return $"{base.ToString()} - обновление сущности вида {nameof(ProjectVersionModel)}.";
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
        public sealed class Handler : IRequestHandler<Command, string>
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
            public async Task<string> Handle(Command request, CancellationToken cancellationToken)
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

                var dbProjectVersion = this.context.ProjectVersions.Search(model.Id)
                    .GetBuilder()
                    .SetAttributes(model)
                    .SetProjectStatus(dbStatus)
                    .SetPlatform(dbPlatform)
                    .SetAnalogModule(dbAnalogModule)
                    .Build();

                this.context.ProjectVersions.Update(dbProjectVersion);
                await this.context.SaveChangesAsync();

                return $"'{dbProjectVersion}' обновлен в системе.";
            }
        }
    }
}