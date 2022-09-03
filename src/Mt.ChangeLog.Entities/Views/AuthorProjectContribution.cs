namespace Mt.ChangeLog.Entities.Views
{
    /// <summary>
    /// Представление автор вклад в проект.
    /// </summary>
    public class AuthorProjectContribution
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
        /// Префикс наименования проекта.
        /// </summary>
        public string ProjectPrefix { get; set; }

        /// <summary>
        /// Заголовок проекта.
        /// </summary>
        public string ProjectTitle { get; set; }

        /// <summary>
        /// Версия проекта.
        /// </summary>
        public string ProjectVersion { get; set; }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"{this.Author}, {this.ProjectPrefix}-{this.ProjectTitle}-{this.ProjectVersion}, вклад: {this.Contribution}";
        }
    }
}