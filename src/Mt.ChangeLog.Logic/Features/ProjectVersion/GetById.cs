using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Mt.ChangeLog.DataContext;
using Mt.ChangeLog.Logic.Mappers;
using Mt.ChangeLog.TransferObjects.Other;
using Mt.ChangeLog.TransferObjects.ProjectVersion;
using Mt.Entities.Abstractions.Extensions;

namespace Mt.ChangeLog.Logic.Features.ProjectVersion;

/// <summary>
/// Запрос на получение перечня моделей данных для таблиц <see cref="ProjectVersionModel"/>.
/// </summary>
public static class GetById
{
    /// <inheritdoc />
    public sealed record Query(BaseModel Model) : IRequest<ProjectVersionModel>
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
            this.RuleFor(e => e.Model).SetValidator(validator);
        }
    }

    /// <inheritdoc />
    public sealed class Handler : IRequestHandler<Query, ProjectVersionModel>
    {
        private readonly ILogger<Handler> logger;

        private readonly MtContext context;

        /// <summary>
        /// Инициализация нового экземпляра класса <see cref="Handler"/>.
        /// </summary>
        /// <param name="logger">Журнал логирования.</param>
        /// <param name="context">Контекст данных.</param>
        public Handler(ILogger<Handler> logger, MtContext context)
        {
            this.logger = logger;
            this.context = context;
        }

        /// <inheritdoc />
        public Task<ProjectVersionModel> Handle(Query request, CancellationToken cancellationToken)
        {
            var model = request.Model;
            this.logger.LogDebug("Получен запрос на предоставление данных о версии проекта '{Id}'.", model.Id);

            var result = this.context.ProjectVersions.AsNoTracking()
                .Include(e => e.AnalogModule)
                .Include(e => e.Platform)
                .Include(e => e.ProjectStatus)
                .Search(request.Model.Id)
                .ToModel();

            this.logger.LogDebug("Запрос на получение данных о версии проекта '{Result}' выполнен успешно.", result);
            return Task.FromResult(result);
        }
    }
}