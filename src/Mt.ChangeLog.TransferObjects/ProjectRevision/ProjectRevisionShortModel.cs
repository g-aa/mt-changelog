using Mt.ChangeLog.TransferObjects.ProjectVersion;

namespace Mt.ChangeLog.TransferObjects.ProjectRevision
{
    public class ProjectRevisionShortModel : ProjectVersionShortModel
    {
        public string Revision { get; set; }

        public override string ToString()
        {
            return $"{base.ToString()}_{this.Revision}";
        }
    }
}
