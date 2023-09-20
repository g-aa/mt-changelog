using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Mt.ChangeLog.DataContext;
using Mt.ChangeLog.Logic.Mappers;
using Mt.ChangeLog.TransferObjects.Other;
using Mt.ChangeLog.TransferObjects.ProjectRevision;
using Mt.Entities.Abstractions.Extensions;

namespace Mt.ChangeLog.Logic.Features.ProjectRevision;

/// <summary>
/// Запрос на обновление сущности <see cref="ProjectRevisionModel"/>.
/// </summary>
public static class Update
{
    /// <inheritdoc />
    public sealed record Command(Guid ProjectRevisionId, ProjectRevisionModel Model) : IRequest<MessageModel>
    {
    }

    /// <summary>
    /// Валидатор модели <see cref="Command"/>.
    /// </summary>
    public sealed class Validator : AbstractValidator<Command>
    {
        /// <summary>
        /// Инициализация экземпляра <see cref="Validator"/>.
        /// </summary>
        /// <param name="validator">Project revision model validator.</param>
        public Validator(IValidator<ProjectRevisionModel> validator)
        {
            this.RuleFor(e => e.ProjectRevisionId)
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
            this.logger.LogDebug("Получен запрос на обновление данных редакции проекта в системе.");

            var dbArmEdit = this.context.ArmEdits
                .SearchOrDefault(model.ArmEdit.Id);

            var dbAuthors = this.context.Authors
                .SearchManyOrDefault(model.Authors.Select(e => e.Id));

            var dbModule = this.context.Communications
                .Search(model.Communication.Id);

            var dbParent = this.context.ProjectRevisions
               .SearchOrNull(model!.ParentRevision!.Id);

            var dbAlgorithms = this.context.RelayAlgorithms
                .SearchManyOrDefault(model.RelayAlgorithms.Select(e => e.Id));

            var dbProjectRevision = this.context.ProjectRevisions
                .Include(e => e.ProjectVersion)
                .Include(e => e.Authors)
                .Include(e => e.RelayAlgorithms)
                .Search(model.Id)
                .GetBuilder()
                .SetAttributes(model)
                .SetArmEdit(dbArmEdit)
                .SetCommunication(dbModule)
                .SetAuthors(dbAuthors)
                .SetAlgorithms(dbAlgorithms)
                .SetParentRevision(dbParent)
                .Build();

            await this.context.SaveChangesAsync(cancellationToken);

            this.logger.LogInformation("Редакция проекта успешно обновлен в системе.");
            return new MessageModel
            {
                Message = $"'{dbProjectRevision}' обновлена в системе.",
            };
        }
    }
}