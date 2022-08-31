using FluentValidation;

namespace Mt.ChangeLog.TransferObjects.Author
{
    public sealed class AuthorModelValidator : AbstractValidator<AuthorModel>
    {
        public AuthorModelValidator()
        {
            RuleFor(e => e.FirstName)
                .NotEmpty()
                .WithMessage("Имя параметр обязательный для заполнения.")
                .MaximumLength(32)
                .WithMessage("Имя должно содержать не больше 32 символов.");

            RuleFor(e => e.LastName)
                .NotEmpty()
                .WithMessage("Фамилия параметр обязательный для заполнения.")
                .MaximumLength(32)
                .WithMessage("Фамилия должна содержать не больше 32 символов.");

            RuleFor(e => e.Position)
                .NotNull()
                .WithMessage("Должность автора не может принимать значение null.")
                .MaximumLength(250)
                .WithMessage("Должность автора должна содержать не больше 250 символов");
        }
    }
}
