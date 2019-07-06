namespace Discord
{
    public class DiscordHttpException : DiscordException
    {
        public DiscordHttpError Error { get; private set; }

        public DiscordHttpException(DiscordClient client, string errorJson) : base(client)
        {
            Error = errorJson.Deserialize<DiscordHttpError>();
        }
    }
}
