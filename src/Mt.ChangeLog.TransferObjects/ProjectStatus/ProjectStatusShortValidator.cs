using FluentValidation;
using Mt.FluentValidation;

namespace Mt.ChangeLog.TransferObjects.ProjectStatus;

/// <summary>
/// Валидатор модели <see cref="ProjectStatusShortModel"/>.
/// </summary>
public sealed class ProjectStatusShortValidator : AbstractValidator<ProjectStatusShortModel>
{
    /// <summary>
    /// Инициализация экземпляра <see cref="ProjectStatusShortValidator"/>.
    /// </summary>
    public ProjectStatusShortValidator()
    {
        this.RuleFor(e => e.Title)
            .NotEmpty()
            .IsTrim()
            .MaximumLength(32);
    }
}