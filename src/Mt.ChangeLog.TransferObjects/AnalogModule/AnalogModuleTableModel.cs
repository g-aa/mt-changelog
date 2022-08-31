namespace Mt.ChangeLog.TransferObjects.AnalogModule
{
    /// <summary>
    /// Модель аналогового модуля для таблиц.
    /// </summary>
    public class AnalogModuleTableModel : AnalogModuleShortModel
    {
        /// <summary>
        /// ДИВГ.
        /// </summary>
        public string DIVG { get; set; }
        
        /// <summary>
        /// Номинальный ток.
        /// </summary>
        public string Current { get; set; }
        
        /// <summary>
        /// Описание.
        /// </summary>
        public string Description { get; set; }
    }
}
