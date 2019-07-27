using Newtonsoft.Json;

namespace Discord
{
    /// <summary>
    /// Represents a universal channel object. This is not specific to any sort of channel
    /// </summary>
    public class Channel : Controllable
    {
        [JsonProperty("id")]
        public ulong Id { get; private set; }


        [JsonProperty("name")]
        public string Name { get; protected set; }


        [JsonProperty("type")]
        public ChannelType Type { get; private set; }


        /// <summary>
        /// Updates the channel's info
        /// </summary>
        public virtual void Update()
        {
            Name = Client.GetChannel(Id).Name;
        }


        /// <summary>
        /// Modifies the channel
        /// </summary>
        /// <param name="properties">Options for modifying the channel</param>
        public void Modify(ChannelProperties properties)
        {
            if (!properties.NameProperty.Set)
                return;

            Name = Client.ModifyChannel(Id, properties).Name;
        }


        /// <summary>
        /// Deletes the channel
        /// </summary>
        /// <returns>The deleted <see cref="Channel"/></returns>
        public Channel Delete()
        {
            return Client.DeleteChannel(Id);
        }


        public static implicit operator ChannelCreationProperties(Channel instance)
        {
            var properties = new ChannelCreationProperties();
            properties.Name = instance.Name;
            properties.Type = instance.Type;
            return properties;
        }


        public override string ToString()
        {
            return Name;
        }
    }
}