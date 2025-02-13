using FluentValidation;

using MediatR;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using Mt.ChangeLog.DataContext;
using Mt.ChangeLog.Entities.Tables;
using Mt.ChangeLog.TransferObjects.Other;
using Mt.ChangeLog.TransferObjects.Platform;
using Mt.Entities.Abstractions.Extensions;
using Mt.Utilities.Exceptions;

namespace Mt.ChangeLog.Logic.Features.Platform;

/// <summary>
/// Запрос на удаления модели аналогового модуля из системы <see cref="PlatformModel"/>.
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
            _logger.LogDebug("Получен запрос на удаление платформы '{Model}' из системы.", model);

            var dbRemovable = _context.Platforms
                .Include(e => e.Projects)
                .Include(e => e.AnalogModules).ThenInclude(e => e.Platforms)
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

            if (dbRemovable.AnalogModules.Count != 0)
            {
                var defPlatform = _context.Platforms.First(e => e.Default);
                foreach (var dbModule in dbRemovable.AnalogModules.Where(am => am.Platforms.Remove(dbRemovable) && am.Platforms.Count == 0))
                {
                    dbModule.Platforms.Add(defPlatform);
                }
            }

            _context.Platforms.Remove(dbRemovable);
            return SaveChangesAsync(dbRemovable, cancellationToken);
        }

        private async Task<MessageModel> SaveChangesAsync(PlatformEntity entity, CancellationToken cancellationToken)
        {
            await _context.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Платформа '{Entity}' успешно удален из системы.", entity);
            return new MessageModel
            {
                Message = $"'{entity}' была удалена из системы.",
            };
        }
    }
}