using FluentValidation;

using MediatR;

using Microsoft.Extensions.Logging;

using Mt.ChangeLog.DataContext;
using Mt.ChangeLog.TransferObjects.Other;
using Mt.ChangeLog.TransferObjects.ProjectVersion;
using Mt.Entities.Abstractions.Extensions;

namespace Mt.ChangeLog.Logic.Features.ProjectVersion;

/// <summary>
/// Запрос на удаления модели аналогового модуля из системы <see cref="ProjectVersionModel"/>.
/// </summary>
public static class Delete
{
    /// <inheritdoc />
    public sealed record Command(BaseModel Model) : IRequest<MessageModel>
    {
    }

    /// <inheritdoc />
    public sealed class CommandValidator : AbstractValidator<Command>
    {
        /// <summary>
        /// Инициализация экземпляра <see cref="CommandValidator"/>.
        /// </summary>
        /// <param name="validator">Base model validator.</param>
        public CommandValidator(IValidator<BaseModel> validator)
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
        public async Task<MessageModel> Handle(Command request, CancellationToken cancellationToken)
        {
            var model = request.Model;
            _logger.LogDebug("Получен запрос на удаление версии проекта '{Model}' из системы.", model);

            var dbRemovable = _context.ProjectVersions.Search(model.Id);
            _context.ProjectVersions.Remove(dbRemovable);
            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Статус проекта '{DbRemovable}' успешно удален из системы.", dbRemovable);
            return new MessageModel
            {
                Message = $"'{dbRemovable}' был удалена из системы.",
            };
        }
    }
}