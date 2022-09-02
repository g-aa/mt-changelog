using FluentValidation;
using Mt.ChangeLog.TransferObjects.AnalogModule;
using Mt.ChangeLog.TransferObjects.Platform;
using Mt.ChangeLog.TransferObjects.ProjectStatus;
using Mt.Utilities;

namespace Mt.ChangeLog.TransferObjects.ProjectVersion
{
    /// <summary>
    /// Валидатор модели <see cref="ProjectVersionModel"/>.
    /// </summary>
    public sealed class ProjectVersionModelValidator : AbstractValidator<ProjectVersionModel>
    {
        /// <summary>
        /// Инициализация экземпляра <see cref="ProjectVersionModelValidator"/>
        /// </summary>
        public ProjectVersionModelValidator()
        {
            this.Include(new ProjectVersionShortModelValidator());
            
            this.RuleFor(e => e.DIVG)
                .NotEmpty()
                .WithMessage("Децимальный номер версии проекта обязательный параметр для заполнения.")
                .Matches(Format.DIVG)
                .WithMessage("Децимальный номер должен иметь следующий вид ДИВГ.xxxxx-xx, где x - число [0-9].");

            this.RuleFor(e => e.Description)
                .NotNull()
                .WithMessage("Описание версии проекта не может принимать значение null.")
                .MaximumLength(500)
                .WithMessage("Описание версии проекта должно содержать не больше 500 символов.");

            this.RuleFor(e => e.ProjectStatus)
                .NotNull()
                .WithMessage("Статус проекта параметр обязательный для заполнения.")
                .SetValidator(new ProjectStatusShortModelValidator());

            this.RuleFor(e => e.AnalogModule)
                .NotNull()
                .WithMessage("Аналоговый модуль параметр обязательный для заполнения.")
                .SetValidator(new AnalogModuleShortModelValidator());

            this.RuleFor(e => e.Platform)
                .NotNull()
                .WithMessage("Платформа параметр обязательный для заполнения.")
                .SetValidator(new PlatformShortModelValidator());
        }
    }
}