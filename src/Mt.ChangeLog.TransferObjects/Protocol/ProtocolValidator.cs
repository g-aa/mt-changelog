using FluentValidation;
using Mt.ChangeLog.TransferObjects.Communication;
using Mt.FluentValidation;

namespace Mt.ChangeLog.TransferObjects.Protocol;

/// <summary>
/// Валидатор модели <see cref="ProtocolModel"/>.
/// </summary>
public sealed class ProtocolValidator : AbstractValidator<ProtocolModel>
{
    /// <summary>
    /// Инициализация экземпляра <see cref="ProtocolValidator"/>.
    /// </summary>
    /// <param name="validator">Communication short model validator.</param>
    public ProtocolValidator(IValidator<CommunicationShortModel> validator)
    {
        RuleFor(e => e.Title)
            .NotEmpty()
            .IsTrim()
            .MaximumLength(32);

        RuleFor(e => e.Description)
            .NotNull()
            .IsTrim()
            .MaximumLength(500);

        RuleFor(e => e.Communications)
            .NotNull()
            .IsTrim();

        RuleForEach(e => e.Communications)
            .SetValidator(validator);
    }
}