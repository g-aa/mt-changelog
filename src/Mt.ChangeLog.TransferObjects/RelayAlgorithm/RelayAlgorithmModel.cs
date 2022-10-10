namespace Mt.ChangeLog.TransferObjects.RelayAlgorithm
{
    /// <summary>
    /// Полная модель алгоритма РЗиА.
    /// </summary>
    public class RelayAlgorithmModel : RelayAlgorithmTableModel
    {
        /// <summary>
        /// Инициализация экземпляра <see cref="RelayAlgorithmModel"/>
        /// </summary>
        public RelayAlgorithmModel() : base()
        {
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"ANSI: {this.ANSI}, {base.ToString()}";
        }
    }
}