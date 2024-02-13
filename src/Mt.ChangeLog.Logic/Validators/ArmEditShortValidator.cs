using FluentValidation;
using Mt.ChangeLog.TransferObjects.ArmEdit;
using Mt.FluentValidation;

namespace Mt.ChangeLog.Logic.Validators;

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
        RuleFor(e => e.Version)
            .NotEmpty()
            .IsCfgVersion();
    }
}