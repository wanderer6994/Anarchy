using Newtonsoft.Json;
using System;

namespace Discord.Gateway
{
    internal class ActivityTimestamps
    {
        [JsonProperty("start")]
        private long? _startValue;
        private TimeSpan? _startSpan;
        [JsonIgnore]
        public uint? Start
        {
            get { return _startSpan.HasValue ? (uint?)_startSpan.Value.Hours : null; }
            set
            {
                _startSpan = value.HasValue ? (TimeSpan?)new TimeSpan((int)value, 0, 0) : null;
                DateTime now = DateTime.UtcNow;
                _startValue = value.HasValue ? (long?)new DateTimeOffset(now.Year, 
                                                                         now.Month, 
                                                                         now.Day, 
                                                                         now.Hour, 
                                                                         now.Minute, 
                                                                         now.Second, 
                                                                         _startSpan.Value)
                                                                    .ToUnixTimeMilliseconds() : null;
            }
        }
    }
}
