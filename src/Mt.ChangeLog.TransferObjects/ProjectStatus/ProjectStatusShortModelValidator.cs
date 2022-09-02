using FluentValidation;

namespace Mt.ChangeLog.TransferObjects.ProjectStatus
{
    /// <summary>
    /// Валидатор модели <see cref="ProjectStatusShortModel"/>.
    /// </summary>
    public sealed class ProjectStatusShortModelValidator : AbstractValidator<ProjectStatusShortModel>
    {
        /// <summary>
        /// Инициализация экземпляра <see cref="ProjectStatusShortModelValidator"/>
        /// </summary>
        public ProjectStatusShortModelValidator()
        {
            this.RuleFor(e => e.Title)
                .NotEmpty()
                .WithMessage("Наименование статуса параметр обязательный для заполнения.")
                .Must(e => e.Trim().Length == e.Length)
                .WithMessage("Наименование статуса не должно содержать пробелов и табов в начале и конце строки.")
                .MaximumLength(32)
                .WithMessage("Наименование статуса должно содержать не более 32 символов.");
        }
    }
}