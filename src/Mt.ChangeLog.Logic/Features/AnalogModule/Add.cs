using FluentValidation;
using MediatR;
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
/// Запрос на добавления сущности <see cref="AnalogModuleModel"/>.
/// </summary>
public static class Add
{
    /// <inheritdoc />
    public sealed record Command(AnalogModuleModel Model) : IRequest<MessageModel>
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
            _logger.LogDebug("Получен запрос на добавление аналогового модуля '{Model}' в систему.", model);

            var dbPlatforms = _context.Platforms.SearchManyOrDefault(model.Platforms.Select(e => e.Id));

            var dbAnalogModule = new AnalogModuleEntity().GetBuilder()
                .SetAttributes(model)
                .SetPlatforms(dbPlatforms)
                .Build();

            if (_context.AnalogModules.IsContained(dbAnalogModule))
            {
                throw new MtException(ErrorCode.EntityAlreadyExists, $"Сущность '{dbAnalogModule}' уже содержится в системе.");
            }

            _context.AnalogModules.Add(dbAnalogModule);
            return SaveChangesAsync(dbAnalogModule, cancellationToken);
        }

        private async Task<MessageModel> SaveChangesAsync(AnalogModuleEntity entity, CancellationToken cancellationToken)
        {
            await _context.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Аналоговый модуль '{Entity}' успешно добавлен в систему.", entity);
            return new MessageModel
            {
                Message = $"'{entity}' был добавлен в систему.",
            };
        }
    }
}