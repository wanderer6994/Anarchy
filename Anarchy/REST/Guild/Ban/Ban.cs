﻿using Newtonsoft.Json;

namespace Discord
{
    public class Ban : Controllable
    {
        public Ban()
        {
            OnClientUpdated += (sender, e) => User.SetClient(Client);
        }


        [JsonProperty("reason")]
        public string Reason { get; private set; }


        [JsonProperty("user")]
        public User User { get; private set; }


        internal ulong GuildId { get; set; }

        public MinimalGuild Guild
        {
            get
            {
                return new MinimalGuild(GuildId);
            }
        }


        /// <summary>
        /// Unbans the user
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
