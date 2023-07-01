using FluentValidation;
using Mt.ChangeLog.TransferObjects.Protocol;

namespace Mt.ChangeLog.TransferObjects.Communication
{
    /// <summary>
    /// Валидатор модели <see cref="CommunicationModel"/>.
    /// </summary>
    public sealed class CommunicationModelValidator : AbstractValidator<CommunicationModel>
    {
        /// <summary>
        /// Инициализация экземпляра <see cref="CommunicationModelValidator"/>.
        /// </summary>
        public CommunicationModelValidator()
        {
            this.Include(new CommunicationShortModelValidator());

            this.RuleFor(e => e.Description)
                .NotNull()
                .WithMessage("Описание адаптера не может принимать значение null.")
                .MaximumLength(500)
                .WithMessage("Описание адаптера должно содержать не больше 500 символов.");

            this.RuleFor(e => e.Protocols)
                .NotNull()
                .WithMessage("Перечень протоколов не может принимать значение null.");

            this.RuleForEach(e => e.Protocols)
                .SetValidator(new ProtocolShortModelValidator());
        }
    }
}