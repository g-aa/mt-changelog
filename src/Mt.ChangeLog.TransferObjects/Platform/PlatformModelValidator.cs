using FluentValidation;
using Mt.ChangeLog.TransferObjects.AnalogModule;

namespace Mt.ChangeLog.TransferObjects.Platform
{
    public sealed class PlatformModelValidator : AbstractValidator<PlatformModel>
    {
        public PlatformModelValidator()
        {
            RuleFor(e => e.Title)
                .NotEmpty()
                .WithMessage("Наименование платформы БМРЗ параметр обязательный для заполнения.")
                .Matches("^БМРЗ-[0-9 A-Z А-Я]{2,5}$")
                .WithMessage("Наименование платформы должено иметь следующий вид БМРЗ-xxxxx, где x - [0-9 A-Z А-Я].");

            RuleFor(e => e.Description)
                .NotNull()
                .WithMessage("Описание платформы БМРЗ не может принимать значение null.")
                .MaximumLength(500)
                .WithMessage("Описание платформы должно содержать не больше 500 символов.");

            RuleFor(e => e.AnalogModules)
                .NotNull()
                .WithMessage("Перечень аналоговых модулей не может принимать значение null.");

            RuleForEach(e => e.AnalogModules)
                .SetValidator(new AnalogModuleShortModelValidator());
        }
    }
}
