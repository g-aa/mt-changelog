namespace Mt.ChangeLog.Entities.Views
{
    /// <summary>
    /// Представление автор общий вклад в проекты.
    /// </summary>
    public class AuthorContribution
    {
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
            return $"{this.Author}, вклад: {this.Contribution}";
        }
    }
}