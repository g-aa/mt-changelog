using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Mt.ChangeLog.DataContext;
using Mt.ChangeLog.Entities.Tables;
using Mt.ChangeLog.Logic.Mappers;
using Mt.ChangeLog.TransferObjects.Other;
using Mt.ChangeLog.TransferObjects.ProjectRevision;
using Mt.Entities.Abstractions.Extensions;
using Mt.Utilities.Exceptions;

namespace Mt.ChangeLog.Logic.Features.ProjectRevision;

/// <summary>
/// Запрос на добавления сущности <see cref="ProjectRevisionModel"/>.
/// </summary>
public static class Add
{
    /// <inheritdoc />
    public sealed record Command(ProjectRevisionModel Model) : IRequest<MessageModel>
    {
    }

    /// <inheritdoc />
    public sealed class Validator : AbstractValidator<Command>
    {
        /// <summary>
        /// Инициализация экземпляра <see cref="Validator"/>.
        /// </summary>
        /// <param name="validator">Project revision model validator.</param>
        public Validator(IValidator<ProjectRevisionModel> validator)
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
            this.logger.LogDebug("Получен запрос на добавление редакции проекта '{Model}' в систему.", model);

            var dbParent = this.context.ProjectRevisions
                .SearchOrNull(model.ParentRevision != null ? model.ParentRevision.Id : Guid.Empty);

            var dbProjectVersion = this.context.ProjectVersions
                .Search(model.ProjectVersion.Id);

            var dbArmEdit = this.context.ArmEdits
                .SearchOrDefault(model.ArmEdit.Id);

            var dbAuthors = this.context.Authors
                .SearchManyOrDefault(model.Authors.Select(e => e.Id));

            var dbModule = this.context.Communications
                .Search(model.Communication.Id);

            var dbAlgorithms = this.context.RelayAlgorithms
                .SearchManyOrDefault(model.RelayAlgorithms.Select(e => e.Id));

            var dbProjectRevision = new ProjectRevisionEntity().GetBuilder()
                .SetAttributes(model)
                .SetParentRevision(dbParent)
                .SetProjectVersion(dbProjectVersion)
                .SetArmEdit(dbArmEdit)
                .SetAuthors(dbAuthors)
                .SetCommunication(dbModule)
                .SetAlgorithms(dbAlgorithms)
                .Build();

            if (this.context.ProjectRevisions.Include(e => e.ProjectVersion).IsContained(dbProjectRevision))
            {
                throw new MtException(ErrorCode.EntityAlreadyExists, $"Сущность '{dbProjectRevision}' уже содержится в системе.");
            }

            return this.SaveChangesAsync(dbProjectRevision, cancellationToken);
        }

        /// <summary>
        /// Сохранить изменения сущности.
        /// </summary>
        /// <param name="entity">Сущность.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Результат выполнения.</returns>
        private async Task<MessageModel> SaveChangesAsync(ProjectRevisionEntity entity, CancellationToken cancellationToken)
        {
            await this.context.ProjectRevisions.AddAsync(entity, cancellationToken);
            await this.context.SaveChangesAsync(cancellationToken);

            this.logger.LogInformation("Редакция проекта '{Entity}' успешно добавлен в систему.", entity);
            return new MessageModel
            {
                Message = $"'{entity}' была добавлена в систему.",
            };
        }
    }
}