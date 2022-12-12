using Mt.Utilities;

namespace Mt.ChangeLog.TransferObjects.Author
{
    /// <summary>
    /// Модель автор общий вклад в проекты.
    /// </summary>
    public class AuthorContributionModel
    {
        /// <summary>
        /// Ф.И.О. автора (LastName FirstName). 
        /// </summary>
        /// <example>Иванов Иван</example>
        public string Author { get; set; }
        
        /// <summary>
        /// Общий вклад в проекты.
        /// </summary>
        public int Contribution { get; set; }

        /// <summary>
        /// Инициализация экземпляра <see cref="AuthorContributionModel"/>.
        /// </summary>
        public AuthorContributionModel()
        {
            this.Author = $"{DefaultString.LastName} {DefaultString.FirstName}";
        }
    }
}