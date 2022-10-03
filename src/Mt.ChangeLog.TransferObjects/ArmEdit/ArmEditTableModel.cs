using Mt.Utilities;
using System;

namespace Mt.ChangeLog.TransferObjects.ArmEdit
{
    /// <summary>
    /// Модель аналогового модуля для таблиц.
    /// </summary>
    public class ArmEditTableModel : ArmEditShortModel
    {
        /// <summary>
        /// ДИВГ.
        /// </summary>
        public string DIVG { get; set; }

        /// <summary>
        /// Дата и время компиляции.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Описание.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Инициализация экземпляра <see cref="ArmEditTableModel"/>.
        /// </summary>
        public ArmEditTableModel() : base()
        {
            this.DIVG = DefaultString.DIVG;
            this.Date = DateTime.Now;
            this.Description = DefaultString.Description;
        }
    }
}