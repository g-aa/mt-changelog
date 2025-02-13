using FluentValidation;

using MediatR;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using Mt.ChangeLog.DataContext;
using Mt.ChangeLog.Logic.Mappers;
using Mt.ChangeLog.TransferObjects.Other;
using Mt.ChangeLog.TransferObjects.Protocol;
using Mt.Entities.Abstractions.Extensions;

namespace Mt.ChangeLog.Logic.Features.Protocol;

/// <summary>
/// Запрос на получение перечня моделей данных для таблиц <see cref="ProtocolModel"/>.
/// </summary>
public static class GetById
{
    /// <inheritdoc />
    public sealed record Query(BaseModel Model) : IRequest<ProtocolModel>
    {
    }

    /// <inheritdoc />
    public sealed class QueryValidator : AbstractValidator<Query>
    {
        /// <summary>
        /// Инициализация экземпляра <see cref="QueryValidator"/>.
        /// </summary>
        /// <param name="validator">Base model validator.</param>
        public QueryValidator(IValidator<BaseModel> validator)
        {
            RuleFor(e => e.Model).SetValidator(validator);
        }
    }

    /// <inheritdoc />
    public sealed class Handler : IRequestHandler<Query, ProtocolModel>
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
        public Task<ProtocolModel> Handle(Query request, CancellationToken cancellationToken)
        {
            var model = request.Model;
            _logger.LogDebug("Получен запрос на предоставление данных об протоколе '{Model}'.", model);

            var result = _context.Protocols.AsNoTracking()
                .Include(e => e.Communications)
                .Search(model.Id)
                .ToModel();

            _logger.LogDebug("Запрос на получение данных об протоколе '{Result}' выполнен успешно.", result);
            return Task.FromResult(result);
        }
    }
}