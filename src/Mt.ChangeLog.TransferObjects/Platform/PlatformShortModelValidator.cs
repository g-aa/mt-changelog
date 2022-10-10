using FluentValidation;
using Mt.Utilities;

namespace Mt.ChangeLog.TransferObjects.Platform
{
    /// <summary>
    /// Валидатор модели <see cref="PlatformShortModel"/>.
    /// </summary>
    public sealed class PlatformShortModelValidator : AbstractValidator<PlatformShortModel>
    {
        /// <summary>
        /// Инициализация экземпляра <see cref="PlatformShortModelValidator"/>.
        /// </summary>
        public PlatformShortModelValidator()
        {
            this.RuleFor(e => e.Title)
                .NotEmpty()
                .WithMessage("Наименование платформы БМРЗ параметр обязательный для заполнения.")
                .Matches(StringFormat.Platform)
                .WithMessage("Наименование платформы должено иметь следующий вид БМРЗ-xxxxx, где x - [0-9 A-Z А-Я].");
        }
    }
}