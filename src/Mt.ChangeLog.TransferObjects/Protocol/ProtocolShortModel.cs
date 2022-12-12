using Mt.Utilities;
using System;

namespace Mt.ChangeLog.TransferObjects.Protocol
{
    /// <summary>
    /// Краткая модель протокола информационнго обмена.
    /// </summary>
    public class ProtocolShortModel
    {
        /// <summary>
        /// ИД.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Наименование.
        /// </summary>
        /// <example>Modbus-MT</example>
        public string Title { get; set; }

        /// <summary>
        /// Инициализация экземпляра <see cref="ProtocolShortModel"/>.
        /// </summary>
        public ProtocolShortModel()
        {
            this.Id = Guid.NewGuid();
            this.Title = DefaultString.Protocol;
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return this.Title;
        }
    }
}