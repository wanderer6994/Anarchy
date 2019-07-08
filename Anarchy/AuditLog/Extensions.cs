using System.Collections.Generic;

namespace Discord
{
    public static class AuditLogExtensions
    {
        public static IReadOnlyList<AuditLogEntry> GetGuildAuditLog(this DiscordClient client, ulong guildId, AuditLogFilters filters = null)
        {
            if (filters == null)
                filters = new AuditLogFilters();

            return client.HttpClient.Get($"/guilds/{guildId}/audit-logs?{(filters.UserIdProperty.Set ? $"user_id={filters.UserId}" : "")}&{(filters.ActionTypeProperty.Set ? $"action_type={(int)filters.ActionType}" : "")}&{(filters.BeforeIdProperty.Set ? $"before={filters.BeforeId}" : "")}&{(filters.LimitProperty.Set ? $"limit={filters.Limit}" : "")}")
                    .Deserialize<AuditLog>().Entries;
        }
    }
}