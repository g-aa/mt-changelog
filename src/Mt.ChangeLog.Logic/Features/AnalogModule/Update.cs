using FluentValidation;

using MediatR;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using Mt.ChangeLog.DataContext;
using Mt.ChangeLog.Entities.Tables;
using Mt.ChangeLog.Logic.Extensions;
using Mt.ChangeLog.TransferObjects.AnalogModule;
using Mt.ChangeLog.TransferObjects.Other;
using Mt.Entities.Abstractions.Extensions;
using Mt.Utilities.Exceptions;

namespace Mt.ChangeLog.Logic.Features.AnalogModule;

/// <summary>
/// Запрос на обновление сущности <see cref="AnalogModuleModel"/>.
/// </summary>
public static class Update
{
    /// <inheritdoc />
    public sealed record Command(Guid AnalogModuleId, AnalogModuleModel Model) : IRequest<MessageModel>
    {
    }

    /// <inheritdoc />
    public sealed class Validator : AbstractValidator<Command>
    {
        /// <summary>
        /// Инициализация экземпляра <see cref="Validator"/>.
        /// </summary>
        /// <param name="validator">Analog module model validator.</param>
        public Validator(IValidator<AnalogModuleModel> validator)
        {
            RuleFor(e => e.AnalogModuleId)
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
            _logger.LogDebug("Получен запрос на обновление данных аналогового модуля '{Model}' в системе.", model);

            var dbAnalogModule = _context.AnalogModules
                .Include(e => e.Projects)
                .Include(e => e.Platforms)
                .Search(model.Id);

            if (dbAnalogModule.Default)
            {
                throw new MtException(ErrorCode.EntityCannotBeModified, $"Сущность по умолчанию '{dbAnalogModule}' не может быть обновлена.");
            }

            var dbPlatforms = _context.Platforms
                .SearchManyOrDefault(model.Platforms.Select(e => e.Id));
            dbAnalogModule.GetBuilder()
                .SetAttributes(model)
                .SetPlatforms(dbPlatforms)
                .Build();

            _context.AnalogModules.Update(dbAnalogModule);
            return SaveChangesAsync(dbAnalogModule, cancellationToken);
        }

        private async Task<MessageModel> SaveChangesAsync(AnalogModuleEntity entity, CancellationToken cancellationToken)
        {
            await _context.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Аналоговый модуль '{Entity}' успешно обновлен в системе.", entity);
            return new MessageModel
            {
                Message = $"'{entity}' обновлен в системе.",
            };
        }
    }
}