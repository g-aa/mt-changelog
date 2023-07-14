using FluentValidation;
using Mt.FluentValidation;

namespace Mt.ChangeLog.TransferObjects.ArmEdit;

/// <summary>
/// Валидатор модели <see cref="ArmEditShortModel"/>.
/// </summary>
public sealed class ArmEditShortValidator : AbstractValidator<ArmEditShortModel>
{
    /// <summary>
    /// Инициализация экземпляра <see cref="ArmEditShortValidator"/>.
    /// </summary>
    public ArmEditShortValidator()
    {
        this.RuleFor(e => e.Version)
            .NotEmpty()
            .IsCfgVersion();
    }
}