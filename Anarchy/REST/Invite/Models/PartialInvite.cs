namespace Discord
{
    public class PartialInvite : BaseInvite
    {
        /// <summary>
        /// Gets the full invite (<see cref="Invite"/>)
        /// </summary>
        public Invite GetFullInvite()
        {
            return Client.GetInvite(Code);
        }
    }
}
