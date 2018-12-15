namespace Chess.Core.Model
{
    public class NullPlayer : Player
    {
        public NullPlayer(int id) : base(id, string.Empty, " - ", 0)
        {
        }
    }
}