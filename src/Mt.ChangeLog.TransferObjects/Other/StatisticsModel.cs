using Mt.ChangeLog.TransferObjects.Author;
using Mt.ChangeLog.TransferObjects.Historical;
using System;
using System.Collections.Generic;

namespace Mt.ChangeLog.TransferObjects.Other
{
    public class StatisticsModel
    {
        public DateTime Date { get; set; }
        public string ArmEdit { get; set; }
        public int ProjectCount { get; set; }
        public Dictionary<string, int> ProjectDistributions { get; set; }
        public IEnumerable<AuthorContributionModel> AuthorContributions { get; set; }
        public IEnumerable<ProjectRevisionHistoryShortModel> LastModifiedProjects { get; set; }
    }
}
