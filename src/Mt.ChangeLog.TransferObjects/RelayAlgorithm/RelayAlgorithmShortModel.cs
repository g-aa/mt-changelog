using Mt.Utilities;
using System;

namespace Mt.ChangeLog.TransferObjects.RelayAlgorithm
{
    /// <summary>
    /// Краткая модель алгоритма РЗиА.
    /// </summary>
    public class RelayAlgorithmShortModel
    {
        /// <summary>
        /// ИД.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Наименование.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Инициализация экземпляра <see cref="RelayAlgorithmShortModel"/>
        /// </summary>
        public RelayAlgorithmShortModel()
        {
            this.Id = Guid.NewGuid();
            this.Title = DefaultString.Algorithm;
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return this.Title;
        }
    }
}