using FluentValidation;

namespace Mt.ChangeLog.TransferObjects.Protocol
{
    /// <summary>
    /// Валидатор модели <see cref="ProtocolShortModel"/>.
    /// </summary>
    public sealed class ProtocolShortModelValidator : AbstractValidator<ProtocolShortModel>
    {
        /// <summary>
        /// Инициализация экземпляра <see cref="ProtocolShortModelValidator"/>
        /// </summary>
        public ProtocolShortModelValidator()
        {
            this.RuleFor(e => e.Title)
                .NotEmpty()
                .WithMessage("Наименование протокола параметр обязательный для заполнения.")
                .Must(e => e.Trim().Length == e.Length)
                .WithMessage("Наименование протокола не должно содержать пробелов и табов в начале и конце строки.")
                .MaximumLength(32)
                .WithMessage("Наименование протокола должно содержать не более 32 символов.");
        }
    }
}