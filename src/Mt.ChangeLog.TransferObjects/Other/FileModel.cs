using System.ComponentModel.DataAnnotations;

using Mt.Utilities;

namespace Mt.ChangeLog.TransferObjects.Other;

/// <summary>
/// Модель файла данных.
/// </summary>
public abstract class FileModel
{
    /// <summary>
    /// Инициализация экземпляра класса <see cref="FileModel"/>.
    /// </summary>
    protected FileModel()
    {
        Title = DefaultString.TextFileName;
        Bytes = Array.Empty<byte>();
    }

    /// <summary>
    /// Инициализация экземпляра класса <see cref="FileModel"/>.
    /// </summary>
    /// <param name="title">Наименование файла.</param>
    /// <param name="bytes">Данные файла в бинарном формате.</param>
    protected FileModel(string title, IReadOnlyCollection<byte> bytes)
        : this()
    {
        if (!string.IsNullOrWhiteSpace(title) && bytes is not null)
        {
            Title = title;
            Bytes = bytes.ToArray();
        }
    }

    /// <summary>
    /// Наименование файла.
    /// </summary>
    [Required]
    [MinLength(1)]
    public string Title { get; protected set; }

    /// <summary>
    /// Данные файла в бинарном формате.
    /// </summary>
    [Required]
    public IReadOnlyCollection<byte> Bytes { get; protected set; }
}