using FluentValidation;
using Mt.ChangeLog.TransferObjects.Protocol;

namespace Mt.ChangeLog.TransferObjects.Communication
{
    public sealed class CommunicationModelValidator : AbstractValidator<CommunicationModel>
    {
        public CommunicationModelValidator()
        {
            RuleFor(e => e.Title)
                .NotEmpty()
                .WithMessage("Наименование адаптера параметр обязательный для заполнения.")
                .MaximumLength(64)
                .WithMessage("Наименование адаптера должно содержать не больше 64 символов.");

            RuleFor(e => e.Description)
                .NotNull()
                .WithMessage("Описание адаптера не может принимать значение null.")
                .MaximumLength(500)
                .WithMessage("Описание адаптера должно содержать не больше 500 символов.");

            RuleFor(e => e.Protocols)
                .NotNull()
                .WithMessage("Перечень протоколов не может принимать значение null.");

            RuleForEach(e => e.Protocols)
                .SetValidator(new ProtocolShortModelValidator());
        }
    }
}
