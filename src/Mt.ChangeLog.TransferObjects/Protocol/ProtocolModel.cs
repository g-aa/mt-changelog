using Mt.ChangeLog.TransferObjects.Communication;
using System;
using System.Collections.Generic;

namespace Mt.ChangeLog.TransferObjects.Protocol
{
    public class ProtocolModel : ProtocolShortModel
    {
        public string Description { get; set; }
        public IEnumerable<CommunicationShortModel> Communications { get; set; }

        public ProtocolModel() 
        {
            this.Id = Guid.NewGuid();
            this.Communications = new HashSet<CommunicationShortModel>();
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
