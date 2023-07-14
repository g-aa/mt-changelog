using FluentValidation;
using Mt.ChangeLog.TransferObjects.AnalogModule;
using Mt.FluentValidation;

namespace Mt.ChangeLog.TransferObjects.Platform;

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
        this.RuleFor(e => e.Title)
            .NotEmpty()
            .IsPlatform();

        this.RuleFor(e => e.Description)
            .NotNull()
            .IsTrim()
            .MaximumLength(500);

        this.RuleFor(e => e.AnalogModules)
            .NotNull()
            .IsTrim();

        this.RuleForEach(e => e.AnalogModules)
            .SetValidator(validator);
    }
}