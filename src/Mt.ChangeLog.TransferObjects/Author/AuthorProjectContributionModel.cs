namespace Mt.ChangeLog.TransferObjects.Author
{
    public class AuthorProjectContributionModel : AuthorContributionModel
    {
        public string ProjectPrefix { get; set; }
        public string ProjectTitle { get; set; }
        public string ProjectVersion { get; set; }
    }
}
