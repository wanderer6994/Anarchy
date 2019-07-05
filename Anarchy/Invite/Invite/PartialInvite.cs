namespace Discord
{
    public class PartialInvite : BaseInvite
    {
        public Invite GetFullInvite()
        {
            return Client.GetInvite(Code);
        }
    }
}
