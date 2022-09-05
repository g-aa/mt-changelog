using Mt.Utilities;
using System;

namespace Mt.ChangeLog.TransferObjects.ArmEdit
{
    /// <summary>
    /// Краткая модель ArmEdit.
    /// </summary>
    public class ArmEditShortModel
    {
        /// <summary>
        /// ИД.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Версия ArmEdit.
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// Инициализация экземпляра <see cref="ArmEditModel"/>.
        /// </summary>
        public ArmEditShortModel()
        {
            this.Id = Guid.NewGuid();
            this.Version = DefaultString.Version;
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"ArmEdit: {this.Version}";
        }
    }
}