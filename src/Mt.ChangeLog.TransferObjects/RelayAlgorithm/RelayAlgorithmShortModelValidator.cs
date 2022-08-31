using FluentValidation;

namespace Mt.ChangeLog.TransferObjects.RelayAlgorithm
{
    public sealed class RelayAlgorithmShortModelValidator : AbstractValidator<RelayAlgorithmShortModel>
    {
        public RelayAlgorithmShortModelValidator()
        {
            RuleFor(e => e.Title)
                .NotEmpty()
                .WithMessage("Наименование алгоритма параметр обязательный для заполнения.")
                .MaximumLength(32)
                .WithMessage("Наименование алгоритма должно содержать не больше 32 символов.");
        }
    }
}
