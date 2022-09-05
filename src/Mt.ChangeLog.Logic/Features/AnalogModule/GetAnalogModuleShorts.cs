using MediatR;
using Microsoft.Extensions.Logging;
using Mt.ChangeLog.Logic.Models;
using Mt.ChangeLog.TransferObjects.AnalogModule;
using Mt.Utilities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using Mt.ChangeLog.Repositories.Abstractions.Interfaces;

namespace Mt.ChangeLog.Logic.Features.AnalogModule
{
    /// <summary>
    /// Запрос на получение перечня кратких моделий данных <see cref="AnalogModuleShortModel"/>.
    /// </summary>
    public static class GetAnalogModuleShorts
    {
        /// <inheritdoc />
        public sealed class Command : MtQuery<Unit, IEnumerable<AnalogModuleShortModel>>
        {
            /// <summary>
            /// Инициализация нового экземпляра класса <see cref="Command"/>.
            /// </summary>
            /// <param name="unit">Тип void.</param>
            public Command(Unit unit) : base(unit)
            {
            }
            
            /// <inheritdoc />
            public override string ToString()
            {
                return $"{base.ToString()} - получение перечня моделей вида {nameof(AnalogModuleShortModel)}.";
            }
        }

        /// <inheritdoc />
        public sealed class Handler : IRequestHandler<Command, IEnumerable<AnalogModuleShortModel>>
        {
            /// <summary>
            /// Журнал логирования.
            /// </summary>
            private readonly ILogger<Handler> logger;

            /// <summary>
            /// Репозиторий для доступа к данным.
            /// </summary>
            private readonly IAnalogModuleRepository repository;

            /// <summary>
            /// Инициализация нового экземпляра класса <see cref="Handler"/>.
            /// </summary>
            /// <param name="logger">Журнал логирования.</param>
            /// <param name="repository">Репозиторий для доступа к данным.</param>
            public Handler(ILogger<Handler> logger, IAnalogModuleRepository repository)
            {
                this.logger = Check.NotNull(logger, nameof(logger));
                this.repository = Check.NotNull(repository, nameof(repository));
            }

            /// <inheritdoc />
            public async Task<IEnumerable<AnalogModuleShortModel>> Handle(Command request, CancellationToken cancellationToken)
            {
                Check.NotNull(request, nameof(request));
                this.logger.LogInformation(request.ToString());
                var result = await this.repository.GetShortEntitiesAsync();
                return result;
            }
        }
    }
}