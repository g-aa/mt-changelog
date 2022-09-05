using FluentValidation;
using Mt.ChangeLog.TransferObjects.ArmEdit;
using Mt.ChangeLog.TransferObjects.Communication;
using Mt.ChangeLog.TransferObjects.ProjectVersion;

namespace Mt.ChangeLog.TransferObjects.ProjectRevision
{
    /// <summary>
    /// Валидатор модели <see cref="ProjectRevisionModel"/>.
    /// </summary>
    public sealed class ProjectRevisionModelValidator : AbstractValidator<ProjectRevisionModel>
    {
        /// <summary>
        /// Инициализация экземпляра <see cref="ProjectRevisionModelValidator"/>.
        /// </summary>
        public ProjectRevisionModelValidator()
        {
            this.RuleFor(e => e.Revision)
                .NotEmpty()
                .WithMessage("Редакция БФПО обязательный параметр для заполнения.")
                .Matches("^[0-9]{2}$")
                .WithMessage("Редакция БФПО, может принимать значение в интервала 00-99.");

            this.RuleFor(e => e.Reason)
                .NotNull()
                .WithMessage("Причина выпуска новой редакции не может принимать значение null.")
                .Must(e => e.Trim().Length == e.Length)
                .WithMessage("Причина выпуска новой редакции не должно содержать пробелов и табов в начале и конце строки.")
                .MaximumLength(500)
                .WithMessage("Причина выпуска новой редакции должна содержать не больше 500 символов.");

            this.RuleFor(e => e.Description)
                .NotNull()
                .WithMessage("Описание редакции не может принимать значение null.")
                .MaximumLength(5000)
                .WithMessage("Описание редакции должно содержать не больше 5000 символов.");

            this.RuleFor(e => e.ProjectVersion)
                .NotNull()
                .WithMessage("Версия проекта параметр обязательный для заполнения.")
                .SetValidator(new ProjectVersionShortModelValidator());

            this.RuleFor(e => e.ParentRevision)
                .SetValidator(new ProjectRevisionShortModelValidator())
                .When(e => e.ParentRevision != null);

            this.RuleFor(e => e.Communication)
                .NotNull()
                .WithMessage("Перечень поддерживаемых протоколов информационного обмена параметр обязательный для заполнения")
                .SetValidator(new CommunicationShortModelValidator());

            this.RuleFor(e => e.ArmEdit)
                .NotNull()
                .WithMessage("Версия ArmEdit параметр обязательный для заполнения.")
                .SetValidator(new ArmEditShortModelValidator());
        }
    }
}