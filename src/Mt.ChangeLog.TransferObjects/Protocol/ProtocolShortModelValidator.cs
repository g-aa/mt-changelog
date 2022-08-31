using FluentValidation;

namespace Mt.ChangeLog.TransferObjects.Protocol
{
    public sealed class ProtocolShortModelValidator : AbstractValidator<ProtocolShortModel>
    {
        public ProtocolShortModelValidator()
        {
            RuleFor(e => e.Title)
                .NotEmpty()
                .WithMessage("Наименование протокола параметр обязательный для заполнения.")
                .MaximumLength(32)
                .WithMessage("Наименование протокола должно содержать не более 32 символов.");
        }
    }
}
