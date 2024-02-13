using FluentValidation;
using Mt.ChangeLog.TransferObjects.Communication;
using Mt.FluentValidation;

namespace Mt.ChangeLog.Logic.Validators;

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
        RuleFor(e => e.Title)
            .NotEmpty()
            .IsTrim()
            .MaximumLength(64);
    }
}