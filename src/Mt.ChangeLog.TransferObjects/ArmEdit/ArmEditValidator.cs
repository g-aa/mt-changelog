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
        this.RuleFor(e => e.Version)
            .NotEmpty()
            .IsCfgVersion();

        this.RuleFor(e => e.DIVG)
            .IsDIVG();

        this.RuleFor(e => e.Description)
            .NotNull()
            .IsTrim()
            .MaximumLength(500);
    }
}