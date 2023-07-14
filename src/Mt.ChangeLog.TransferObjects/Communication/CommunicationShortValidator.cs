using FluentValidation;
using Mt.FluentValidation;

namespace Mt.ChangeLog.TransferObjects.Communication;

/// <summary>
/// Валидатор модели <see cref="CommunicationShortModel"/>.
/// </summary>
public sealed class CommunicationShortValidator : AbstractValidator<CommunicationShortModel>
{
    /// <summary>
    /// Инициализация экземпляра <see cref="CommunicationShortValidator"/>.
    /// </summary>
    public CommunicationShortValidator()
    {
        this.RuleFor(e => e.Title)
            .NotEmpty()
            .IsTrim()
            .MaximumLength(64);
    }
}