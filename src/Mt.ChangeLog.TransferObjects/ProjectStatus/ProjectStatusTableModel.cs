using Mt.Utilities;

namespace Mt.ChangeLog.TransferObjects.ProjectStatus
{
    /// <summary>
    /// Модель статуса проекта для таблиц.
    /// </summary>
    public class ProjectStatusTableModel : ProjectStatusShortModel
    {
        /// <summary>
        /// Описание.
        /// </summary>
        /// <example>Описание...</example>
        public string Description { get; set; }

        /// <summary>
        /// Инициализация экземпляра <see cref="ProjectStatusTableModel"/>.
        /// </summary>
        public ProjectStatusTableModel() : base()
        {
            this.Description = DefaultString.Description;
        }
    }
}