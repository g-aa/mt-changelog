using FluentValidation;

namespace Mt.ChangeLog.TransferObjects.RelayAlgorithm
{
    /// <summary>
    /// Валидатор модели <see cref="RelayAlgorithmModel"/>.
    /// </summary>
    public sealed class RelayAlgorithmModelValidator : AbstractValidator<RelayAlgorithmModel>
    {
        /// <summary>
        /// Инициализация экземпляра <see cref="RelayAlgorithmModelValidator"/>.
        /// </summary>
        public RelayAlgorithmModelValidator()
        {
            this.Include(new RelayAlgorithmShortModelValidator());
            
            this.RuleFor(e => e.Group)
                .NotNull()
                .WithMessage("Наименование группы алгоритмов не может принимать значение null.")
                .Must(e => e.Trim().Length == e.Length)
                .WithMessage("Наименование группы алгоритмов не должно содержать пробелов и табов в начале и конце строки.")
                .MaximumLength(32)
                .WithMessage("Наименование группы алгоритмов должно содержать не больше 32 символо.");

            this.RuleFor(e => e.ANSI)
                .NotEmpty()
                .WithMessage("Код ANSI параметр обязательный для заполнения.")
                .Matches("^[0-9 A-Z -/]{1,32}$")
                .WithMessage("Код ANSI может содержать следующие символы 0-9, A-Z, -, /, но не более 32.");

            this.RuleFor(e => e.LogicalNode)
                .NotEmpty()
                .WithMessage("Logical node параметр обязательный для заполнения.")
                .Matches("^[0-9 A-Z -/]{1,32}$")
                .WithMessage("Logical node может содержать следующие символы 0-9, A-Z, -, /, но не более 32.");

            this.RuleFor(e => e.Description)
                .NotNull()
                .WithMessage("Описание алгоритма не может принимать значение null.")
                .MaximumLength(500)
                .WithMessage("Описание алгоритма должно содержать не больше 500 символов.");
        }
    }
}