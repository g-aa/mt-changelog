using FluentValidation;
using Mt.ChangeLog.TransferObjects.AnalogModule;

namespace Mt.ChangeLog.TransferObjects.Platform
{
    /// <summary>
    /// Валидатор модели <see cref="PlatformModel"/>.
    /// </summary>
    public sealed class PlatformModelValidator : AbstractValidator<PlatformModel>
    {
        /// <summary>
        /// Инициализация экземпляра <see cref="PlatformModelValidator"/>.
        /// </summary>
        public PlatformModelValidator()
        {
            this.Include(new PlatformShortModelValidator());
            
            this.RuleFor(e => e.Description)
                .NotNull()
                .WithMessage("Описание платформы БМРЗ не может принимать значение null.")
                .MaximumLength(500)
                .WithMessage("Описание платформы должно содержать не больше 500 символов.");

            this.RuleFor(e => e.AnalogModules)
                .NotNull()
                .WithMessage("Перечень аналоговых модулей не может принимать значение null.");

            this.RuleForEach(e => e.AnalogModules)
                .SetValidator(new AnalogModuleShortModelValidator());
        }
    }
}