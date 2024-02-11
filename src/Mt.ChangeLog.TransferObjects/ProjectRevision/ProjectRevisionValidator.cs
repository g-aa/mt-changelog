using FluentValidation;
using Mt.ChangeLog.TransferObjects.ArmEdit;
using Mt.ChangeLog.TransferObjects.Communication;
using Mt.ChangeLog.TransferObjects.ProjectVersion;
using Mt.FluentValidation;

namespace Mt.ChangeLog.TransferObjects.ProjectRevision;

/// <summary>
/// Валидатор модели <see cref="ProjectRevisionModel"/>.
/// </summary>
public sealed class ProjectRevisionValidator : AbstractValidator<ProjectRevisionModel>
{
    /// <summary>
    /// Инициализация экземпляра <see cref="ProjectRevisionValidator"/>.
    /// </summary>
    /// <param name="projectVersionValidator">Project version short model validator.</param>
    /// <param name="projectRevisionValidator">Project revision short model validator.</param>
    /// <param name="communicationValidator">Communication short model validator.</param>
    /// <param name="armEditValidator">ArmEdit short model validator.</param>
    public ProjectRevisionValidator(
        IValidator<ProjectVersionShortModel> projectVersionValidator,
        IValidator<ProjectRevisionShortModel> projectRevisionValidator,
        IValidator<CommunicationShortModel> communicationValidator,
        IValidator<ArmEditShortModel> armEditValidator)
    {
        RuleFor(e => e.Revision)
            .NotEmpty()
            .IsDigits()
            .Length(2);

        RuleFor(e => e.Reason)
            .NotNull()
            .IsTrim()
            .MaximumLength(500);

        RuleFor(e => e.Description)
            .NotNull()
            .IsTrim()
            .MaximumLength(5000);

        RuleFor(e => e.ProjectVersion)
            .SetValidator(projectVersionValidator);

        RuleFor(e => e.ParentRevision)
            .SetValidator(projectRevisionValidator!)
            .When(e => e.ParentRevision != null);

        RuleFor(e => e.Communication)
            .SetValidator(communicationValidator);

        RuleFor(e => e.ArmEdit)
            .SetValidator(armEditValidator);
    }
}