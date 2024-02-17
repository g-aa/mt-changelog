using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Mt.ChangeLog.DataContext;
using Mt.ChangeLog.Logic.Mappers;
using Mt.ChangeLog.TransferObjects.Communication;

namespace Mt.ChangeLog.Logic.Features.Communication;

/// <summary>
/// Запрос на получение шаблона <see cref="CommunicationModel"/>.
/// </summary>
public static class GetTemplate
{
    /// <inheritdoc />
    public sealed class Query : IRequest<CommunicationModel>
    {
    }

    /// <inheritdoc />
    public sealed class Handler : IRequestHandler<Query, CommunicationModel>
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
        public async Task<CommunicationModel> Handle(Query request, CancellationToken cancellationToken)
        {
            _logger.LogDebug("Получен запрос на создание шаблона коммуникационного модуля.");

            var protocols = await _context.Protocols.AsNoTracking()
                .Where(e => e.Default)
                .Select(e => e.ToShortModel())
                .ToListAsync(cancellationToken);

            var model = new CommunicationModel
            {
                Protocols = protocols,
            };

            _logger.LogDebug("Запрос на создание шаблона коммуникационного модуля '{Model}' выполнен успешно.", model);
            return model;
        }
    }
}