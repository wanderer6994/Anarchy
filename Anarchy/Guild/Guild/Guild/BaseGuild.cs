using Discord.Webhook;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Drawing;
using System.Net;
using System.Net.Http;

namespace Discord
{
    public class BaseGuild : Controllable
    {
        [JsonProperty("id")]
        public long Id { get; private set; }

        [JsonProperty("name")]
        public string Name { get; protected set; }

        [JsonProperty("icon")]
        public string IconId { get; protected set; }


        public bool Delete()
        {
            return Client.DeleteGuild(Id);
        }


        public bool KickMember(long userId)
        {
            return Client.KickGuildMember(Id, userId);
        }


        public bool KickMember(User user)
        {
            return KickMember(user.Id);
        }


        public List<Ban> GetBans(long guildId)
        {
            return Client.GetGuildBans(guildId);
        }


        public Ban GetBan(long guildId, long userId)
        {
            return Client.GetGuildBan(guildId, userId);
        }


        public bool BanMember(long userId, int deleteMessageDays, string reason)
        {
            return Client.BanGuildMember(Id, userId, deleteMessageDays, reason);
        }


        public bool BanMember(User user, int deleteMessageDays, string reason)
        {
            return BanMember(user.Id, deleteMessageDays, reason);
        }


        public bool UnbanMember(long userId)
        {
            return Client.UnbanGuildMember(Id, userId);
        }


        public bool UnbanMember(User user)
        {
            return UnbanMember(user.Id);
        }


        public bool ChangeNickname(string nickname)
        {
            return Client.ChangeNickname(Id, nickname);
        }


        public IReadOnlyList<AuditLogEntry> GetAuditLog(AuditLogFilters filters = null)
        {
            return Client.GetGuildAuditLog(Id, filters);
        }


        public IReadOnlyList<Channel> GetChannels()
        {
            return Client.GetGuildChannels(Id);
        }


        public virtual IReadOnlyList<Reaction> GetReactions()
        {
            return Client.GetGuildReactions(Id);
        }


        public Reaction GetReaction(long reactionId)
        {
            return Client.GetGuildReaction(Id, reactionId);
        }


        public virtual IReadOnlyList<Role> GetRoles()
        {
            return Client.GetGuildRoles(Id);
        }


        public IReadOnlyList<Invite> GetInvites()
        {
            return Client.GetGuildInvites(Id);
        }


        public Channel CreateChannel(ChannelCreationProperties properties)
        {
            return Client.CreateGuildChannel(Id, properties);
        }


        public Reaction CreateReaction(ReactionCreationProperties properties)
        {
            return Client.CreateGuildReaction(Id, properties);
        }


        public Role CreateRole(RoleProperties properties = null)
        {
            return Client.CreateGuildRole(Id, properties);
        }


        public IReadOnlyList<Hook> GetWebhooks()
        {
            return Client.GetGuildWebhooks(Id);
        }


        public Image GetIcon()
        {
            var resp = new HttpClient().GetAsync($"https://cdn.discordapp.com/icons/{Id}/{IconId}.png").Result;

            if (resp.StatusCode == HttpStatusCode.NotFound)
                throw new ImageNotFoundException(IconId);

            return (Bitmap)new ImageConverter().ConvertFrom(resp.Content.ReadAsByteArrayAsync().Result);
        }


        public override string ToString()
        {
            return $"{Name} ({Id})";
        }
    }
}
