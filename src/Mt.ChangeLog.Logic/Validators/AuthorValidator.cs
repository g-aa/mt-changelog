using FluentValidation;
using Mt.ChangeLog.TransferObjects.Author;
using Mt.FluentValidation;

namespace Mt.ChangeLog.Logic.Validators;

/// <summary>
/// Валидатор модели <see cref="AuthorModel"/>.
/// </summary>
public sealed class AuthorValidator : AbstractValidator<AuthorModel>
{
    /// <summary>
    /// Инициализация экземпляра <see cref="AuthorValidator"/>.
    /// </summary>
    public AuthorValidator()
    {
        RuleFor(e => e.FirstName)
            .NotEmpty()
            .IsTrim()
            .MaximumLength(32);

        RuleFor(e => e.LastName)
            .NotEmpty()
            .IsTrim()
            .MaximumLength(32);

        RuleFor(e => e.Position)
            .NotNull()
            .IsTrim()
            .MaximumLength(250);
    }
}