using FluentValidation;
using Mt.FluentValidation;

namespace Mt.ChangeLog.TransferObjects.Protocol;

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
        this.RuleFor(e => e.Title)
            .NotEmpty()
            .IsTrim()
            .MaximumLength(32);
    }
}