using System;

namespace Mt.ChangeLog.TransferObjects.ArmEdit
{
    public class ArmEditTableModel : ArmEditShortModel
    {
        public string DIVG { get; set; }

        public DateTime Date { get; set; }

        public string Description { get; set; }
    }
}
