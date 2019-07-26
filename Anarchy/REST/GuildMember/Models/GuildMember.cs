using Newtonsoft.Json;

namespace Discord
{
    public class GuildMember : PartialGuildMember
    {
        public GuildMember()
        {
            OnClientUpdated += (sender, e) => User.SetClient(Client);
        }


        [JsonProperty("guild_id")]
        public ulong GuildId { get; internal set; }


        [JsonProperty("user")]
        public User User { get; internal set; }


        [JsonProperty("mute")]
        public bool Muted { get; private set; }

        
        [JsonProperty("deaf")]
        public bool Deafened { get; private set; }


        public void Modify(GuildMemberProperties properties)
        {
            Client.ModifyGuildMember(GuildId, User.Id, properties);
        }


        /// <summary>
        /// Changes the member's nickname for this guild
        /// </summary>
        /// <param name="nickname">New nickname</param>
        public void ChangeNickname(string nickname)
        {
            Client.ChangeNickname(GuildId, User.Id, nickname);
        }


        /// <summary>
        /// Changes a user's voice state in the specified guild
        /// </summary>
        /// <param name="muted">Whether the member should be muted or not (null for 'don't change')</param>
        /// <param name="deafened">Whether the member should be deafened or not (null for 'don't change')</param>
        public void ChangeVoiceState(bool? muted = null, bool? deafened = null)
        {
            Client.ChangeGuildMemberVoiceState(GuildId, User.Id, muted, deafened);
        }


        /// <summary>
        /// Kicks the member from the guild
        /// </summary>
        public void Kick()
        {
            Client.KickGuildMember(GuildId, User.Id);
        }


        /// <summary>
        /// Bans the member from the guild
        /// </summary>
        /// <param name="reason">The reason for banning the user</param>
        /// <param name="deleteMessageDays">Amount of days to purge messages</param>
        public void Ban(string reason = null, uint deleteMessageDays = 0)
        {
            Client.BanGuildMember(GuildId, User.Id, reason, deleteMessageDays);
        }


        /// <summary>
        /// Unbans the user from the guild
        /// </summary>
        public void Unban()
        {
            Client.UnbanGuildMember(GuildId, User.Id);
        }


        public override string ToString()
        {
            return User.ToString();
        }
    }
}