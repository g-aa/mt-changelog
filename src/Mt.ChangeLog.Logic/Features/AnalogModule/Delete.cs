using FluentValidation;

using MediatR;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using Mt.ChangeLog.DataContext;
using Mt.ChangeLog.Entities.Tables;
using Mt.ChangeLog.TransferObjects.AnalogModule;
using Mt.ChangeLog.TransferObjects.Other;
using Mt.Entities.Abstractions.Extensions;
using Mt.Utilities.Exceptions;

namespace Mt.ChangeLog.Logic.Features.AnalogModule;

/// <summary>
/// Запрос на удаления модели аналогового модуля из системы <see cref="AnalogModuleModel"/>.
/// </summary>
public static class Delete
{
    /// <inheritdoc />
    public sealed record Command(BaseModel Model) : IRequest<MessageModel>
    {
    }

    /// <inheritdoc />
    public sealed class Validator : AbstractValidator<Command>
    {
        /// <summary>
        /// Инициализация экземпляра <see cref="Validator"/>.
        /// </summary>
        /// <param name="validator">Base model validator.</param>
        public Validator(IValidator<BaseModel> validator)
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
            _logger.LogDebug("Получен запрос на удаление аналогового модуля '{Model}' из системы.", model);

            var dbRemovable = _context.AnalogModules
                .Include(e => e.Projects)
                .Include(e => e.Platforms).ThenInclude(e => e.AnalogModules)
                .AsSingleQuery()
                .Search(model.Id);

            if (dbRemovable.Default)
            {
                throw new MtException(ErrorCode.EntityCannotBeDeleted, $"Сущность по умолчанию '{dbRemovable}' не может быть удалена из системы.");
            }

            if (dbRemovable.Projects.Count != 0)
            {
                throw new MtException(ErrorCode.EntityCannotBeDeleted, $"Сущность '{dbRemovable}' используемая в проектах не может быть удалена из системы.");
            }

            if (dbRemovable.Platforms.Count != 0)
            {
                var defModule = _context.AnalogModules.First(e => e.Default);
                foreach (var dbPlatform in dbRemovable.Platforms.Where(p => p.AnalogModules.Remove(dbRemovable) && p.AnalogModules.Count == 0))
                {
                    dbPlatform.AnalogModules.Add(defModule);
                }
            }

            _context.AnalogModules.Remove(dbRemovable);
            return SaveChangesAsync(dbRemovable, cancellationToken);
        }

        private async Task<MessageModel> SaveChangesAsync(AnalogModuleEntity entity, CancellationToken cancellationToken)
        {
            await _context.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Аналоговый модуль '{Entity}' успешно удален из системы.", entity);
            return new MessageModel
            {
                Message = $"'{entity}' был удален из системы.",
            };
        }
    }
}