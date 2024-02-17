using FluentValidation;
using Mt.ChangeLog.TransferObjects.ProjectStatus;
using Mt.FluentValidation;

namespace Mt.ChangeLog.Logic.Validators;

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
        RuleFor(e => e.Title)
            .NotEmpty()
            .IsTrim()
            .MaximumLength(32);

        RuleFor(e => e.Description)
            .NotNull()
            .IsTrim()
            .MaximumLength(500);
    }
}