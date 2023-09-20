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
        this.Title = DefaultString.TextFileName;
        this.Bytes = Array.Empty<byte>();
    }

    /// <summary>
    /// Инициализация экземпляра класса <see cref="FileModel"/>.
    /// </summary>
    /// <param name="title">Наименование файла.</param>
    /// <param name="bytes">Данные файла в бинарном формате.</param>
    protected FileModel(string title, IEnumerable<byte> bytes)
        : this()
    {
        if (!string.IsNullOrWhiteSpace(title) && bytes is not null)
        {
            this.Title = title;
            this.Bytes = bytes.ToArray();
        }
    }

    /// <summary>
    /// Наименование файла.
    /// </summary>
    public string Title { get; protected set; }

    /// <summary>
    /// Данные файла в бинарном формате.
    /// </summary>
    public byte[] Bytes { get; protected set; }
}