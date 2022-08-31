using System;

namespace Mt.ChangeLog.TransferObjects.ProjectRevision
{
    public class LastProjectRevisionModel
    {
        public string Prefix { get; set; }
        public string Title { get; set; }
        public string Version { get; set; }
        public string Revision { get; set; }
        public string Platform { get; set; }
        public string ArmEdit { get; set; }
        public DateTime Date { get; set; }
    }
}
