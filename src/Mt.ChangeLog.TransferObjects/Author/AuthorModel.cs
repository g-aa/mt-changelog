namespace Mt.ChangeLog.TransferObjects.Author;

/// <summary>
/// Полная модель автора.
/// </summary>
public class AuthorModel : AuthorTableModel
{
    /// <summary>
    /// Инициализация экземпляра <see cref="AuthorModel"/>.
    /// </summary>
    public AuthorModel()
        : base()
    {
    }

    /// <inheritdoc />
    public override string ToString()
    {
        return $"{base.ToString()}, {this.Position}";
    }
}