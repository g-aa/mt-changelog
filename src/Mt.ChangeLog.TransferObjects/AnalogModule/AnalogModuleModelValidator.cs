using FluentValidation;
using Mt.ChangeLog.TransferObjects.Platform;
using Mt.Utilities;

namespace Mt.ChangeLog.TransferObjects.AnalogModule
{
    /// <summary>
    /// Валидатор модели <see cref="AnalogModuleModel"/>.
    /// </summary>
    public sealed class AnalogModuleModelValidator : AbstractValidator<AnalogModuleModel>
    {
        /// <summary>
        /// Инициализация экземпляра <see cref="AnalogModuleModelValidator"/>.
        /// </summary>
        public AnalogModuleModelValidator()
        {
            this.Include(new AnalogModuleShortModelValidator());

            this.RuleFor(e => e.DIVG)
                .NotEmpty()
                .WithMessage("Децимальный номер аналогового модуля обязательный параметр для заполнения.")
                .Matches(StringFormat.DIVG)
                .WithMessage("Децимальный номер аналогового модуля должен иметь следующий вид ДИВГ.xxxxx-xx, где x - число [0-9].");

            this.RuleFor(e => e.Current)
                .NotEmpty()
                .WithMessage("Номинальный ток аналогового модуля обязательный параметр для заполнения.")
                .Matches(StringFormat.Current)
                .WithMessage("Номинальный ток аналогового модуля должен принимать значение от [0-9]A.");

            this.RuleFor(e => e.Description)
                .NotNull()
                .WithMessage("Описание аналогового модуля не может принимать значение null.")
                .MaximumLength(500)
                .WithMessage("Описание аналогового модуля должно содержать не больше 500 символов.");

            this.RuleFor(e => e.Platforms)
                .NotNull()
                .WithMessage("Перечень платформ не может принимать значение null.");

            this.RuleForEach(e => e.Platforms)
                .SetValidator(new PlatformShortModelValidator());
        }
    }
}