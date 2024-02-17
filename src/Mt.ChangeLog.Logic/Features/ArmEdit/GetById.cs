using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using Mt.ChangeLog.DataAccess.Abstraction;
using Mt.ChangeLog.TransferObjects.ArmEdit;
using Mt.ChangeLog.TransferObjects.Other;

namespace Mt.ChangeLog.Logic.Features.ArmEdit;

/// <summary>
/// Запрос на получение перечня моделей данных для таблиц <see cref="ArmEditModel"/>.
/// </summary>
public static class GetById
{
    /// <inheritdoc />
    public sealed record Query(BaseModel Model) : IRequest<ArmEditModel>
    {
    }

    /// <inheritdoc />
    public sealed class Validator : AbstractValidator<Query>
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
    public sealed class Handler : IRequestHandler<Query, ArmEditModel>
    {
        private readonly ILogger<Handler> _logger;

        private readonly IArmEditRepository _repository;

        /// <summary>
        /// Инициализация нового экземпляра класса <see cref="Handler"/>.
        /// </summary>
        /// <param name="logger">Журнал логирования.</param>
        /// <param name="repository">Репозиторий с данными.</param>
        public Handler(ILogger<Handler> logger, IArmEditRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        /// <inheritdoc />
        public async Task<ArmEditModel> Handle(Query request, CancellationToken cancellationToken)
        {
            var model = request.Model;
            _logger.LogDebug("Получен запрос на предоставление данных об ArmEdit '{Model}'.", model);

            var result = await _repository.GetEntityAsync(model.Id);

            _logger.LogDebug("Запрос на получение данных об ArmEdit '{Result}' выполнен успешно.", result);
            return result;
        }
    }
}