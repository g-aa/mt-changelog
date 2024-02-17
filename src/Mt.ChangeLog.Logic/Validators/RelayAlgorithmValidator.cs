using FluentValidation;
using Mt.ChangeLog.TransferObjects.RelayAlgorithm;
using Mt.FluentValidation;

namespace Mt.ChangeLog.Logic.Validators;

/// <summary>
/// Валидатор модели <see cref="RelayAlgorithmModel"/>.
/// </summary>
public sealed class RelayAlgorithmValidator : AbstractValidator<RelayAlgorithmModel>
{
    /// <summary>
    /// Инициализация экземпляра <see cref="RelayAlgorithmValidator"/>.
    /// </summary>
    public RelayAlgorithmValidator()
    {
        RuleFor(e => e.Title)
            .NotEmpty()
            .IsTrim()
            .MaximumLength(32);

        RuleFor(e => e.Group)
            .NotNull()
            .IsTrim()
            .MaximumLength(32);

        RuleFor(e => e.ANSI)
            .NotEmpty()
            .Matches("^[0-9 A-Z -/]{1,32}$")
            .WithMessage("Значение параметра '{PropertyName}' может содержать следующие символы 0-9, A-Z, -, /, но не более 32.");

        RuleFor(e => e.LogicalNode)
            .NotEmpty()
            .Matches("^[0-9 A-Z -/]{1,32}$")
            .WithMessage("Значение параметра '{PropertyName}' может содержать следующие символы 0-9, A-Z, -, /, но не более 32.");

        RuleFor(e => e.Description)
            .NotNull()
            .IsTrim()
            .MaximumLength(500);
    }
}