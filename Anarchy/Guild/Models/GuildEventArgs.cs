namespace Discord
{
    public class GuildEventArgs
    {
        public Guild Guild { get; private set; }

        public GuildEventArgs(Guild guild)
        {
            Guild = guild;
        }


        public override string ToString()
        {
            return Guild.ToString();
        }
    }
}
