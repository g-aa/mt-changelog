using FluentValidation;

namespace Mt.ChangeLog.TransferObjects.Author
{
    /// <summary>
    /// Валидатор модели <see cref="AuthorModel"/>.
    /// </summary>
    public sealed class AuthorModelValidator : AbstractValidator<AuthorModel>
    {
        /// <summary>
        /// Инициализация экземпляра <see cref="AuthorModelValidator"/>
        /// </summary>
        public AuthorModelValidator()
        {
            this.RuleFor(e => e.FirstName)
                .NotEmpty()
                .WithMessage("Имя параметр обязательный для заполнения.")
                .Must(e => e.Trim().Length == e.Length)
                .WithMessage("Имя не должно содержать пробелов и табов в начале и конце строки.")
                .MaximumLength(32)
                .WithMessage("Имя должно содержать не больше 32 символов.");

            this.RuleFor(e => e.LastName)
                .NotEmpty()
                .WithMessage("Фамилия параметр обязательный для заполнения.")
                .Must(e => e.Trim().Length == e.Length)
                .WithMessage("Фамилия не должна содержать пробелов и табов в начале и конце строки.")
                .MaximumLength(32)
                .WithMessage("Фамилия должна содержать не больше 32 символов.");

            this.RuleFor(e => e.Position)
                .NotNull()
                .WithMessage("Должность автора не может принимать значение null.")
                .Must(e => e.Trim().Length == e.Length)
                .WithMessage("Должность не должна содержать пробелов и табов в начале и конце строки.")
                .MaximumLength(250)
                .WithMessage("Должность автора должна содержать не больше 250 символов");
        }
    }
}