using FluentValidation;

using MediatR;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using Mt.ChangeLog.DataContext;
using Mt.ChangeLog.Entities.Tables;
using Mt.ChangeLog.Logic.Extensions;
using Mt.ChangeLog.TransferObjects.Other;
using Mt.ChangeLog.TransferObjects.Platform;
using Mt.Entities.Abstractions.Extensions;
using Mt.Utilities.Exceptions;

namespace Mt.ChangeLog.Logic.Features.Platform;

/// <summary>
/// Запрос на обновление сущности <see cref="PlatformModel"/>.
/// </summary>
public static class Update
{
    /// <inheritdoc />
    public sealed record Command(Guid PlatformId, PlatformModel Model) : IRequest<MessageModel>
    {
    }

    /// <inheritdoc />
    public sealed class Validator : AbstractValidator<Command>
    {
        /// <summary>
        /// Инициализация экземпляра <see cref="Validator"/>.
        /// </summary>
        /// <param name="validator">Platform model validator.</param>
        public Validator(IValidator<PlatformModel> validator)
        {
            RuleFor(e => e.PlatformId)
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
        public Task<MessageModel> Handle(Command request, CancellationToken cancellationToken)
        {
            var model = request.Model;
            _logger.LogDebug("Получен запрос на обновление данных платформы '{Model}' в системе.", model);

            var dbPlatform = _context.Platforms
                .Include(e => e.Projects)
                .Include(e => e.AnalogModules)
                .Search(model.Id);

            if (dbPlatform.Default)
            {
                throw new MtException(ErrorCode.EntityCannotBeModified, $"Сущность по умолчанию '{dbPlatform}' не может быть обновлена.");
            }

            var dbAnalogModules = _context.AnalogModules
                .SearchManyOrDefault(model.AnalogModules.Select(e => e.Id));
            dbPlatform.GetBuilder()
                .SetAttributes(model)
                .SetAnalogModules(dbAnalogModules)
                .Build();

            _context.Platforms.Update(dbPlatform);
            return SaveChangesAsync(dbPlatform, cancellationToken);
        }

        private async Task<MessageModel> SaveChangesAsync(PlatformEntity entity, CancellationToken cancellationToken)
        {
            await _context.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Платформа '{Entity}' успешно обновлен в системе.", entity);
            return new MessageModel
            {
                Message = $"'{entity}' обновлена в системе.",
            };
        }
    }
}