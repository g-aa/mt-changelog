using FluentValidation;
using Mt.FluentValidation;
using Mt.Utilities;

namespace Mt.ChangeLog.TransferObjects.ProjectVersion;

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
    }
}