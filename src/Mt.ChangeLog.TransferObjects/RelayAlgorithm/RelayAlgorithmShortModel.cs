using System;

namespace Mt.ChangeLog.TransferObjects.RelayAlgorithm
{
    public class RelayAlgorithmShortModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }

        public override string ToString()
        {
            return this.Title;
        }
    }
}
