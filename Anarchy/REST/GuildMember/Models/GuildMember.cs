using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Discord
{
    public class GuildMember : Controllable
    {
        public GuildMember()
        {
            OnClientUpdated += (sender, e) => User.SetClient(Client);
        }


        [JsonProperty("guild_id")]
        public ulong GuildId { get; internal set; }


        [JsonProperty("user")]
        public User User { get; internal set; }


        [JsonProperty("nick")]
        public string Nickname { get; internal set; }


        [JsonProperty("roles")]
        public IReadOnlyList<ulong> Roles { get; internal set; }


        [JsonProperty("joined_at")]
#pragma warning disable CS0649
        private string _joinedAt;
        public DateTime? JoinedAt
        {
            get
            {
                if (_joinedAt == null)
                    return null;

                return DiscordTimestamp.FromString(_joinedAt);
            }
            internal set
            {
                if (value == null)
                    _joinedAt = null;
                else
                    _joinedAt = DiscordTimestamp.ToString(value.Value);
            }
        }
#pragma warning restore CS0649


        [JsonProperty("mute")]
        public bool Muted { get; private set; }

        
        [JsonProperty("deaf")]
        public bool Deafened { get; private set; }


        /// <summary>
        /// Updates the member's information
        /// </summary>
        public void Update()
        {
            GuildMember member = Client.GetGuildMember(GuildId, User.Id);
            User = member.User;
            Nickname = member.Nickname;
            Roles = member.Roles;
            Muted = member.Muted;
            Deafened = member.Deafened;
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
        /// Sets the member's roles
        /// </summary>
        public void SetRoles(List<ulong> roles)
        {
            Client.SetGuildMemberRoles(GuildId, User.Id, roles);
        }


        /// <summary>
        /// Sets the member's roles
        /// </summary>
        public void SetRoles(List<Role> roles)
        {
            List<ulong> ids = new List<ulong>();
            foreach (var role in roles)
                ids.Add(role.Id);

            SetRoles(ids);
        }


        /// <summary>
        /// Adds a role to the guild member
        /// </summary>
        /// <param name="roleId">ID of the role</param>
        public void AddRole(ulong roleId)
        {
            Client.AddRoleToUser(GuildId, roleId, User.Id);
        }


        /// <summary>
        /// Adds a role to the guild member
        /// </summary>
        public void AddRole(Role role)
        {
            AddRole(role.Id);
        }


        /// <summary>
        /// Removes a role from the guild member
        /// </summary>
        /// <param name="roleId">ID of the role</param>
        public void RemoveRole(ulong roleId)
        {
            Client.RemoveRoleFromUser(GuildId, roleId, User.Id);
        }


        /// <summary>
        /// Removes a role from the guild member
        /// </summary>
        public void RemoveRole(Role role)
        {
            RemoveRole(role.Id);
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
        /// <param name="deleteMessageDays">Amount of days to purge messages (max is 7)</param>
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


        public static implicit operator ulong(GuildMember instance)
        {
            return instance.User.Id;
        }
    }
}