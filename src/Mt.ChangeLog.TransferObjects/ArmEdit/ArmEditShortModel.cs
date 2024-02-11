using System.ComponentModel.DataAnnotations;

using Mt.Utilities;

namespace Mt.ChangeLog.TransferObjects.ArmEdit;

/// <summary>
/// Краткая модель ArmEdit.
/// </summary>
public class ArmEditShortModel
{
    /// <summary>
    /// Инициализация экземпляра <see cref="ArmEditModel"/>.
    /// </summary>
    public ArmEditShortModel()
    {
        Id = Guid.NewGuid();
        Version = DefaultString.Version;
    }

    /// <summary>
    /// ИД.
    /// </summary>
    [Required]
    public Guid Id { get; set; }

    /// <summary>
    /// Версия ArmEdit.
    /// </summary>
    /// <example>v0.00.00.00</example>
    [Required]
    [RegularExpression(StringFormat.Version)]
    public string Version { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return $"ArmEdit: {Version}";
    }
}