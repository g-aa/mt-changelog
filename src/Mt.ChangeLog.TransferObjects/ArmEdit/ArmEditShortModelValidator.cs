using FluentValidation;
using Mt.Utilities;

namespace Mt.ChangeLog.TransferObjects.ArmEdit
{
    /// <summary>
    /// Валидатор модели <see cref="ArmEditShortModel"/>.
    /// </summary>
    public sealed class ArmEditShortModelValidator : AbstractValidator<ArmEditShortModel>
    {
        /// <summary>
        /// Инициализация экземпляра <see cref="ArmEditShortModelValidator"/>
        /// </summary>
        public ArmEditShortModelValidator()
        {
            this.RuleFor(e => e.Version)
                .NotEmpty()
                .WithMessage("Версия ArmEdit параметр обязательный для заполнения.")
                .Matches(Format.Version)
                .WithMessage("Версия ArmEdit должна принимать следующий вид vx.xx.xx.xx, где - x число [0-9].");
        }
    }
}