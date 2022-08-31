using System;

namespace Mt.ChangeLog.TransferObjects.Historical
{
    public class ProjectRevisionTreeModel
    {
        public Guid ParentId { get; set; }
        public Guid Id { get; set; }
        public string Prefix { get; set; }
        public string Title { get; set; }
        public string Version { get; set; }
        public string Revision { get; set; }
        public string ArmEdit { get; set; }
        public string Date { get; set; }
        public string Platform { get; set; }
    }
}
