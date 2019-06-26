using System.Collections.Generic;
using System.Linq;

namespace Discord
{
    public struct OrganizedChannelList
    {
        public OrganizedChannelList(List<Channel> channels)
        {
            Categories = channels.Where(ch => ch.Type == ChannelType.Category).ToList();
            TextChannels = channels.Where(ch => ch.Type == ChannelType.Text).ToList();
            VoiceChannels = channels.Where(ch => ch.Type == ChannelType.Voice).ToList();
        }

        public List<Channel> Categories { get; private set; }
        public List<Channel> TextChannels { get; private set; }
        public List<Channel> VoiceChannels { get; private set; }
    }
}