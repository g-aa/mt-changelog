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
    [Required]
    public Guid Id { get; set; }

    /// <inheritdoc/>
    public override string ToString()
    {
        return $"ID: {Id}";
    }
}