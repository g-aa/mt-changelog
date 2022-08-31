using FluentValidation;
using Mt.ChangeLog.TransferObjects.AnalogModule;
using Mt.ChangeLog.TransferObjects.Platform;
using Mt.ChangeLog.TransferObjects.ProjectStatus;

namespace Mt.ChangeLog.TransferObjects.ProjectVersion
{
    public sealed class ProjectVersionModelValidator : AbstractValidator<ProjectVersionModel>
    {
        public ProjectVersionModelValidator()
        {
            RuleFor(e => e.Prefix)
                .NotNull()
                .WithMessage("Префикс проекта, псевдоним аналогового модуля обязательный параметр для заполнения.")
                .Matches("^(БФПО(-[0-9]{3})?)?$")
                .WithMessage("Префикс проекта, псевдоним аналогового модуля должено иметь следующий вид БФПО-xxx, где x - [0-9].");

            RuleFor(e => e.Title)
                .Length(2, 16)
                .WithMessage("Наименование проекта должно содержать не больше 2 и не менее 16 символов.");

            RuleFor(e => e.Version)
                .NotEmpty()
                .WithMessage("Версия БФПО обязательный параметр для заполнения.")
                .Matches("^[0-9]{2}$")
                .WithMessage("Версия БФПО, может принимать значение в интервала 00-99.");

            RuleFor(e => e.DIVG)
                .NotEmpty()
                .WithMessage("Децимальный номер версии проекта обязательный параметр для заполнения.")
                .Matches("^ДИВГ.[0-9]{5}-[0-9]{2}$")
                .WithMessage("Децимальный номер должен иметь следующий вид ДИВГ.xxxxx-xx, где x - число [0-9].");

            RuleFor(e => e.Description)
                .NotNull()
                .WithMessage("Описание версии проекта не может принимать значение null.")
                .MaximumLength(500)
                .WithMessage("Описание версии проекта должно содержать не больше 500 символов.");

            RuleFor(e => e.ProjectStatus)
                .NotNull()
                .WithMessage("Статус проекта параметр обязательный для заполнения.")
                .SetValidator(new ProjectStatusShortModelValidator());

            RuleFor(e => e.AnalogModule)
                .NotNull()
                .WithMessage("Аналоговый модуль параметр обязательный для заполнения.")
                .SetValidator(new AnalogModuleShortModelValidator());

            RuleFor(e => e.Platform)
                .NotNull()
                .WithMessage("Платформа параметр обязательный для заполнения.")
                .SetValidator(new PlatformShortModelValidator());
        }
    }
}
