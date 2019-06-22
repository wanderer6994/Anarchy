using System;

namespace Discord
{
    public class ChannelEventArgs : EventArgs
    {
        public Channel Channel { get; private set; }

        public ChannelEventArgs(Channel channel)
        {
            Channel = channel;
        }
    }
}