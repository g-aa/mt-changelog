using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using Mt.ChangeLog.DataContext;
using Mt.ChangeLog.Entities.Tables;
using Mt.ChangeLog.Logic.Extensions;
using Mt.ChangeLog.TransferObjects.Author;
using Mt.ChangeLog.TransferObjects.Other;
using Mt.Entities.Abstractions.Extensions;
using Mt.Utilities.Exceptions;

namespace Mt.ChangeLog.Logic.Features.Author;

/// <summary>
/// Запрос на добавления сущности <see cref="AuthorModel"/>.
/// </summary>
public static class Add
{
    /// <inheritdoc />
    public sealed record Command(AuthorModel Model) : IRequest<MessageModel>
    {
    }

    /// <inheritdoc />
    public sealed class Validator : AbstractValidator<Command>
    {
        /// <summary>
        /// Инициализация экземпляра <see cref="Validator"/>.
        /// </summary>
        /// <param name="validator">Author model validator.</param>
        public Validator(IValidator<AuthorModel> validator)
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
            _logger.LogDebug("Получен запрос на добавление  автора '{Model}' в систему.", model);

            var dbAuthor = new AuthorEntity().GetBuilder()
                .SetAttributes(model)
                .Build();

            if (_context.Authors.IsContained(dbAuthor))
            {
                throw new MtException(ErrorCode.EntityAlreadyExists, $"Сущность '{dbAuthor}' уже содержится в системе.");
            }

            return SaveChangesAsync(dbAuthor, cancellationToken);
        }

        /// <summary>
        /// Сохранить изменения сущности.
        /// </summary>
        /// <param name="entity">Сущность.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Результат выполнения.</returns>
        private async Task<MessageModel> SaveChangesAsync(AuthorEntity entity, CancellationToken cancellationToken)
        {
            await _context.Authors.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Автор '{Entity}' успешно добавлен в систему.", entity);
            return new MessageModel
            {
                Message = $"'{entity}' был добавлен в систему.",
            };
        }
    }
}