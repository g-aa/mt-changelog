using FluentValidation;
using Mt.Utilities;

namespace Mt.ChangeLog.TransferObjects.AnalogModule
{
    /// <summary>
    /// Валидатор модели <see cref="AnalogModuleShortModel"/>.
    /// </summary>
    public sealed class AnalogModuleShortModelValidator : AbstractValidator<AnalogModuleShortModel>
    {
        /// <summary>
        /// Инициализация экземпляра <see cref="AnalogModuleShortModelValidator"/>.
        /// </summary>
        public AnalogModuleShortModelValidator()
        {
            this.RuleFor(e => e.Title)
                .NotEmpty()
                .WithMessage("Наименование аналогово модуля обязательный параметр для заполнения.")
                .Matches(Format.AnalogModule)
                .WithMessage("Наименование аналогово модуля должено иметь следующий вид БМРЗ-xxxx, где x - [0-9 A-Z А-Я].");
        }
    }
}