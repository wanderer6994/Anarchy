using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Discord
{
    public class VoiceChannel : GuildChannel
    {
        [JsonProperty("bitrate")]
        public uint Bitrate { get; private set; }


        [JsonProperty("user_limit")]
        public uint UserLimit { get; private set; }


        public override void Update()
        {
            VoiceChannel channel = Client.GetVoiceChannel(Id);
            Name = channel.Name;
            Bitrate = channel.Bitrate;
            UserLimit = channel.UserLimit;
            Position = channel.Position;
            ParentId = channel.ParentId;
            PermissionOverwrites = channel.PermissionOverwrites;
        }


        public void Modify(VoiceChannelProperties properties)
        {
            if (!properties.NameProperty.Set)
                properties.Name = Name;
            if (!properties.PositionProperty.Set)
                properties.Position = Position;
            if (!properties.ParentProperty.Set)
                properties.ParentId = ParentId;
            if (!properties.BitrateProperty.Set)
                properties.Bitrate = Bitrate;
            if (!properties.UserLimitProperty.Set)
                properties.UserLimit = UserLimit;

            VoiceChannel channel = Client.ModifyVoiceChannel(Id, properties);
            Name = channel.Name;
            Position = channel.Position;
            ParentId = channel.ParentId;
            PermissionOverwrites = channel.PermissionOverwrites;
            Bitrate = channel.Bitrate;
            UserLimit = channel.UserLimit;
        }

        
        //this is ONLY a temporary solution
        #region messages
        [Obsolete("This type of channel does not support messages", true)]
        public new void TriggerTyping()
        {
        }


        [Obsolete("This type of channel does not support messages", true)]
        public new Message SendMessage(MessageProperties properties)
        { return null; }


        [Obsolete("This type of channel does not support messages", true)]
        public new Message SendMessage(string message, bool tts = false)
        { return null; }


        [Obsolete("This type of channel does not support messages", true)]
        public new IReadOnlyList<Message> GetMessages(uint limit = 100, uint afterId = 0)
        { return null; }


        [Obsolete("This type of channel does not support messages", true)]
        public new IReadOnlyList<Message> GetPinnedMessages()
        { return null; }


        [Obsolete("This type of channel does not support messages", true)]
        public new void PinMessage(ulong messageId)
        { }


        [Obsolete("This type of channel does not support messages", true)]
        public new void PinMessage(Message message)
        { }


        [Obsolete("This type of channel does not support messages", true)]
        public new void UnpinMessage(ulong messageId)
        { }


        [Obsolete("This type of channel does not support messages", true)]
        public new void UnpinMessage(Message message)
        { }
        #endregion
    }
}