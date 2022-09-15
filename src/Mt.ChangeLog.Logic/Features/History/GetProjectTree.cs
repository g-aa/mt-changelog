using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Mt.ChangeLog.Context;
using Mt.ChangeLog.Entities.Extensions.Tables;
using Mt.ChangeLog.Logic.Models;
using Mt.ChangeLog.TransferObjects.Historical;
using Mt.Utilities;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Mt.ChangeLog.Logic.Features.History
{
    /// <summary>
    /// Перечень моделий для дерева изменений.
    /// </summary>
    public static class GetProjectTree
    {
        /// <inheritdoc />
        public sealed class Query : MtQuery<string, IEnumerable<ProjectRevisionTreeModel>>
        {
            /// <summary>
            /// Инициализация нового экземпляра класса <see cref="Query"/>.
            /// </summary>
            public Query(string title) : base(title)
            {
            }

            /// <inheritdoc />
            public override string ToString()
            {
                return $"{base.ToString()} - получение перечня моделий для дерева изменений";
            }
        }

        /// <summary>
        /// Валидатор модели <see cref="Query"/>.
        /// </summary>
        public sealed class QueryValidator : AbstractValidator<Query>
        {
            /// <summary>
            /// Инициализация экземпляра <see cref="QueryValidator"/>.
            /// </summary>
            public QueryValidator()
            {
                this.RuleFor(e => e.Model)
                    .Must(e => e.Trim().Length == e.Length)
                    .WithMessage("Наименование проекта не должно содержать пробелов и табов в начале и конце строки.")
                    .Length(2, 16)
                    .WithMessage("Наименование проекта должно содержать не больше 2 и не менее 16 символов.");
            }
        }

        /// <inheritdoc />
        public sealed class Handler : IRequestHandler<Query, IEnumerable<ProjectRevisionTreeModel>>
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
            public async Task<IEnumerable<ProjectRevisionTreeModel>> Handle(Query request, CancellationToken cancellationToken)
            {
                Check.NotNull(request, nameof(request));
                this.logger.LogInformation(request.ToString());

                var result = this.context.ProjectRevisions.AsNoTracking()
                    .Include(pr => pr.ArmEdit)
                    .Include(pr => pr.ProjectVersion.AnalogModule)
                    .Include(pr => pr.ProjectVersion.Platform)
                    .Where(pr => pr.ProjectVersion.Title == request.Model)
                    .Select(pr => pr.ToTreeModel());

                return await Task.FromResult(result);
            }
        }
    }
}