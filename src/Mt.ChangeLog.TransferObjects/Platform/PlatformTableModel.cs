using Mt.Utilities;

namespace Mt.ChangeLog.TransferObjects.Platform
{
    /// <summary>
    /// Модель платформы БМРЗ для таблиц.
    /// </summary>
    public class PlatformTableModel : PlatformShortModel
    {
        /// <summary>
        /// Описание.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Инициализация экземпляра <see cref="PlatformTableModel"/>.
        /// </summary>
        public PlatformTableModel() : base()
        {
            this.Description = DefaultString.Description;
        }
    }
}