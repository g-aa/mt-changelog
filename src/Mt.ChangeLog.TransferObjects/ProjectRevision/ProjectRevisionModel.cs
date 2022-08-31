using Mt.ChangeLog.TransferObjects.ArmEdit;
using Mt.ChangeLog.TransferObjects.Author;
using Mt.ChangeLog.TransferObjects.Communication;
using Mt.ChangeLog.TransferObjects.ProjectVersion;
using Mt.ChangeLog.TransferObjects.RelayAlgorithm;
using System;
using System.Collections.Generic;

namespace Mt.ChangeLog.TransferObjects.ProjectRevision
{
    public class ProjectRevisionModel
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string Revision { get; set; }
        public string Reason { get; set; }
        public string Description { get; set; }
        public ProjectVersionShortModel ProjectVersion { get; set; }
        public ProjectRevisionShortModel ParentRevision { get; set; }
        public CommunicationShortModel CommunicationModule { get; set; }
        public ArmEditShortModel ArmEdit { get; set; }
        public IEnumerable<AuthorShortModel> Authors { get; set; }
        public IEnumerable<RelayAlgorithmShortModel> RelayAlgorithms { get; set; }

        public ProjectRevisionModel()
        {
            this.Id = Guid.NewGuid();
            this.Authors = new HashSet<AuthorShortModel>();
            this.RelayAlgorithms = new HashSet<RelayAlgorithmShortModel>();
        }
    }
}
