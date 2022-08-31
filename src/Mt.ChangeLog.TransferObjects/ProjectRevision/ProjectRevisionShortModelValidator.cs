using FluentValidation;

namespace Mt.ChangeLog.TransferObjects.ProjectRevision
{
    public sealed class ProjectRevisionShortModelValidator : AbstractValidator<ProjectRevisionShortModel>
    {
        public ProjectRevisionShortModelValidator()
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

            RuleFor(e => e.Revision)
                .NotEmpty()
                .WithMessage("Редакция БФПО обязательный параметр для заполнения.")
                .Matches("^[0-9]{2}$")
                .WithMessage("Редакция БФПО, может принимать значение в интервала 00-99.");
        }
    }
}
