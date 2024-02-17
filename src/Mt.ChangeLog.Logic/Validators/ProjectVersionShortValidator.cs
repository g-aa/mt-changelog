using FluentValidation;
using Mt.ChangeLog.TransferObjects.ProjectVersion;
using Mt.FluentValidation;
using Mt.Utilities;

namespace Mt.ChangeLog.Logic.Validators;

/// <summary>
/// Валидатор модели <see cref="ProjectVersionShortModel"/>.
/// </summary>
public sealed class ProjectVersionShortValidator : AbstractValidator<ProjectVersionShortModel>
{
    /// <summary>
    /// Инициализация экземпляра <see cref="ProjectVersionShortValidator"/>.
    /// </summary>
    public ProjectVersionShortValidator()
    {
        RuleFor(e => e.Prefix)
            .NotNull()
            .Matches(StringFormat.Prefix)
            .WithMessage("Значение параметра '{PropertyName}' должно иметь следующий вид БФПО-xxx, где x - [0-9].");

        RuleFor(e => e.Title)
            .IsTrim()
            .Length(2, 16);

        RuleFor(e => e.Version)
            .NotEmpty()
            .IsDigits()
            .Length(2);
    }
}