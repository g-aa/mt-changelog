using System;

namespace Mt.ChangeLog.TransferObjects.ProjectStatus
{
    public class ProjectStatusModel : ProjectStatusTableModel
    {
        public ProjectStatusModel()
        {
            this.Id = Guid.NewGuid();
        }
        
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
