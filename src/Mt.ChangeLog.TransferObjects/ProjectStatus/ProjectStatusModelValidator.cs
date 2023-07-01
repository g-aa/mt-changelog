using FluentValidation;

namespace Mt.ChangeLog.TransferObjects.ProjectStatus
{
    /// <summary>
    /// Валидатор модели <see cref="ProjectStatusModel"/>.
    /// </summary>
    public sealed class ProjectStatusModelValidator : AbstractValidator<ProjectStatusModel>
    {
        /// <summary>
        /// Инициализация экземпляра <see cref="ProjectStatusModelValidator"/>.
        /// </summary>
        public ProjectStatusModelValidator()
        {
            this.Include(new ProjectStatusShortModelValidator());

            this.RuleFor(e => e.Description)
                .NotNull()
                .WithMessage("Описание статуса не может принимать значение null.")
                .MaximumLength(500)
                .WithMessage("Описание статуса должно содержать не больше 500 символов.");
        }
    }
}