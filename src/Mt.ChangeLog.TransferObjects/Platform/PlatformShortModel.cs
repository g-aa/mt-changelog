using System;

namespace Mt.ChangeLog.TransferObjects.Platform
{
    public class PlatformShortModel
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public override string ToString()
        {
            return this.Title;
        }
    }
}
