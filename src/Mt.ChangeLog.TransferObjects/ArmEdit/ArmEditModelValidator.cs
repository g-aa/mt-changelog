using FluentValidation;
using Mt.Utilities;

namespace Mt.ChangeLog.TransferObjects.ArmEdit
{
    /// <summary>
    /// Валидатор модели <see cref="ArmEditModel"/>.
    /// </summary>
    public sealed class ArmEditModelValidator : AbstractValidator<ArmEditModel>
    {
        /// <summary>
        /// Инициализация экземпляра <see cref="ArmEditModelValidator"/>
        /// </summary>
        public ArmEditModelValidator()
        {
            this.Include(new ArmEditShortModelValidator());
            
            this.RuleFor(e => e.DIVG)
                .NotEmpty()
                .WithMessage("Децимальный номер ArmEdit обязательный параметр для заполнения.")
                .Matches(Format.DIVG)
                .WithMessage("Децимальный номер ArmEdit должен иметь следующий вид ДИВГ.xxxxx-xx, где x - число [0-9].");

            this.RuleFor(e => e.Description)
                .NotNull()
                .WithMessage("Описание ArmEdit не может принимать значение null.")
                .MaximumLength(500)
                .WithMessage("Описание ArmEdit должно содержать не больше 500 символов.");
        }
    }
}