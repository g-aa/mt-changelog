using FluentValidation;
using Mt.ChangeLog.TransferObjects.Platform;
using Mt.FluentValidation;

namespace Mt.ChangeLog.Logic.Validators;

/// <summary>
/// Валидатор модели <see cref="PlatformShortModel"/>.
/// </summary>
public sealed class PlatformShortValidator : AbstractValidator<PlatformShortModel>
{
    /// <summary>
    /// Инициализация экземпляра <see cref="PlatformShortValidator"/>.
    /// </summary>
    public PlatformShortValidator()
    {
        RuleFor(e => e.Title)
            .NotEmpty()
            .IsPlatform();
    }
}