using FluentValidation;

using Mt.ChangeLog.TransferObjects.RelayAlgorithm;
using Mt.FluentValidation;

namespace Mt.ChangeLog.Logic.Validators;

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
        RuleFor(e => e.Title)
            .NotEmpty()
            .IsTrim()
            .MaximumLength(32);
    }
}