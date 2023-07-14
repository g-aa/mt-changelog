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
        this.RuleFor(e => e.Revision)
            .NotEmpty()
            .IsDigits()
            .Length(2);

        this.RuleFor(e => e.Reason)
            .NotNull()
            .IsTrim()
            .MaximumLength(500);

        this.RuleFor(e => e.Description)
            .NotNull()
            .IsTrim()
            .MaximumLength(5000);

        this.RuleFor(e => e.ProjectVersion)
            .NotNull()
            .WithMessage("Версия проекта параметр обязательный для заполнения.")
            .SetValidator(projectVersionValidator);

        this.RuleFor(e => e.ParentRevision)
            .SetValidator(projectRevisionValidator)
            .When(e => e.ParentRevision != null);

        this.RuleFor(e => e.Communication)
            .NotNull()
            .WithMessage("Перечень поддерживаемых протоколов информационного обмена параметр обязательный для заполнения")
            .SetValidator(communicationValidator);

        this.RuleFor(e => e.ArmEdit)
            .NotNull()
            .WithMessage("Версия ArmEdit параметр обязательный для заполнения.")
            .SetValidator(armEditValidator);
    }
}