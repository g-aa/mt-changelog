using FluentValidation;
using Mt.FluentValidation;
using Mt.Utilities;

namespace Mt.ChangeLog.TransferObjects.ProjectRevision;

/// <summary>
/// Валидатор модели <see cref="ProjectRevisionShortModel"/>.
/// </summary>
public sealed class ProjectRevisionShortValidator : AbstractValidator<ProjectRevisionShortModel>
{
    /// <summary>
    /// Инициализация экземпляра <see cref="ProjectRevisionShortValidator"/>.
    /// </summary>
    public ProjectRevisionShortValidator()
    {
        this.RuleFor(e => e.Prefix)
            .NotNull()
            .Matches(StringFormat.Prefix)
            .WithMessage("Значение параметра '{PropertyName}' должено иметь следующий вид БФПО-xxx, где x - [0-9].");

        this.RuleFor(e => e.Title)
            .IsTrim()
            .Length(2, 16);

        this.RuleFor(e => e.Version)
            .NotEmpty()
            .IsDigits()
            .Length(2);

        this.RuleFor(e => e.Revision)
            .NotEmpty()
            .IsDigits()
            .Length(2);
    }
}