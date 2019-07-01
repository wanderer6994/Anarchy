using System.Collections.Generic;
using Newtonsoft.Json;

namespace Discord
{
    public class AuditLogEntry
    {
        [JsonProperty("id")]
        public long Id { get; private set; }

        [JsonProperty("target_id")]
        public long? TargetId { get; private set; }

        [JsonProperty("changes")]
        public List<AuditLogChange> Changes { get; private set; }

        [JsonProperty("user_id")]
        public long ChangerId { get; private set; }

        [JsonProperty("action_type")]
        public AuditLogActionType ActionType { get; private set; }

        [JsonProperty("reason")]
        public string Reason { get; private set; }


        public override string ToString()
        {
            return ActionType.ToString();
        }
    }
}
