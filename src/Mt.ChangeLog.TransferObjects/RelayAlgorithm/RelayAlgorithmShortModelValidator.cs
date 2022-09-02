using FluentValidation;

namespace Mt.ChangeLog.TransferObjects.RelayAlgorithm
{
    /// <summary>
    /// Валидатор модели <see cref="RelayAlgorithmShortModel"/>.
    /// </summary>
    public sealed class RelayAlgorithmShortModelValidator : AbstractValidator<RelayAlgorithmShortModel>
    {
        /// <summary>
        /// Инициализация экземпляра <see cref="RelayAlgorithmShortModelValidator"/>
        /// </summary>
        public RelayAlgorithmShortModelValidator()
        {
            this.RuleFor(e => e.Title)
                .NotEmpty()
                .WithMessage("Наименование алгоритма параметр обязательный для заполнения.")
                .Must(e => e.Trim().Length == e.Length)
                .WithMessage("Наименование алгоритма не должно содержать пробелов и табов в начале и конце строки.")
                .MaximumLength(32)
                .WithMessage("Наименование алгоритма должно содержать не больше 32 символов.");
        }
    }
}