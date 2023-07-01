using MediatR;
using Microsoft.Extensions.Logging;
using Mt.ChangeLog.DataAccess.Abstractions;
using Mt.ChangeLog.Logic.Models;
using Mt.ChangeLog.TransferObjects.ArmEdit;
using Mt.Utilities;

namespace Mt.ChangeLog.Logic.Features.ArmEdit
{
    /// <summary>
    /// Запрос на получение перечня моделий данных для таблиц <see cref="ArmEditTableModel"/>.
    /// </summary>
    public static class GetTables
    {
        /// <inheritdoc />
        public sealed class Query : MtQuery<Unit, IEnumerable<ArmEditTableModel>>
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
                return $"{base.ToString()} - получение перечня сущностей вида {nameof(ArmEditTableModel)}.";
            }
        }

        /// <inheritdoc />
        public sealed class Handler : IRequestHandler<Query, IEnumerable<ArmEditTableModel>>
        {
            /// <summary>
            /// Журнал логирования.
            /// </summary>
            private readonly ILogger<Handler> logger;

            /// <summary>
            /// Репозиторий с данными.
            /// </summary>
            private readonly IArmEditRepository repository;

            /// <summary>
            /// Инициализация нового экземпляра класса <see cref="Handler"/>.
            /// </summary>
            /// <param name="logger">Журнал логирования.</param>
            /// <param name="repository">Репозиторий с данными.</param>
            public Handler(ILogger<Handler> logger, IArmEditRepository repository)
            {
                this.logger = Check.NotNull(logger, nameof(logger));
                this.repository = Check.NotNull(repository, nameof(repository));
            }

            /// <inheritdoc />
            public async Task<IEnumerable<ArmEditTableModel>> Handle(Query request, CancellationToken cancellationToken)
            {
                Check.NotNull(request, nameof(request));
                this.logger.LogInformation(request.ToString());

                var result = await this.repository.GetTableEntitiesAsync();
                return result.OrderByDescending(e => e.Version);
            }
        }
    }
}