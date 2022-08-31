using Mt.ChangeLog.TransferObjects.AnalogModule;
using Mt.ChangeLog.TransferObjects.Platform;
using Mt.ChangeLog.TransferObjects.ProjectStatus;

namespace Mt.ChangeLog.TransferObjects.ProjectVersion
{
    public class ProjectVersionModel : ProjectVersionShortModel
    {
        public string DIVG { get; set; }
        public string Description { get; set; }
        public ProjectStatusShortModel ProjectStatus { get; set; }
        public AnalogModuleShortModel AnalogModule { get; set; }
        public PlatformShortModel Platform { get; set; }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
