using Newtonsoft.Json;
using System;

namespace Discord.Gateway
{
    internal class ActivityTimestamps
    {
        [JsonProperty("start")]
#pragma warning disable IDE0052
        private long? _startValue;
#pragma warning restore IDE0052
        private TimeSpan? _startSpan;
        [JsonIgnore]
        public uint? Start
        {
            get { return _startSpan.HasValue ? (uint?)_startSpan.Value.Hours : null; }
            set
            {
                if (value == null)
                {
                    _startValue = null;
                    _startSpan = null;
                }
                else
                {
                    _startSpan = (TimeSpan?)new TimeSpan((int)value, 0, 0);
                    _startValue = new DateTimeOffset(DateTime.UtcNow, _startSpan.Value)
                                                  .ToUnixTimeMilliseconds();
                }
                
            }
        }
    }
}
