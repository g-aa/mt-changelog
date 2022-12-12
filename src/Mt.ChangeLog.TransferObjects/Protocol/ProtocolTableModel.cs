using Mt.Utilities;

namespace Mt.ChangeLog.TransferObjects.Protocol
{
    /// <summary>
    /// Модель протокола информационнго обмена для таблиц.
    /// </summary>
    public class ProtocolTableModel : ProtocolShortModel
    {
        /// <summary>
        /// Описание.
        /// </summary>
        /// <example>Описание...</example>
        public string Description { get; set; }

        /// <summary>
        /// Инициализация экземпляра <see cref="ProtocolTableModel"/>.
        /// </summary>
        public ProtocolTableModel() : base()
        {
            this.Description = DefaultString.Description;
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return this.Title;
        }
    }
}