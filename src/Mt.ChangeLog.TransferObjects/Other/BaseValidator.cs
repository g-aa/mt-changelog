using FluentValidation;

namespace Mt.ChangeLog.TransferObjects.Other;

/// <summary>
/// Валидатор модели <see cref="BaseModel"/>.
/// </summary>
public sealed class BaseValidator : AbstractValidator<BaseModel>
{
    /// <summary>
    /// Инициализация экземпляра <see cref="BaseValidator"/>.
    /// </summary>
    public BaseValidator()
    {
        this.RuleFor(x => x.Id)
            .Must(id => id != Guid.Empty);
    }
}