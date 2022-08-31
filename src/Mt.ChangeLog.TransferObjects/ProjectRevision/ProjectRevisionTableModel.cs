using System;

namespace Mt.ChangeLog.TransferObjects.ProjectRevision
{
    public class ProjectRevisionTableModel : ProjectRevisionShortModel
    {
        public DateTime Date { get; set; }
        public string ArmEdit { get; set; }
        public string Reason { get; set; }
    }
}
