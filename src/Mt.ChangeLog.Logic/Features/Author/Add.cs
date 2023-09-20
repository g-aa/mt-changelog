using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using Mt.ChangeLog.DataContext;
using Mt.ChangeLog.Entities.Tables;
using Mt.ChangeLog.Logic.Mappers;
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
            this.logger.LogDebug("Получен запрос на добавление  автора '{LastName}' '{FirstName}' в систему.", model.LastName, model.FirstName);

            var dbAuthor = new AuthorEntity().GetBuilder()
                .SetAttributes(model)
                .Build();

            if (this.context.Authors.IsContained(dbAuthor))
            {
                throw new MtException(ErrorCode.EntityAlreadyExists, $"Сущность '{dbAuthor}' уже содержится в системе.");
            }

            return this.SaveChangesAsync(dbAuthor, cancellationToken);
        }

        /// <summary>
        /// Сохранить изменения сущности.
        /// </summary>
        /// <param name="entity">Сущность.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Результат выполнения.</returns>
        private async Task<MessageModel> SaveChangesAsync(AuthorEntity entity, CancellationToken cancellationToken)
        {
            await this.context.Authors.AddAsync(entity, cancellationToken);
            await this.context.SaveChangesAsync(cancellationToken);

            this.logger.LogInformation("Автор '{LastName}' '{FirstName}' успешно добавлен в систему.", entity.LastName, entity.FirstName);
            return new MessageModel
            {
                Message = $"'{entity}' был добавлен в систему.",
            };
        }
    }
}