namespace Discord
{
    public class ReactionEventArgs
    {
        public MessageReactionUpdate Reaction { get; private set; }

        public ReactionEventArgs(MessageReactionUpdate reaction)
        {
            Reaction = reaction;
        }


        public override string ToString()
        {
            return Reaction.ToString();
        }
    }
}
