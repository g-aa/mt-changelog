using MediatR;
using Microsoft.Extensions.Logging;
using Mt.ChangeLog.Logic.Models;
using Mt.ChangeLog.TransferObjects.ProjectStatus;
using Mt.Utilities;

namespace Mt.ChangeLog.Logic.Features.ProjectStatus
{
    /// <summary>
    /// Запрос на получение шаблона <see cref="ProjectStatusModel"/>.
    /// </summary>
    public static class GetTemplate
    {
        /// <inheritdoc />
        public sealed class Query : MtQuery<Unit, ProjectStatusModel>
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
                return $"{base.ToString()} - получение шаблона сущности вида {nameof(ProjectStatusModel)}.";
            }
        }

        /// <inheritdoc />
        public sealed class Handler : IRequestHandler<Query, ProjectStatusModel>
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
            public async Task<ProjectStatusModel> Handle(Query request, CancellationToken cancellationToken)
            {
                Check.NotNull(request, nameof(request));
                this.logger.LogInformation(request.ToString());
                return await Task.FromResult(new ProjectStatusModel());
            }
        }
    }
}