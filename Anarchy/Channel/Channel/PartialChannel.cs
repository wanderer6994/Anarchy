namespace Discord
{
    public class PartialChannel : BaseChannel
    {
        public Channel GetFullChannel()
        {
            return Client.GetChannel(Id);
        }
    }
}
