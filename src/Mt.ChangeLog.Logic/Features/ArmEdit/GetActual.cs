using MediatR;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using Mt.ChangeLog.DataContext;
using Mt.ChangeLog.Logic.Mappers;
using Mt.ChangeLog.TransferObjects.ArmEdit;

namespace Mt.ChangeLog.Logic.Features.ArmEdit;

/// <summary>
/// Получить актуальную версию ArmEdit.
/// </summary>
public static class GetActual
{
    /// <inheritdoc />
    public sealed class Query : IRequest<ArmEditModel>
    {
    }

    /// <inheritdoc />
    public sealed class Handler : IRequestHandler<Query, ArmEditModel>
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
        public Task<ArmEditModel> Handle(Query request, CancellationToken cancellationToken)
        {
            _logger.LogDebug("Получен запрос на предоставление данных об актуальном ArmEdit.");

            var result = _context.ArmEdits.AsNoTracking()
                .OrderByDescending(e => e.Version)
                .First()
                .ToModel();

            _logger.LogDebug("Запрос на получение данных об актуальном ArmEdit '{Result}' выполнен успешно.", result);
            return Task.FromResult(result);
        }
    }
}