using FluentValidation;
using Mt.ChangeLog.TransferObjects.AnalogModule;
using Mt.ChangeLog.TransferObjects.Platform;
using Mt.ChangeLog.TransferObjects.ProjectStatus;
using Mt.ChangeLog.TransferObjects.ProjectVersion;
using Mt.FluentValidation;
using Mt.Utilities;

namespace Mt.ChangeLog.Logic.Validators;

/// <summary>
/// Валидатор модели <see cref="ProjectVersionModel"/>.
/// </summary>
public sealed class ProjectVersionValidator : AbstractValidator<ProjectVersionModel>
{
    /// <summary>
    /// Инициализация экземпляра <see cref="ProjectVersionValidator"/>.
    /// </summary>
    /// <param name="analogModuleValidator">Analog module short model validator.</param>
    /// <param name="platformValidator">Platform short model validator.</param>
    /// <param name="projectStatusValidator">Project status short model validator.</param>
    public ProjectVersionValidator(
        IValidator<AnalogModuleShortModel> analogModuleValidator,
        IValidator<PlatformShortModel> platformValidator,
        IValidator<ProjectStatusShortModel> projectStatusValidator)
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

        RuleFor(e => e.DIVG)
            .NotEmpty()
            .IsDIVG();

        RuleFor(e => e.Description)
            .NotNull()
            .IsTrim()
            .MaximumLength(500);

        RuleFor(e => e.ProjectStatus)
            .SetValidator(projectStatusValidator);

        RuleFor(e => e.AnalogModule)
            .SetValidator(analogModuleValidator);

        RuleFor(e => e.Platform)
            .SetValidator(platformValidator);
    }
}