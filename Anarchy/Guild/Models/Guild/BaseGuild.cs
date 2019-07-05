using Discord.Webhook;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Drawing;
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


        public void Delete()
        {
            Client.DeleteGuild(Id);
        }


        public void ChangeNickname(string nickname)
        {
            Client.ChangeNickname(Id, nickname);
        }


        public virtual IReadOnlyList<GuildChannel> GetChannels()
        {
            return Client.GetGuildChannels(Id);
        }


        public GuildChannel CreateChannel(ChannelCreationProperties properties)
        {
            return Client.CreateGuildChannel(Id, properties);
        }


        public TextChannel CreateTextChannel(ChannelCreationProperties properties)
        {
            return Client.CreateTextChannel(Id, properties);
        }


        public VoiceChannel CreateVoiceChannel(ChannelCreationProperties properties)
        {
            return Client.CreateVoiceChannel(Id, properties);
        }


        public virtual IReadOnlyList<Emoji> GetEmojis()
        {
            return Client.GetGuildEmojis(Id);
        }


        public Emoji GetEmoji(long reactionId)
        {
            return Client.GetGuildEmoji(Id, reactionId);
        }


        public Emoji CreateEmoji(EmojiCreationProperties properties)
        {
            return Client.CreateGuildEmoji(Id, properties);
        }


        public virtual IReadOnlyList<Role> GetRoles()
        {
            return Client.GetGuildRoles(Id);
        }


        public Role CreateRole(RoleProperties properties = null)
        {
            return Client.CreateGuildRole(Id, properties);
        }


        public IReadOnlyList<Invite> GetInvites()
        {
            return Client.GetGuildInvites(Id);
        }


        public IReadOnlyList<Hook> GetWebhooks()
        {
            return Client.GetGuildWebhooks(Id);
        }


        public IReadOnlyList<AuditLogEntry> GetAuditLog(AuditLogFilters filters = null)
        {
            return Client.GetGuildAuditLog(Id, filters);
        }


        public IReadOnlyList<Ban> GetBans()
        {
            return Client.GetGuildBans(Id);
        }


        public Ban GetBan(long userId)
        {
            return Client.GetGuildBan(Id, userId);
        }


        public Image GetIcon()
        {
            if (IconId == null)
                return null;

            return (Bitmap)new ImageConverter().ConvertFrom(new HttpClient().GetAsync($"https://cdn.discordapp.com/icons/{Id}/{IconId}.png")
                                                                                .Result.Content.ReadAsByteArrayAsync().Result);
        }


        public override string ToString()
        {
            return Name;
        }
    }
}
