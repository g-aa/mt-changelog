using System.ComponentModel.DataAnnotations;

using Mt.Utilities;

namespace Mt.ChangeLog.TransferObjects.Communication;

/// <summary>
/// Краткая модель коммуникационного модуля.
/// </summary>
public class CommunicationShortModel
{
    /// <summary>
    /// Инициализация экземпляра <see cref="CommunicationShortModel"/>.
    /// </summary>
    public CommunicationShortModel()
    {
        Id = Guid.NewGuid();
        Title = DefaultString.Communication;
    }

    /// <summary>
    /// ИД.
    /// </summary>
    [Required]
    public Guid Id { get; set; }

    /// <summary>
    /// Наименование.
    /// </summary>
    /// <example>АК Virtual</example>
    [Required]
    [StringLength(64, MinimumLength = 1)]
    public string Title { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return Title;
    }
}