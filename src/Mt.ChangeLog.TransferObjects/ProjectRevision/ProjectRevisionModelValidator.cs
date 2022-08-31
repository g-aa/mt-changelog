using FluentValidation;
using Mt.ChangeLog.TransferObjects.ArmEdit;
using Mt.ChangeLog.TransferObjects.Communication;
using Mt.ChangeLog.TransferObjects.ProjectVersion;

namespace Mt.ChangeLog.TransferObjects.ProjectRevision
{
    public sealed class ProjectRevisionModelValidator : AbstractValidator<ProjectRevisionModel>
    {
        public ProjectRevisionModelValidator()
        {
            RuleFor(e => e.Revision)
                .NotEmpty()
                .WithMessage("Редакция БФПО обязательный параметр для заполнения.")
                .Matches("^[0-9]{2}$")
                .WithMessage("Редакция БФПО, может принимать значение в интервала 00-99.");

            RuleFor(e => e.Reason)
                .NotNull()
                .WithMessage("Причина выпуска новой редакции не может принимать значение null.")
                .MaximumLength(500)
                .WithMessage("Причина выпуска новой редакции должна содержать не больше 500 символов.");

            RuleFor(e => e.Description)
                .NotNull()
                .WithMessage("Описание редакции не может принимать значение null.")
                .MaximumLength(5000)
                .WithMessage("Описание редакции должно содержать не больше 5000 символов.");

            RuleFor(e => e.ProjectVersion)
                .NotNull()
                .WithMessage("Версия проекта параметр обязательный для заполнения.")
                .SetValidator(new ProjectVersionShortModelValidator());

            RuleFor(e => e.ParentRevision)
                .SetValidator(new ProjectRevisionShortModelValidator())
                .When(e => e.ParentRevision != null);

            RuleFor(e => e.CommunicationModule)
                .NotNull()
                .WithMessage("Перечень поддерживаемых протоколов информационного обмена параметр обязательный для заполнения")
                .SetValidator(new CommunicationShortModelValidator());

            RuleFor(e => e.ArmEdit)
                .NotNull()
                .WithMessage("Версия ArmEdit параметр обязательный для заполнения.")
                .SetValidator(new ArmEditShortModelValidator());
        }
    }
}
