using Mt.ChangeLog.TransferObjects.ProjectVersion;

namespace Mt.ChangeLog.TransferObjects.ProjectRevision
{
    /// <summary>
    /// Краткая модель редакции проекта.
    /// </summary>
    public class ProjectRevisionShortModel : ProjectVersionShortModel
    {
        /// <summary>
        /// Редакция
        /// </summary>
        public string Revision { get; set; }

        /// <summary>
        /// Инициализация экземпляра <see cref="ProjectRevisionShortModel"/>
        /// </summary>
        public ProjectRevisionShortModel() : base()
        {
            this.Revision = "00";
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"{base.ToString()}_{this.Revision}";
        }
    }
}