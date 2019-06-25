using System;

namespace Discord
{
    public class SPUpdatedEventArgs : EventArgs
    {
        public SuperPropertiesInfo Properties { get; private set; }

        public SPUpdatedEventArgs(SuperPropertiesInfo properties)
        {
            Properties = properties;
        }
    }
}
