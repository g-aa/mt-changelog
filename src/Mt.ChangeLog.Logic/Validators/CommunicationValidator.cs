using FluentValidation;

using Mt.ChangeLog.TransferObjects.Communication;
using Mt.ChangeLog.TransferObjects.Protocol;
using Mt.FluentValidation;

namespace Mt.ChangeLog.Logic.Validators;

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
        RuleFor(e => e.Title)
            .NotEmpty()
            .IsTrim()
            .MaximumLength(64);

        RuleFor(e => e.Description)
            .NotNull()
            .IsTrim()
            .MaximumLength(500);

        RuleFor(e => e.Protocols)
            .NotNull()
            .IsTrim();

        RuleForEach(e => e.Protocols)
            .SetValidator(validator);
    }
}