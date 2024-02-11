using FluentValidation;
using Mt.FluentValidation;

namespace Mt.ChangeLog.TransferObjects.ArmEdit;

/// <summary>
/// Валидатор модели <see cref="ArmEditModel"/>.
/// </summary>
public sealed class ArmEditValidator : AbstractValidator<ArmEditModel>
{
    /// <summary>
    /// Инициализация экземпляра <see cref="ArmEditValidator"/>.
    /// </summary>
    public ArmEditValidator()
    {
        RuleFor(e => e.Version)
            .NotEmpty()
            .IsCfgVersion();

        RuleFor(e => e.DIVG)
            .IsDIVG();

        RuleFor(e => e.Description)
            .NotNull()
            .IsTrim()
            .MaximumLength(500);
    }
}