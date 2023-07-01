using FluentValidation;

namespace Mt.ChangeLog.TransferObjects.Other
{
    /// <summary>
    /// Валидатор модели <see cref="BaseModel"/>.
    /// </summary>
    public sealed class BaseModelValidator : AbstractValidator<BaseModel>
    {
        /// <summary>
        /// Инициализация экземпляра <see cref="BaseModelValidator"/>.
        /// </summary>
        public BaseModelValidator()
        {
            this.RuleFor(x => x.Id)
                .Must(id => id != Guid.Empty)
                .WithMessage("Параметр Id не должен принимать пустое значение (00000000-0000-0000-0000-000000000000).");
        }
    }
}