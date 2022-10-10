using Mt.Utilities;

namespace Mt.ChangeLog.TransferObjects.ProjectVersion
{
    /// <summary>
    /// Модель версии проекта для таблиц.
    /// </summary>
    public class ProjectVersionTableModel : ProjectVersionShortModel
    {
        /// <summary>
        /// ДИВГ.
        /// </summary>
        public string DIVG { get; set; }
        
        /// <summary>
        /// Статус.
        /// </summary>
        public string Status { get; set; }
        
        /// <summary>
        /// Описание.
        /// </summary>
        public string Description { get; set; }
        
        /// <summary>
        /// Платформа.
        /// </summary>
        public string Platform { get; set; }
        
        /// <summary>
        /// Аналоговый модуль.
        /// </summary>
        public string Module { get; set; }

        /// <summary>
        /// Инициализация экземпляра <see cref="ProjectVersionTableModel"/>.
        /// </summary>
        public ProjectVersionTableModel() : base()
        {
            this.DIVG = DefaultString.DIVG;
            this.Status = "Внутренний";
            this.Description = DefaultString.Description;
            this.Platform = DefaultString.Platform;
            this.Module = DefaultString.AnalogModule;
        }
    }
}