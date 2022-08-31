using FluentValidation;

namespace Mt.ChangeLog.TransferObjects.ArmEdit
{
    public sealed class ArmEditShortModelValidator : AbstractValidator<ArmEditShortModel>
    {
        public ArmEditShortModelValidator()
        {
            RuleFor(e => e.Version)
                .NotEmpty()
                .WithMessage("Версия ArmEdit параметр обязательный для заполнения.")
                .Matches("^v[0-9]{1}.[0-9]{2}.[0-9]{2}.[0-9]{2}$")
                .WithMessage("Версия ArmEdit должна принимать следующий вид vx.xx.xx.xx, где - x число [0-9].");
        }
    }
}
