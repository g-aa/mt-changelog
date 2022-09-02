using Mt.ChangeLog.TransferObjects.AnalogModule;
using Mt.ChangeLog.TransferObjects.Platform;
using Mt.ChangeLog.TransferObjects.ProjectStatus;
using Mt.Utilities;

namespace Mt.ChangeLog.TransferObjects.ProjectVersion
{
    /// <summary>
    /// Полная модель версии проекта.
    /// </summary>
    public class ProjectVersionModel : ProjectVersionShortModel
    {
        /// <summary>
        /// ДИВГ.
        /// </summary>
        public string DIVG { get; set; }
        
        /// <summary>
        /// Описание.
        /// </summary>
        public string Description { get; set; }
        
        /// <summary>
        /// Статус.
        /// </summary>
        public ProjectStatusShortModel ProjectStatus { get; set; }
        
        /// <summary>
        /// Аналоговый модуль.
        /// </summary>
        public AnalogModuleShortModel AnalogModule { get; set; }
        
        /// <summary>
        /// Платформа.
        /// </summary>
        public PlatformShortModel Platform { get; set; }

        /// <summary>
        /// Инициализация экземпляра <see cref="ProjectVersionModel"/>
        /// </summary>
        public ProjectVersionModel() : base()
        {
            this.DIVG = DefaultString.DIVG;
            this.Description = DefaultString.Description;
            this.ProjectStatus = new ProjectStatusShortModel();
            this.AnalogModule = new AnalogModuleShortModel();
            this.Platform = new PlatformShortModel();
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return base.ToString();
        }
    }
}