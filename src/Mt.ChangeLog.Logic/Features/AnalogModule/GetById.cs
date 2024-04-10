using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Mt.ChangeLog.DataContext;
using Mt.ChangeLog.Logic.Mappers;
using Mt.ChangeLog.TransferObjects.AnalogModule;
using Mt.ChangeLog.TransferObjects.Other;
using Mt.Entities.Abstractions.Extensions;

namespace Mt.ChangeLog.Logic.Features.AnalogModule;

/// <summary>
/// Запрос на получение перечня моделей данных для таблиц <see cref="AnalogModuleModel"/>.
/// </summary>
public static class GetById
{
    /// <inheritdoc />
    public sealed record Query(BaseModel Model) : IRequest<AnalogModuleModel>
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
    public sealed class Handler : IRequestHandler<Query, AnalogModuleModel>
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
        public Task<AnalogModuleModel> Handle(Query request, CancellationToken cancellationToken)
        {
            var model = request.Model;
            _logger.LogDebug("Получен запрос на предоставление данных об аналоговом модуле '{Model}'.", model);

            var result = _context.AnalogModules.AsNoTracking()
                .Include(e => e.Platforms)
                .Search(model.Id)
                .ToModel();

            _logger.LogDebug("Запрос на получение данных об аналоговом модуле '{Result}' выполнен успешно.", result);
            return Task.FromResult(result);
        }
    }
}