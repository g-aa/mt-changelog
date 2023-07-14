using FluentValidation;
using Mt.FluentValidation;

namespace Mt.ChangeLog.TransferObjects.Author;

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
        this.RuleFor(e => e.FirstName)
            .NotEmpty()
            .IsTrim()
            .MaximumLength(32);

        this.RuleFor(e => e.LastName)
            .NotEmpty()
            .IsTrim()
            .MaximumLength(32);

        this.RuleFor(e => e.Position)
            .NotNull()
            .IsTrim()
            .MaximumLength(250);
    }
}