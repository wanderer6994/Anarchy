using System.Collections.Generic;
using System.Net;

namespace Discord
{
    public static class AuditLogExtensions
    {
        public static IReadOnlyList<AuditLogEntry> GetGuildAuditLog(this DiscordClient client, long guildId, AuditLogFilters filters = null)
        {
            if (filters == null) filters = new AuditLogFilters();

            var resp = client.HttpClient.Get($"/guilds/{guildId}/audit-logs?{(filters.UserIdProperty.Set ? $"user_id={filters.UserId}" : "")}&{(filters.ActionTypeProperty.Set ? $"action_type={(int)filters.ActionType}" : "")}&{(filters.BeforeIdProperty.Set ? $"before={filters.BeforeId}" : "")}&{(filters.LimitProperty.Set ? $"limit={filters.Limit}" : "")}");

            if (resp.StatusCode == HttpStatusCode.NotFound)
                throw new GuildNotFoundException(client, guildId);

            return resp.Deserialize<AuditLog>().Entries;
        }
    }
}