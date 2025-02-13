using FluentValidation;

using Mt.ChangeLog.TransferObjects.Other;

namespace Mt.ChangeLog.Logic.Validators;

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
        RuleFor(x => x.Id)
            .Must(id => id != Guid.Empty);
    }
}