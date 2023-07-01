using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Mt.ChangeLog.Context;
using Mt.ChangeLog.Entities.Extensions.Tables;
using Mt.ChangeLog.Logic.Models;
using Mt.ChangeLog.TransferObjects.RelayAlgorithm;
using Mt.Utilities;

namespace Mt.ChangeLog.Logic.Features.RelayAlgorithm
{
    /// <summary>
    /// Запрос на получение перечня моделий данных для таблиц <see cref="RelayAlgorithmTableModel"/>.
    /// </summary>
    public static class GetTables
    {
        /// <inheritdoc />
        public sealed class Query : MtQuery<Unit, IEnumerable<RelayAlgorithmTableModel>>
        {
            /// <summary>
            /// Инициализация нового экземпляра класса <see cref="Query"/>.
            /// </summary>
            public Query() : base(Unit.Value)
            {
            }

            /// <inheritdoc />
            public override string ToString()
            {
                return $"{base.ToString()} - получение перечня сущностей вида {nameof(RelayAlgorithmTableModel)}.";
            }
        }

        /// <inheritdoc />
        public sealed class Handler : IRequestHandler<Query, IEnumerable<RelayAlgorithmTableModel>>
        {
            /// <summary>
            /// Журнал логирования.
            /// </summary>
            private readonly ILogger<Handler> logger;

            /// <summary>
            /// Контекст данных.
            /// </summary>
            private readonly MtContext context;

            /// <summary>
            /// Инициализация нового экземпляра класса <see cref="Handler"/>.
            /// </summary>
            /// <param name="logger">Журнал логирования.</param>
            /// <param name="context">Контекст данных.</param>
            public Handler(ILogger<Handler> logger, MtContext context)
            {
                this.logger = Check.NotNull(logger, nameof(logger));
                this.context = Check.NotNull(context, nameof(context));
            }

            /// <inheritdoc />
            public async Task<IEnumerable<RelayAlgorithmTableModel>> Handle(Query request, CancellationToken cancellationToken)
            {
                Check.NotNull(request, nameof(request));
                this.logger.LogInformation(request.ToString());

                var result = await this.context.RelayAlgorithms.AsNoTracking()
                    .OrderBy(e => e.Group)
                    .ThenBy(e => e.Title)
                    .Select(e => e.ToModel())
                    .ToListAsync(cancellationToken);

                return result;
            }
        }
    }
}