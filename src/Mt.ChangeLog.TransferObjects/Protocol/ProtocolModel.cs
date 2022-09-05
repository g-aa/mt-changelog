using Mt.ChangeLog.TransferObjects.Communication;
using Mt.Utilities;
using System.Collections.Generic;

namespace Mt.ChangeLog.TransferObjects.Protocol
{
    /// <summary>
    /// Полная модель протокола информационнго обмена.
    /// </summary>
    public class ProtocolModel : ProtocolShortModel
    {
        /// <summary>
        /// Описание.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Перечень коммуникационных модулей.
        /// </summary>
        public IEnumerable<CommunicationShortModel> Communications { get; set; }

        /// <summary>
        /// Инициализация экземпляра <see cref="ProtocolModel"/>.
        /// </summary>
        public ProtocolModel() : base()
        {
            this.Description = DefaultString.Description;
            this.Communications = new HashSet<CommunicationShortModel>();
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return base.ToString();
        }
    }
}