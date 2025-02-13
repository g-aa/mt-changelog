using FluentValidation;

using MediatR;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using Mt.ChangeLog.DataContext;
using Mt.ChangeLog.Logic.Extensions;
using Mt.ChangeLog.TransferObjects.Other;
using Mt.ChangeLog.TransferObjects.ProjectVersion;
using Mt.Entities.Abstractions.Extensions;

namespace Mt.ChangeLog.Logic.Features.ProjectVersion;

/// <summary>
/// Запрос на обновление сущности <see cref="ProjectVersionModel"/>.
/// </summary>
public static class Update
{
    /// <inheritdoc />
    public sealed record Command(Guid ProjectVersionId, ProjectVersionModel Model) : IRequest<MessageModel>
    {
    }

    /// <inheritdoc />
    public sealed class CommandValidator : AbstractValidator<Command>
    {
        /// <summary>
        /// Инициализация экземпляра <see cref="CommandValidator"/>.
        /// </summary>
        /// <param name="validator">Project version model validator.</param>
        public CommandValidator(IValidator<ProjectVersionModel> validator)
        {
            RuleFor(e => e.ProjectVersionId)
                .Must((command, id) => id == command.Model.Id)
                .WithMessage("Значение параметра '{PropertyName}' не равен значению идентификатора в модели из тела запроса.");

            RuleFor(e => e.Model).SetValidator(validator);
        }
    }

    /// <inheritdoc />
    public sealed class Handler : IRequestHandler<Command, MessageModel>
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
        public async Task<MessageModel> Handle(Command request, CancellationToken cancellationToken)
        {
            var model = request.Model;
            _logger.LogDebug("Получен запрос на обновление данных версии проекта '{Model}' в системе.", model);

            var dbStatus = _context.ProjectStatuses
                .SearchOrDefault(model.ProjectStatus.Id);

            var dbPlatform = _context.Platforms
                .Include(e => e.AnalogModules)
                .SearchOrDefault(model.Platform.Id);

            var dbAnalogModule = dbPlatform.AnalogModules
                .Search(model.AnalogModule.Id);

            var dbProjectVersion = _context.ProjectVersions.Search(model.Id)
                .GetBuilder()
                .SetAttributes(model)
                .SetProjectStatus(dbStatus)
                .SetPlatform(dbPlatform)
                .SetAnalogModule(dbAnalogModule)
                .Build();

            _context.ProjectVersions.Update(dbProjectVersion);
            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Версия проекта '{DbProjectVersion}' успешно обновлен в системе.", dbProjectVersion);
            return new MessageModel
            {
                Message = $"'{dbProjectVersion}' обновлен в системе.",
            };
        }
    }
}