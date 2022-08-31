using System;

namespace Mt.ChangeLog.TransferObjects.ProjectStatus
{
    public class ProjectStatusShortModel
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public override string ToString()
        {
            return this.Title;
        }
    }
}
