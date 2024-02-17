using System.ComponentModel.DataAnnotations;

namespace Mt.ChangeLog.TransferObjects.Other;

/// <summary>
/// Модель данных сообщения.
/// </summary>
public sealed class MessageModel
{
    /// <summary>
    /// Инициализация экземпляра класса <see cref="MessageModel"/>.
    /// </summary>
    public MessageModel()
    {
        Message = "Текст сообщения.";
    }

    /// <summary>
    /// Сообщение.
    /// </summary>
    /// <example>Текстовое сообщение...</example>
    [Required]
    [MinLength(1)]
    public string Message { get; set; }
}