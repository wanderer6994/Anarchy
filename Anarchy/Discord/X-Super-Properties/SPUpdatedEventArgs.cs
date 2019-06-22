using System;

namespace Discord
{
    public class SPUpdatedEventArgs : EventArgs
    {
        public SuperPropertiesInformation PropertiesInfo { get; private set; }

        public SPUpdatedEventArgs(SuperPropertiesInformation properties)
        {
            PropertiesInfo = properties;
        }
    }
}
