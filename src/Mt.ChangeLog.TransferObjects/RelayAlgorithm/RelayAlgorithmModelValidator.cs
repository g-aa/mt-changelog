using FluentValidation;

namespace Mt.ChangeLog.TransferObjects.RelayAlgorithm
{
    public sealed class RelayAlgorithmModelValidator : AbstractValidator<RelayAlgorithmModel>
    {
        public RelayAlgorithmModelValidator()
        {
            RuleFor(e => e.Title)
                .NotEmpty()
                .WithMessage("Наименование алгоритма параметр обязательный для заполнения.")
                .MaximumLength(32)
                .WithMessage("Наименование алгоритма должно содержать не больше 32 символов.");

            RuleFor(e => e.Group)
                .NotNull()
                .WithMessage("Наименование группы алгоритмов не может принимать значение null.")
                .MaximumLength(32)
                .WithMessage("Наименование группы алгоритмов должно содержать не больше 32 символо.");

            RuleFor(e => e.ANSI)
                .NotEmpty()
                .WithMessage("Код ANSI параметр обязательный для заполнения.")
                .Matches("^[0-9 A-Z -/]{1,32}$")
                .WithMessage("Код ANSI может содержать следующие символы 0-9, A-Z, -, /, но не более 32.");

            RuleFor(e => e.LogicalNode)
                .NotEmpty()
                .WithMessage("Logical node параметр обязательный для заполнения.")
                .Matches("^[0-9 A-Z -/]{1,32}$")
                .WithMessage("Logical node может содержать следующие символы 0-9, A-Z, -, /, но не более 32.");

            RuleFor(e => e.Description)
                .NotNull()
                .WithMessage("Описание алгоритма не может принимать значение null.")
                .MaximumLength(500)
                .WithMessage("Описание алгоритма должно содержать не больше 500 символов.");
        }
    }
}
