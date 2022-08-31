using FluentValidation;

namespace Mt.ChangeLog.TransferObjects.Communication
{
    public sealed class CommunicationShortModelValidator : AbstractValidator<CommunicationShortModel>
    {
        public CommunicationShortModelValidator()
        {
            RuleFor(e => e.Title)
                .NotEmpty()
                .WithMessage("Наименование адаптера параметр обязательный для заполнения.")
                .MaximumLength(64)
                .WithMessage("Наименование адаптера должно содержать не больше 64 символов.");
        }
    }
}
