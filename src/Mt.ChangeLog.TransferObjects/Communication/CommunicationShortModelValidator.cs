using FluentValidation;

namespace Mt.ChangeLog.TransferObjects.Communication
{
    /// <summary>
    /// Валидатор модели <see cref="CommunicationShortModel"/>.
    /// </summary>
    public sealed class CommunicationShortModelValidator : AbstractValidator<CommunicationShortModel>
    {
        /// <summary>
        /// Инициализация экземпляра <see cref="CommunicationShortModelValidator"/>.
        /// </summary>
        public CommunicationShortModelValidator()
        {
            this.RuleFor(e => e.Title)
                .NotEmpty()
                .WithMessage("Наименование адаптера параметр обязательный для заполнения.")
                .Must(e => e.Trim().Length == e.Length)
                .WithMessage("Наименование адаптера не должно содержать пробелов и табов в начале и конце строки.")
                .MaximumLength(64)
                .WithMessage("Наименование адаптера должно содержать не больше 64 символов.");
        }
    }
}