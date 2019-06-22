using System;

namespace Discord
{
    public class MessageDeletedEventArgs : EventArgs
    {
        public MessageDelete DeletedMessage { get; private set; }

        public MessageDeletedEventArgs(MessageDelete delete)
        {
            DeletedMessage = delete;
        }
    }
}
