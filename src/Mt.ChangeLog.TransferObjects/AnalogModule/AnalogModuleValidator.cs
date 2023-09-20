using FluentValidation;
using Mt.ChangeLog.TransferObjects.Platform;
using Mt.FluentValidation;
using Mt.Utilities;

namespace Mt.ChangeLog.TransferObjects.AnalogModule;

/// <summary>
/// Валидатор модели <see cref="AnalogModuleModel"/>.
/// </summary>
public sealed class AnalogModuleValidator : AbstractValidator<AnalogModuleModel>
{
    /// <summary>
    /// Инициализация экземпляра <see cref="AnalogModuleValidator"/>.
    /// </summary>
    /// <param name="validator">Platform short model validator.</param>
    public AnalogModuleValidator(IValidator<PlatformShortModel> validator)
    {
        this.RuleFor(e => e.Title)
            .NotEmpty()
            .IsAnalogModule();

        this.RuleFor(e => e.DIVG)
            .NotEmpty()
            .IsDIVG();

        this.RuleFor(e => e.Current)
            .NotEmpty()
            .Matches(StringFormat.Current)
            .WithMessage("Значение параметра '{PropertyName}' должно принимать значения от [0-9]A.");

        this.RuleFor(e => e.Description)
            .NotNull()
            .IsTrim()
            .MaximumLength(500);

        this.RuleFor(e => e.Platforms)
            .NotNull()
            .IsTrim();

        this.RuleForEach(e => e.Platforms)
            .SetValidator(validator);
    }
}