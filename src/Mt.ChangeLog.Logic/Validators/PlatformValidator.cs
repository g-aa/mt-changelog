using FluentValidation;
using Mt.ChangeLog.TransferObjects.AnalogModule;
using Mt.ChangeLog.TransferObjects.Platform;
using Mt.FluentValidation;

namespace Mt.ChangeLog.Logic.Validators;

/// <summary>
/// Валидатор модели <see cref="PlatformModel"/>.
/// </summary>
public sealed class PlatformValidator : AbstractValidator<PlatformModel>
{
    /// <summary>
    /// Инициализация экземпляра <see cref="PlatformValidator"/>.
    /// </summary>
    /// <param name="validator">Analog module short model validator.</param>
    public PlatformValidator(IValidator<AnalogModuleShortModel> validator)
    {
        RuleFor(e => e.Title)
            .NotEmpty()
            .IsPlatform();

        RuleFor(e => e.Description)
            .NotNull()
            .IsTrim()
            .MaximumLength(500);

        RuleFor(e => e.AnalogModules)
            .NotNull()
            .IsTrim();

        RuleForEach(e => e.AnalogModules)
            .SetValidator(validator);
    }
}