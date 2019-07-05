namespace Discord
{
    public class DiscordHttpErrorException : DiscordException
    {
        public DiscordHttpError Error { get; private set; }

        public DiscordHttpErrorException(DiscordClient client, string errorJson) : base(client)
        {
            Error = errorJson.Deserialize<DiscordHttpError>();
        }
    }
}
