using Mt.Utilities;

namespace Mt.ChangeLog.Entities.Views;

/// <summary>
/// Представление автор общий вклад в проекты.
/// </summary>
public class AuthorContributionView
{
    /// <summary>
    /// Инициализация экземпляра класса <see cref="AuthorContributionView"/>.
    /// </summary>
    public AuthorContributionView()
    {
        Author = $"{DefaultString.LastName} {DefaultString.FirstName}";
        Contribution = 0;
    }

    /// <summary>
    /// Ф.И.О. автора (LastName FirstName).
    /// </summary>
    public string Author { get; set; }

    /// <summary>
    /// Общий вклад в проекты.
    /// </summary>
    public int Contribution { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return $"{Author}, вклад: {Contribution}";
    }
}