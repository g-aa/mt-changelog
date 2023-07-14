using FluentValidation;
using Mt.FluentValidation;

namespace Mt.ChangeLog.TransferObjects.AnalogModule;

/// <summary>
/// Валидатор модели <see cref="AnalogModuleShortModel"/>.
/// </summary>
public sealed class AnalogModuleShortValidator : AbstractValidator<AnalogModuleShortModel>
{
    /// <summary>
    /// Инициализация экземпляра <see cref="AnalogModuleShortValidator"/>.
    /// </summary>
    public AnalogModuleShortValidator()
    {
        this.RuleFor(e => e.Title)
            .NotEmpty()
            .IsAnalogModule();
    }
}