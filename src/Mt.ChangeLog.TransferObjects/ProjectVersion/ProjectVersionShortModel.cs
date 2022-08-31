using System;

namespace Mt.ChangeLog.TransferObjects.ProjectVersion
{
    public class ProjectVersionShortModel
    {
        public Guid Id { get; set; }
        public string Prefix { get; set; }
        public string Title { get; set; }
        public string Version { get; set; }

        public override string ToString()
        {
            return $"{this.Prefix}-{this.Title}-{this.Version}";
        }
    }
}
