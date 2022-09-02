using FluentValidation;
using Mt.Utilities;

namespace Mt.ChangeLog.TransferObjects.ProjectRevision
{
    /// <summary>
    /// Валидатор модели <see cref="ProjectRevisionShortModel"/>.
    /// </summary>
    public sealed class ProjectRevisionShortModelValidator : AbstractValidator<ProjectRevisionShortModel>
    {
        /// <summary>
        /// Инициализация экземпляра <see cref="ProjectRevisionShortModelValidator"/>
        /// </summary>
        public ProjectRevisionShortModelValidator()
        {
            this.RuleFor(e => e.Prefix)
                .NotNull()
                .WithMessage("Префикс проекта, псевдоним аналогового модуля обязательный параметр для заполнения.")
                .Matches(Format.Prefix)
                .WithMessage("Префикс проекта, псевдоним аналогового модуля должено иметь следующий вид БФПО-xxx, где x - [0-9].");

            this.RuleFor(e => e.Title)
                .Must(e => e.Trim().Length == e.Length)
                .WithMessage("Наименование проекта не должно содержать пробелов и табов в начале и конце строки.")
                .Length(2, 16)
                .WithMessage("Наименование проекта должно содержать не больше 2 и не менее 16 символов.");

            this.RuleFor(e => e.Version)
                .NotEmpty()
                .WithMessage("Версия БФПО обязательный параметр для заполнения.")
                .Matches("^[0-9]{2}$")
                .WithMessage("Версия БФПО, может принимать значение в интервала 00-99.");

            this.RuleFor(e => e.Revision)
                .NotEmpty()
                .WithMessage("Редакция БФПО обязательный параметр для заполнения.")
                .Matches("^[0-9]{2}$")
                .WithMessage("Редакция БФПО, может принимать значение в интервала 00-99.");
        }
    }
}