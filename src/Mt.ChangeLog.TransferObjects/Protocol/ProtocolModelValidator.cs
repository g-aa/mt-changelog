using FluentValidation;
using Mt.ChangeLog.TransferObjects.Communication;

namespace Mt.ChangeLog.TransferObjects.Protocol
{
    /// <summary>
    /// Валидатор модели <see cref="ProtocolModel"/>.
    /// </summary>
    public sealed class ProtocolModelValidator : AbstractValidator<ProtocolModel>
    {
        /// <summary>
        /// Инициализация экземпляра <see cref="ProtocolModelValidator"/>.
        /// </summary>
        public ProtocolModelValidator()
        {
            this.Include(new ProtocolShortModelValidator());
            
            this.RuleFor(e => e.Description)
                .NotNull()
                .WithMessage("Описание протокола не может принимать значение null.")
                .MaximumLength(500)
                .WithMessage("Описание протокола должно содержать не больше 500 символов.");

            this.RuleFor(e => e.Communications)
                .NotNull()
                .WithMessage("Перечень модулей не может принимать значение null.");

            this.RuleForEach(e => e.Communications)
                .SetValidator(new CommunicationShortModelValidator());
        }
    }
}