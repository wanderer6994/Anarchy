using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discord
{
    public static class ChannelExtensions
    {
        /// <summary>
        /// Gets a channel
        /// </summary>
        /// <param name="channelId">ID of the channel</param>
        public static Channel GetChannel(this DiscordClient client, ulong channelId)
        {
            return client.HttpClient.Get($"/channels/{channelId}")
                                .Deserialize<Channel>().SetClient(client);
        }


        /// <summary>
        /// Modifies a channel
        /// </summary>
        /// <param name="channelId">ID of the channel</param>
        /// <param name="properties">Options for modifying the channel</param>
        /// <returns>The modified <see cref="Channel"/></returns>
        public static Channel ModifyChannel(this DiscordClient client, ulong channelId, ChannelProperties properties)
        {
            return client.HttpClient.Patch($"/channels/{channelId}",
                    JsonConvert.SerializeObject(properties)).Deserialize<Channel>().SetClient(client);
        }


        /// <summary>
        /// Deletes a channel
        /// </summary>
        /// <param name="channelId">ID of the channel</param>
        /// <returns>The deleted channel</returns>
        public static Channel DeleteChannel(this DiscordClient client, ulong channelId)
        {
            return client.HttpClient.Delete($"/channels/{channelId}")
                                .Deserialize<Channel>().SetClient(client);
        }
    }
}
