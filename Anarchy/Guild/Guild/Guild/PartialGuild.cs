﻿using Newtonsoft.Json;

namespace Discord
{
    public class PartialGuild : BaseGuild
    {
        [JsonProperty("owner")]
        public bool Owner { get; private set; }

        [JsonProperty("permissions")]
        public int Permissions { get; private set; }

        [JsonProperty("icon")]
        public string IconId { get; protected set; }


        public Guild GetGuild()
        {
            return Client.GetGuild(Id);
        }
    }
}