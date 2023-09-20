using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Mt.ChangeLog.DataContext;
using Mt.ChangeLog.Entities.Tables;
using Mt.ChangeLog.Logic.Mappers;
using Mt.ChangeLog.TransferObjects.Other;
using Mt.ChangeLog.TransferObjects.ProjectVersion;
using Mt.Entities.Abstractions.Extensions;
using Mt.Utilities.Exceptions;

namespace Mt.ChangeLog.Logic.Features.ProjectVersion;

/// <summary>
/// Запрос на добавления сущности <see cref="ProjectVersionModel"/>.
/// </summary>
public static class Add
{
    /// <inheritdoc />
    public sealed record Command(ProjectVersionModel Model) : IRequest<MessageModel>
    {
    }

    /// <inheritdoc />
    public sealed class Validator : AbstractValidator<Command>
    {
        /// <summary>
        /// Инициализация экземпляра <see cref="Validator"/>.
        /// </summary>
        /// <param name="validator">Project version model validator.</param>
        public Validator(IValidator<ProjectVersionModel> validator)
        {
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
        public Task<MessageModel> Handle(Command request, CancellationToken cancellationToken)
        {
            var model = request.Model;
            this.logger.LogDebug("Получен запрос на добавление версии проекта '{Model}' в систему.", model);

            var dbStatus = this.context.ProjectStatuses
                .SearchOrDefault(model.ProjectStatus.Id);

            var dbPlatform = this.context.Platforms
                .Include(e => e.AnalogModules)
                .SearchOrDefault(model.Platform.Id);

            var dbAnalogModule = dbPlatform.AnalogModules
                .Search(model.AnalogModule.Id);

            var dbProjectVersion = new ProjectVersionEntity().GetBuilder()
                .SetAttributes(model)
                .SetProjectStatus(dbStatus)
                .SetPlatform(dbPlatform)
                .SetAnalogModule(dbAnalogModule)
                .Build();

            if (this.context.ProjectVersions.IsContained(dbProjectVersion))
            {
                throw new MtException(ErrorCode.EntityAlreadyExists, $"Сущность '{dbProjectVersion}' уже содержится в системе.");
            }

            return this.SaveChangesAsync(dbProjectVersion, cancellationToken);
        }

        /// <summary>
        /// Сохранить изменения сущности.
        /// </summary>
        /// <param name="entity">Сущность.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Результат выполнения.</returns>
        private async Task<MessageModel> SaveChangesAsync(ProjectVersionEntity entity, CancellationToken cancellationToken)
        {
            await this.context.ProjectVersions.AddAsync(entity, cancellationToken);
            await this.context.SaveChangesAsync(cancellationToken);

            this.logger.LogInformation("Версия проекта '{Entity}' успешно добавлен в систему.", entity);
            return new MessageModel
            {
                Message = $"'{entity}' была добавлена в систему.",
            };
        }
    }
}