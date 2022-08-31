using FluentValidation;

namespace Mt.ChangeLog.TransferObjects.ArmEdit
{
    public sealed class ArmEditModelValidator : AbstractValidator<ArmEditModel>
    {
        public ArmEditModelValidator()
        {
            RuleFor(e => e.Version)
                .NotEmpty()
                .WithMessage("Версия ArmEdit параметр обязательный для заполнения.")
                .Matches("^v[0-9]{1}.[0-9]{2}.[0-9]{2}.[0-9]{2}$")
                .WithMessage("Версия ArmEdit должна принимать следующий вид vx.xx.xx.xx, где - x число [0-9].");

            RuleFor(e => e.DIVG)
                .NotEmpty()
                .WithMessage("Децимальный номер ArmEdit обязательный параметр для заполнения.")
                .Matches("^ДИВГ.[0-9]{5}-[0-9]{2}$")
                .WithMessage("Децимальный номер ArmEdit должен иметь следующий вид ДИВГ.xxxxx-xx, где x - число [0-9].");

            RuleFor(e => e.Description)
                .NotNull()
                .WithMessage("Описание ArmEdit не может принимать значение null.")
                .MaximumLength(500)
                .WithMessage("Описание ArmEdit должно содержать не больше 500 символов.");
        }
    }
}
