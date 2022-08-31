using System;

namespace Mt.ChangeLog.TransferObjects.Author
{
    public class AuthorShortModel
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public override string ToString()
        {
            return $"{this.LastName} {this.FirstName}";
        }
    }
}
