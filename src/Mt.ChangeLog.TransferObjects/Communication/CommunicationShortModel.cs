using System;

namespace Mt.ChangeLog.TransferObjects.Communication
{
    public class CommunicationShortModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }

        public override string ToString()
        {
            return this.Title;
        }
    }
}
