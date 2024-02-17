using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Mt.ChangeLog.DataContext;
using Mt.ChangeLog.Logic.Extensions;
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
            RuleFor(e => e.ProjectRevisionId)
                .Must((command, id) => id == command.Model.Id)
                .WithMessage("Значение параметра '{PropertyName}' не равен значению идентификатора в модели из тела запроса.");

            RuleFor(e => e.Model).SetValidator(validator);
        }
    }

    /// <inheritdoc />
    public sealed class Handler : IRequestHandler<Command, MessageModel>
    {
        private readonly ILogger<Handler> _logger;

        private readonly MtContext _context;

        /// <summary>
        /// Инициализация нового экземпляра класса <see cref="Handler"/>.
        /// </summary>
        /// <param name="logger">Журнал логирования.</param>
        /// <param name="context">Контекст данных.</param>
        public Handler(ILogger<Handler> logger, MtContext context)
        {
            _logger = logger;
            _context = context;
        }

        /// <inheritdoc />
        public async Task<MessageModel> Handle(Command request, CancellationToken cancellationToken)
        {
            var model = request.Model;
            _logger.LogDebug("Получен запрос на обновление данных редакции проекта в системе.");

            var dbArmEdit = _context.ArmEdits
                .SearchOrDefault(model.ArmEdit.Id);

            var dbAuthors = _context.Authors
                .SearchManyOrDefault(model.Authors.Select(e => e.Id));

            var dbModule = _context.Communications
                .Search(model.Communication.Id);

            var dbParent = _context.ProjectRevisions
               .SearchOrNull(model!.ParentRevision!.Id);

            var dbAlgorithms = _context.RelayAlgorithms
                .SearchManyOrDefault(model.RelayAlgorithms.Select(e => e.Id));

            var dbProjectRevision = _context.ProjectRevisions
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

            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Редакция проекта успешно обновлен в системе.");
            return new MessageModel
            {
                Message = $"'{dbProjectRevision}' обновлена в системе.",
            };
        }
    }
}