using System;

namespace Mt.ChangeLog.TransferObjects.Author
{
    public class AuthorModel : AuthorTableModel
    {
        public AuthorModel()
        {
            this.Id = Guid.NewGuid();
        }
        
        public override string ToString()
        {
            return $"{base.ToString()}, {this.Position}";
        }
    }
}
