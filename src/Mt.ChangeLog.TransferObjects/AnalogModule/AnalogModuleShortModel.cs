using System;

namespace Mt.ChangeLog.TransferObjects.AnalogModule
{
    /// <summary>
    /// Краткая модель аналогового модуля.
    /// </summary>
    public class AnalogModuleShortModel
    {
        /// <summary>
        /// ИД.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Наименование.
        /// </summary>
        public string Title { get; set; }

        /// <inheritdoc />
        public override string ToString()
        {
            return this.Title;
        }
    }
}
