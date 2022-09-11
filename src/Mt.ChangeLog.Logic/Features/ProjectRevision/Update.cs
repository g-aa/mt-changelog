using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Mt.ChangeLog.Context;
using Mt.ChangeLog.Entities.Extensions.Tables;
using Mt.ChangeLog.Logic.Models;
using Mt.ChangeLog.TransferObjects.Other;
using Mt.ChangeLog.TransferObjects.ProjectRevision;
using Mt.Entities.Abstractions.Extensions;
using Mt.Utilities;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Mt.ChangeLog.Logic.Features.ProjectRevision
{
    /// <summary>
    /// Запрос на обновление сущности <see cref="ProjectRevisionModel"/>.
    /// </summary>
    public static class Update
    {
        /// <inheritdoc />
        public sealed class Command : MtCommand<ProjectRevisionModel, StatusModel>
        {
            /// <summary>
            /// Инициализация нового экземпляра класса <see cref="Command"/>.
            /// </summary>
            /// <param name="model">Модель данных.</param>
            public Command(ProjectRevisionModel model) : base(model)
            {
            }

            /// <inheritdoc />
            public override string ToString()
            {
                return $"{base.ToString()} - обновление сущности вида {nameof(ProjectRevisionModel)}.";
            }
        }

        /// <summary>
        /// Валидатор модели <see cref="Command"/>.
        /// </summary>
        public sealed class CommandValidator : AbstractValidator<Command>
        {
            /// <summary>
            /// Инициализация экземпляра <see cref="CommandValidator"/>.
            /// </summary>
            public CommandValidator(ProjectRevisionModelValidator validator)
            {
                this.RuleFor(e => e.Model)
                    .SetValidator(Check.NotNull(validator, nameof(validator)));
            }
        }

        /// <inheritdoc />
        public sealed class Handler : IRequestHandler<Command, StatusModel>
        {
            /// <summary>
            /// Журнал логирования.
            /// </summary>
            private readonly ILogger<Handler> logger;

            /// <summary>
            /// Контекст данных.
            /// </summary>
            private readonly ApplicationContext context;

            /// <summary>
            /// Инициализация нового экземпляра класса <see cref="Handler"/>.
            /// </summary>
            /// <param name="logger">Журнал логирования.</param>
            /// <param name="context">Контекст данных.</param>
            public Handler(ILogger<Handler> logger, ApplicationContext context)
            {
                this.logger = Check.NotNull(logger, nameof(logger));
                this.context = Check.NotNull(context, nameof(context));
            }

            /// <inheritdoc />
            public async Task<StatusModel> Handle(Command request, CancellationToken cancellationToken)
            {
                var model = Check.NotNull(request, nameof(request)).Model;
                this.logger.LogInformation(request.ToString());

                var dbArmEdit = this.context.ArmEdits
                    .SearchOrDefault(model.ArmEdit.Id);

                var dbAuthors = this.context.Authors
                    .SearchManyOrDefault(model.Authors.Select(e => e.Id));

                var dbModule = this.context.Communications
                    .Search(model.Communication.Id);

                var dbParent = this.context.ProjectRevisions
                   .SearchOrNull(model.ParentRevision.Id);

                var dbAlgorithms = this.context.RelayAlgorithms
                    .SearchManyOrDefault(model.RelayAlgorithms.Select(e => e.Id));

                var dbProjectRevision = this.context.ProjectRevisions
                    .Include(e => e.ProjectVersion)
                    .Include(e => e.Authors)
                    .Include(e => e.RelayAlgorithms)
                    .Search(model.Id)
                    .GetBuilder()
                    .SetAttributes(model)
                    .SetArmEdit(dbArmEdit)
                    .SetCommunication(dbModule)
                    .SetAuthors(dbAuthors)
                    .SetAlgorithms(dbAlgorithms)
                    .SetParentRevision(dbParent)
                    .Build();

                await this.context.SaveChangesAsync();

                return new StatusModel($"'{dbProjectRevision}' обновлен в системе.");
            }
        }
    }
}