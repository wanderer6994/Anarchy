namespace Discord.Gateway
{
    public class BanUpdateEventArgs
    {
        public long GuildId { get; private set; }
        public User User { get; private set; }

        internal BanUpdateEventArgs(BanContainer ban)
        {
            GuildId = ban.GuildId;
            User = ban.User;
        }


        public override string ToString()
        {
            return User.ToString();
        }
    }
}
