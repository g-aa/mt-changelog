using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Mt.ChangeLog.DataContext;
using Mt.ChangeLog.Logic.Mappers;
using Mt.ChangeLog.TransferObjects.Other;
using Mt.ChangeLog.TransferObjects.ProjectVersion;
using Mt.Entities.Abstractions.Extensions;

namespace Mt.ChangeLog.Logic.Features.ProjectVersion;

/// <summary>
/// Запрос на обновление сущности <see cref="ProjectVersionModel"/>.
/// </summary>
public static class Update
{
    /// <inheritdoc />
    public sealed record Command(Guid ProjectVersionId, ProjectVersionModel Model) : IRequest<MessageModel>
    {
    }

    /// <inheritdoc />
    public sealed class CommandValidator : AbstractValidator<Command>
    {
        /// <summary>
        /// Инициализация экземпляра <see cref="CommandValidator"/>.
        /// </summary>
        /// <param name="validator">Project version model validator.</param>
        public CommandValidator(IValidator<ProjectVersionModel> validator)
        {
            this.RuleFor(e => e.ProjectVersionId)
                .Must((command, id) => id == command.Model.Id)
                .WithMessage("Значение параметра '{PropertyName}' не равен значению идентификатора в модели из тела запроса.");

            this.RuleFor(e => e.Model).SetValidator(validator);
        }
    }

    /// <inheritdoc />
    public sealed class Handler : IRequestHandler<Command, MessageModel>
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
        public async Task<MessageModel> Handle(Command request, CancellationToken cancellationToken)
        {
            var model = request.Model;
            this.logger.LogDebug("Получен запрос на обновление данных версии проекта '{Model}' в системе.", model);

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
            await this.context.SaveChangesAsync(cancellationToken);

            this.logger.LogInformation("Версия проекта '{DbProjectVersion}' успешно обновлен в системе.", dbProjectVersion);
            return new MessageModel
            {
                Message = $"'{dbProjectVersion}' обновлен в системе.",
            };
        }
    }
}