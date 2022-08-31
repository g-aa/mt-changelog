using System;

namespace Mt.ChangeLog.TransferObjects.Protocol
{
    public class ProtocolShortModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        
        public override string ToString()
        {
            return this.Title;
        }
    }
}
