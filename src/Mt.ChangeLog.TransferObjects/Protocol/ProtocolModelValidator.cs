using FluentValidation;
using Mt.ChangeLog.TransferObjects.Communication;

namespace Mt.ChangeLog.TransferObjects.Protocol
{
    public sealed class ProtocolModelValidator : AbstractValidator<ProtocolModel>
    {
        public ProtocolModelValidator()
        {
            RuleFor(e => e.Title)
                .NotEmpty()
                .WithMessage("Наименование протокола параметр обязательный для заполнения.")
                .MaximumLength(32)
                .WithMessage("Наименование протокола должно содержать не более 32 символов.");

            RuleFor(e => e.Description)
                .NotNull()
                .WithMessage("Описание протокола не может принимать значение null.")
                .MaximumLength(500)
                .WithMessage("Описание протокола должно содержать не больше 500 символов.");

            RuleFor(e => e.Communications)
                .NotNull()
                .WithMessage("Перечень модулей не может принимать значение null.");

            RuleForEach(e => e.Communications)
                .SetValidator(new CommunicationShortModelValidator());
        }
    }
}
