using MediatR;
using Microsoft.Extensions.Logging;
using Mt.ChangeLog.Logic.Models;
using Mt.ChangeLog.TransferObjects.ArmEdit;
using Mt.Utilities;

namespace Mt.ChangeLog.Logic.Features.ArmEdit
{
    /// <summary>
    /// Запрос на получение шаблона <see cref="ArmEditModel"/>.
    /// </summary>
    public static class GetTemplate
    {
        /// <inheritdoc />
        public sealed class Query : MtQuery<Unit, ArmEditModel>
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
                return $"{base.ToString()} - получение шаблона сущностей вида {nameof(ArmEditModel)}.";
            }
        }

        /// <inheritdoc />
        public sealed class Handler : IRequestHandler<Query, ArmEditModel>
        {
            /// <summary>
            /// Журнал логирования.
            /// </summary>
            private readonly ILogger<Handler> logger;

            /// <summary>
            /// Инициализация нового экземпляра класса <see cref="Handler"/>.
            /// </summary>
            /// <param name="logger">Журнал логирования.</param>
            public Handler(ILogger<Handler> logger)
            {
                this.logger = Check.NotNull(logger, nameof(logger));
            }

            /// <inheritdoc />
            public async Task<ArmEditModel> Handle(Query request, CancellationToken cancellationToken)
            {
                Check.NotNull(request, nameof(request));
                this.logger.LogInformation(request.ToString());
                return await Task.FromResult(new ArmEditModel());
            }
        }
    }
}