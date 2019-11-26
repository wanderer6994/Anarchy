using Newtonsoft.Json;

namespace Discord
{
    /// <summary>
    /// Represents a universal channel object. This is not specific to any sort of channel
    /// </summary>
    public class Channel : ControllableEx
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
            Channel channel = Client.GetChannel(Id);
            Json = channel.Json;
            Name = channel.Name;
        }


        /// <summary>
        /// Modifies the channel
        /// </summary>
        /// <param name="properties">Options for modifying the channel</param>
        public void Modify(string name)
        {
            Channel channel = Client.ModifyChannel(Id, name);
            Json = channel.Json;
            Name = channel.Name;
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


        // Note: the reason the Json == null checks are here is because Anarchy currently does not have a method for getting the children of a JObject which is a list

        
        public GuildChannel ToGuildChannel()
        {
            if (Json == null)
                return Client.GetGuildChannel(Id);
            else
                return ((GuildChannel)Json.ToObject(typeof(GuildChannel))).SetClient(Client).SetJson(Json);
        }


        public TextChannel ToTextChannel()
        {
            if (Json == null)
                return Client.GetTextChannel(Id);
            else
                return ((TextChannel)Json.ToObject(typeof(TextChannel))).SetClient(Client).SetJson(Json);
        }


        public VoiceChannel ToVoiceChannel()
        {
            if (Json == null)
                return Client.GetVoiceChannel(Id);
            else
                return ((VoiceChannel)Json.ToObject(typeof(VoiceChannel))).SetClient(Client).SetJson(Json);
        }


        public DMChannel ToDMChannel()
        {
            if (Json == null)
                return Client.GetDMChannel(Id);
            else
                return ((DMChannel)Json.ToObject(typeof(DMChannel))).SetClient(Client).SetJson(Json);
        }


        public Group ToGroup()
        {
            if (Json == null)
                return Client.GetGroup(Id);
            else
                return ((Group)Json.ToObject(typeof(Group))).SetClient(Client).SetJson(Json);
        }
    }
}