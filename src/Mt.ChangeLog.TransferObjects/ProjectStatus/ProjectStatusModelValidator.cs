using FluentValidation;

namespace Mt.ChangeLog.TransferObjects.ProjectStatus
{
    public sealed class ProjectStatusModelValidator : AbstractValidator<ProjectStatusModel>
    {
        public ProjectStatusModelValidator()
        {
            RuleFor(e => e.Title)
                .NotEmpty()
                .WithMessage("Наименование статуса параметр обязательный для заполнения.")
                .MaximumLength(32)
                .WithMessage("Наименование статуса должно содержать не более 32 символов.");

            RuleFor(e => e.Description)
                .NotNull()
                .WithMessage("Описание статуса не может принимать значение null.")
                .MaximumLength(500)
                .WithMessage("Описание статуса должно содержать не больше 500 символов.");
        }
    }
}
