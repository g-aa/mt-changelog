using Mt.ChangeLog.TransferObjects.Platform;
using System;
using System.Collections.Generic;

namespace Mt.ChangeLog.TransferObjects.AnalogModule
{
    /// <summary>
    /// Полная модель аналогового модуля.
    /// </summary>
    public class AnalogModuleModel : AnalogModuleTableModel
    {
        /// <summary>
        /// Перечень платформ.
        /// </summary>
        public IEnumerable<PlatformShortModel> Platforms { get; set; }

        /// <summary>
        /// Инициализация экземпляра <see cref="AnalogModuleModel"/>
        /// </summary>
        public AnalogModuleModel()
        {
            this.Id = Guid.NewGuid();
            this.Platforms = new HashSet<PlatformShortModel>();
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"{base.ToString()}, номинальный ток: {this.Current}";
        }
    }
}
