﻿using Mt.Utilities;

namespace Mt.ChangeLog.TransferObjects.RelayAlgorithm
{
    /// <summary>
    /// Модель алгоритма РЗиА для таблиц.
    /// </summary>
    public class RelayAlgorithmTableModel : RelayAlgorithmShortModel
    {
        /// <summary>
        /// Наименование группы.
        /// </summary>
        public string Group { get; set; }
        
        /// <summary>
        /// Код ANSI.
        /// </summary>
        public string ANSI { get; set; }
        
        /// <summary>
        /// Логический узел IEC-61850.
        /// </summary>
        public string LogicalNode { get; set; }
        
        /// <summary>
        /// Описание.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Инициализация экземпляра <see cref="RelayAlgorithmTableModel"/>
        /// </summary>
        public RelayAlgorithmTableModel() : base()
        {
            this.Group = DefaultString.AlgorithmGroup;
            this.ANSI = DefaultString.AlgorithmANSI;
            this.LogicalNode = DefaultString.AlgorithmLN;
            this.Description = DefaultString.Description;
        }
    }
}