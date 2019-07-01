using System.Collections.Generic;
using Newtonsoft.Json;

namespace Discord
{
    public class AuditLog
    {
        [JsonProperty("audit_log_entries")]
        public List<AuditLogEntry> Entries { get; private set; }
    }
}
