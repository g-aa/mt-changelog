using Mt.Utilities;

namespace Mt.ChangeLog.TransferObjects.Author
{
    /// <summary>
    /// Модель автор вклад в проекты.
    /// </summary>
    public class AuthorProjectContributionModel : AuthorContributionModel
    {
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

        /// <summary>
        /// Инициализация экземпляра <see cref="AuthorProjectContributionModel"/>.
        /// </summary>
        public AuthorProjectContributionModel()
        {
            this.ProjectPrefix = DefaultString.Prefix;
            this.ProjectTitle = DefaultString.Project;
            this.ProjectVersion = DefaultString.Revision;    
        }
    }
}