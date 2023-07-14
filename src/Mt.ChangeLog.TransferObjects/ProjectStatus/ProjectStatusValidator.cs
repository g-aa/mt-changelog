using FluentValidation;
using Mt.FluentValidation;

namespace Mt.ChangeLog.TransferObjects.ProjectStatus;

/// <summary>
/// Валидатор модели <see cref="ProjectStatusModel"/>.
/// </summary>
public sealed class ProjectStatusValidator : AbstractValidator<ProjectStatusModel>
{
    /// <summary>
    /// Инициализация экземпляра <see cref="ProjectStatusValidator"/>.
    /// </summary>
    public ProjectStatusValidator()
    {
        this.RuleFor(e => e.Title)
            .NotEmpty()
            .IsTrim()
            .MaximumLength(32);

        this.RuleFor(e => e.Description)
            .NotNull()
            .IsTrim()
            .MaximumLength(500);
    }
}