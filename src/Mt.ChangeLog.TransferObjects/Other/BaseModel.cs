using System.ComponentModel.DataAnnotations;

namespace Mt.ChangeLog.TransferObjects.Other;

/// <summary>
/// Базовое представление модели данных.
/// </summary>
public class BaseModel
{
    /// <summary>
    /// Идентификатор модели данных.
    /// </summary>
    /// <example>00000000-0000-0000-0000-000000000000</example>
    [Required]
    public Guid Id { get; set; }

    /// <inheritdoc/>
    public override string ToString()
    {
        return $"ID: {Id}";
    }
}