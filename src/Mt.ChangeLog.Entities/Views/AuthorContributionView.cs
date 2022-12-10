using Mt.Utilities;

namespace Mt.ChangeLog.Entities.Views
{
    /// <summary>
    /// Представление автор общий вклад в проекты.
    /// </summary>
    public class AuthorContributionView
    {
        /// <summary>
        /// Ф.И.О. автора (LastName FirstName). 
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// Общий вклад в проекты.
        /// </summary>
        public int Contribution { get; set; }

        /// <summary>
        /// Инициализация экземпляра класса <see cref="AuthorContributionView"/>.
        /// </summary>
        public AuthorContributionView()
        {
            this.Author = $"{DefaultString.LastName} {DefaultString.FirstName}";
            this.Contribution = 0;
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"{this.Author}, вклад: {this.Contribution}";
        }
    }
}