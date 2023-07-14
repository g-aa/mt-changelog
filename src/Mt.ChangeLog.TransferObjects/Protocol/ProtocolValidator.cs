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
        this.RuleFor(e => e.Title)
            .NotEmpty()
            .IsTrim()
            .MaximumLength(32);

        this.RuleFor(e => e.Description)
            .NotNull()
            .IsTrim()
            .MaximumLength(500);

        this.RuleFor(e => e.Communications)
            .NotNull()
            .IsTrim();

        this.RuleForEach(e => e.Communications)
            .SetValidator(validator);
    }
}