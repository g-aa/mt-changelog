using System;

namespace Mt.ChangeLog.TransferObjects.ArmEdit
{
    public class ArmEditModel : ArmEditTableModel
    {
        public ArmEditModel()
        {
            this.Id = Guid.NewGuid();
            this.Date = DateTime.Now;
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
