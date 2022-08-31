using FluentValidation;

namespace Mt.ChangeLog.TransferObjects.Platform
{
    public sealed class PlatformShortModelValidator : AbstractValidator<PlatformShortModel>
    {
        public PlatformShortModelValidator()
        {
            RuleFor(e => e.Title)
                .NotEmpty()
                .WithMessage("Наименование платформы БМРЗ параметр обязательный для заполнения.")
                .Matches("^БМРЗ-[0-9 A-Z А-Я]{2,5}$")
                .WithMessage("Наименование платформы должено иметь следующий вид БМРЗ-xxxxx, где x - [0-9 A-Z А-Я].");
        }
    }
}
