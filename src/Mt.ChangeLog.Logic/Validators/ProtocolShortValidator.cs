using FluentValidation;
using Mt.ChangeLog.TransferObjects.Protocol;
using Mt.FluentValidation;

namespace Mt.ChangeLog.Logic.Validators;

/// <summary>
/// Валидатор модели <see cref="ProtocolShortModel"/>.
/// </summary>
public sealed class ProtocolShortValidator : AbstractValidator<ProtocolShortModel>
{
    /// <summary>
    /// Инициализация экземпляра <see cref="ProtocolShortValidator"/>.
    /// </summary>
    public ProtocolShortValidator()
    {
        RuleFor(e => e.Title)
            .NotEmpty()
            .IsTrim()
            .MaximumLength(32);
    }
}