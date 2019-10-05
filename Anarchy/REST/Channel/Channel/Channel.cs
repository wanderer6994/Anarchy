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
        public void Modify(string name)
        {
            Name = Client.ModifyChannel(Id, name).Name;
        }


        /// <summary>
        /// Deletes the channel
        /// </summary>
        /// <returns>The deleted <see cref="Channel"/></returns>
        public void Delete()
        {
            Name = Client.DeleteChannel(Id).Name;
        }


        public override string ToString()
        {
            return Name;
        }


        public static implicit operator ulong(Channel instance)
        {
            return instance.Id;
        }
    }
}