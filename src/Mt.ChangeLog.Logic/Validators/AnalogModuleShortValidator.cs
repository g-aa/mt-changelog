using FluentValidation;
using Mt.ChangeLog.TransferObjects.AnalogModule;
using Mt.FluentValidation;

namespace Mt.ChangeLog.Logic.Validators;

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
        RuleFor(e => e.Title)
            .NotEmpty()
            .IsAnalogModule();
    }
}