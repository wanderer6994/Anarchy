using System;
using System.Text;
using Newtonsoft.Json;

namespace Discord
{
    public class SuperPropertiesInfo
    {
        internal delegate void UpdatedHandler(object sender, SPUpdatedEventArgs args);
        internal event UpdatedHandler OnPropertiesUpdated;

        public string Base64
        {
            get { return Properties != null ? Convert.ToBase64String(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(Properties))) : null; }
            set { Properties = value != null ? JsonConvert.DeserializeObject<SuperProperties>(Encoding.UTF8.GetString(Convert.FromBase64String(value))) : null; }
        }

        private SuperProperties _properties;
        public SuperProperties Properties
        {
            get { return _properties; }
            private set
            {
                _properties = value;
                OnPropertiesUpdated?.Invoke(this, new SPUpdatedEventArgs(this));
            }
        }
    }
}