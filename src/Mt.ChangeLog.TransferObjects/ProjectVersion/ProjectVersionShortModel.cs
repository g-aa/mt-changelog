using Mt.Utilities;

namespace Mt.ChangeLog.TransferObjects.ProjectVersion
{
    /// <summary>
    /// Краткая модель версии проекта.
    /// </summary>
    public class ProjectVersionShortModel
    {
        /// <summary>
        /// ИД.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Префикс.
        /// </summary>
        /// <example>БФПО-000</example>
        public string Prefix { get; set; }

        /// <summary>
        /// Наименование.
        /// </summary>
        /// <example>ПМК</example>
        public string Title { get; set; }

        /// <summary>
        /// Версия.
        /// </summary>
        /// <example>00</example>
        public string Version { get; set; }

        /// <summary>
        /// Инициализация экземпляра <see cref="ProjectVersionShortModel"/>.
        /// </summary>
        public ProjectVersionShortModel()
        {
            this.Id = Guid.NewGuid();
            this.Prefix = DefaultString.Prefix;
            this.Title = DefaultString.Project;
            this.Version = DefaultString.Revision;
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"{this.Prefix}-{this.Title}-{this.Version}";
        }
    }
}