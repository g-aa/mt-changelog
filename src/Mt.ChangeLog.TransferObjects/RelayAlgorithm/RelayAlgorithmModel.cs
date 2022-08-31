using System;

namespace Mt.ChangeLog.TransferObjects.RelayAlgorithm
{
    public class RelayAlgorithmModel : RelayAlgorithmTableModel
    {
        public RelayAlgorithmModel()
        {
            this.Id = Guid.NewGuid();
        }

        public override string ToString()
        {
            return $"ANSI: {this.ANSI}, {base.ToString()}";
        }
    }
}
