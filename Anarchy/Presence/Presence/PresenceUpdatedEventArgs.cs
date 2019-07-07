namespace Discord.Gateway
{
    public class PresenceUpdatedEventArgs
    {
        public PresenceUpdate Presence { get; private set; }

        public PresenceUpdatedEventArgs(PresenceUpdate presence)
        {
            Presence = presence;
        }


        public override string ToString()
        {
            return Presence.ToString();
        }
    }
}
