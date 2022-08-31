using Mt.ChangeLog.TransferObjects.AnalogModule;
using System;
using System.Collections.Generic;

namespace Mt.ChangeLog.TransferObjects.Platform
{
    public class PlatformModel : PlatformTableModel
    {
        public IEnumerable<AnalogModuleShortModel> AnalogModules { get; set; }

        public PlatformModel()
        {
            this.Id = Guid.NewGuid();
            this.AnalogModules = new HashSet<AnalogModuleShortModel>();
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
