using FluentValidation;
using Mt.ChangeLog.TransferObjects.Protocol;
using Mt.FluentValidation;

namespace Mt.ChangeLog.TransferObjects.Communication;

/// <summary>
/// Валидатор модели <see cref="CommunicationModel"/>.
/// </summary>
public sealed class CommunicationValidator : AbstractValidator<CommunicationModel>
{
    /// <summary>
    /// Инициализация экземпляра <see cref="CommunicationValidator"/>.
    /// </summary>
    /// <param name="validator">Protocol short model validator.</param>
    public CommunicationValidator(IValidator<ProtocolShortModel> validator)
    {
        this.RuleFor(e => e.Title)
            .NotEmpty()
            .IsTrim()
            .MaximumLength(64);

        this.RuleFor(e => e.Description)
            .NotNull()
            .IsTrim()
            .MaximumLength(500);

        this.RuleFor(e => e.Protocols)
            .NotNull()
            .IsTrim();

        this.RuleForEach(e => e.Protocols)
            .SetValidator(validator);
    }
}