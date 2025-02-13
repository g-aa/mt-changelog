using System.Reflection;

using FluentValidation;

using Microsoft.Extensions.DependencyInjection;

using Mt.ChangeLog.Logic.Extensions;
using Mt.ChangeLog.Logic.Pipelines;
using Mt.ChangeLog.Logic.Validators;

namespace Mt.ChangeLog.Logic;

/// <summary>
/// Методы расширения для <see cref="IServiceCollection"/>.
/// </summary>
public static class LogicServiceCollectionExtensions
{
    /// <summary>
    /// Добавить компоненты логики в коллекцию сервисов.
    /// </summary>
    /// <param name="services">Коллекция сервисов.</param>
    /// <param name="assemblies">Перечень сборок проекта.</param>
    /// <returns>Модифицированная коллекция сервисов.</returns>
    public static IServiceCollection AddLogic(this IServiceCollection services, IReadOnlyCollection<Assembly> assemblies)
    {
        return services
            .AddConverters<LogicLayer>()
            .AddValidatorsFromAssemblies(assemblies)
            .AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblies((Assembly[])assemblies);
                cfg.AddOpenRequestPreProcessor(typeof(ValidationRequestPreProcessor<>));
                cfg.AddOpenBehavior(typeof(LoggingScopePipelineBehavior<,>));
            });
    }
}