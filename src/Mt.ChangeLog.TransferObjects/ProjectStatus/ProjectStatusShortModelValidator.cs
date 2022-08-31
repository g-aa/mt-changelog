using FluentValidation;

namespace Mt.ChangeLog.TransferObjects.ProjectStatus
{
    public sealed class ProjectStatusShortModelValidator : AbstractValidator<ProjectStatusShortModel>
    {
        public ProjectStatusShortModelValidator()
        {
            RuleFor(e => e.Title)
                .NotEmpty()
                .WithMessage("Наименование статуса параметр обязательный для заполнения.")
                .MaximumLength(32)
                .WithMessage("Наименование статуса должно содержать не более 32 символов.");
        }
    }
}
