using FluentValidation;
using Mt.ChangeLog.TransferObjects.ProjectRevision;
using Mt.FluentValidation;
using Mt.Utilities;

namespace Mt.ChangeLog.Logic.Validators;

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
        RuleFor(e => e.Prefix)
            .NotNull()
            .Matches(StringFormat.Prefix)
            .WithMessage("Значение параметра '{PropertyName}' должено иметь следующий вид БФПО-xxx, где x - [0-9].");

        RuleFor(e => e.Title)
            .IsTrim()
            .Length(2, 16);

        RuleFor(e => e.Version)
            .NotEmpty()
            .IsDigits()
            .Length(2);

        RuleFor(e => e.Revision)
            .NotEmpty()
            .IsDigits()
            .Length(2);
    }
}