using FluentValidation;

using Mt.ChangeLog.TransferObjects.ProjectStatus;
using Mt.FluentValidation;

namespace Mt.ChangeLog.Logic.Validators;

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
        RuleFor(e => e.Title)
            .NotEmpty()
            .IsTrim()
            .MaximumLength(32);
    }
}