using System;

namespace Discord
{
    public class MessageDeletedEventArgs : EventArgs
    {
        public DeletedMessage DeletedMessage { get; private set; }

        public MessageDeletedEventArgs(DeletedMessage msg)
        {
            DeletedMessage = msg;
        }
    }
}
