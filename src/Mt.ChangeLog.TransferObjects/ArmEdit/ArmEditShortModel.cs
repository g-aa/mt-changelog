using System;

namespace Mt.ChangeLog.TransferObjects.ArmEdit
{
    public class ArmEditShortModel
    {
        public Guid Id { get; set; }

        public string Version { get; set; }

        public override string ToString()
        {
            return $"ArmEdit: {this.Version}";
        }
    }
}
