using Mt.Utilities;

namespace Mt.ChangeLog.TransferObjects.Communication
{
    /// <summary>
    /// Модель коммуникационного модуля для таблиц.
    /// </summary>
    public class CommunicationTableModel : CommunicationShortModel
    {
        /// <summary>
        /// Перечень протоколов через ','.
        /// </summary>
        public string Protocols { get; set; }
        
        /// <summary>
        /// Описание.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Инициализация экземпляра <see cref="CommunicationTableModel"/>.
        /// </summary>
        public CommunicationTableModel() : base()
        {
            this.Protocols = DefaultString.Protocol;
            this.Description = DefaultString.Description;
        }
    }
}