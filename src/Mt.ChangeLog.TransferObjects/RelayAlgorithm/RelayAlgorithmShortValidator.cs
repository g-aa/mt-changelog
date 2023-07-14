using FluentValidation;
using Mt.FluentValidation;

namespace Mt.ChangeLog.TransferObjects.RelayAlgorithm;

/// <summary>
/// Валидатор модели <see cref="RelayAlgorithmShortModel"/>.
/// </summary>
public sealed class RelayAlgorithmShortValidator : AbstractValidator<RelayAlgorithmShortModel>
{
    /// <summary>
    /// Инициализация экземпляра <see cref="RelayAlgorithmShortValidator"/>.
    /// </summary>
    public RelayAlgorithmShortValidator()
    {
        this.RuleFor(e => e.Title)
            .NotEmpty()
            .IsTrim()
            .MaximumLength(32);
    }
}