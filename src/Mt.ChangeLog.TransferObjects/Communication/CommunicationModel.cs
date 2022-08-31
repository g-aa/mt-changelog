using Mt.ChangeLog.TransferObjects.Protocol;
using System;
using System.Collections.Generic;

namespace Mt.ChangeLog.TransferObjects.Communication
{
    public class CommunicationModel : CommunicationShortModel
    {
        public string Description { get; set; }
        public IEnumerable<ProtocolShortModel> Protocols { get; set; }

        public CommunicationModel() 
        {
            this.Id = Guid.NewGuid();
            this.Protocols = new HashSet<ProtocolShortModel>();
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
