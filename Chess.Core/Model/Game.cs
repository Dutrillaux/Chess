namespace Chess.Core.Model
{
    public class Game
    {
        public Player BlackContestant;
        public Player WhiteContestant;
        public GameResult GameResult = GameResult.None;
        public bool IsPlayedGame => !(BlackContestant is NullPlayer) && !(WhiteContestant is NullPlayer);

        public Game(Player whiteContestant, Player blackContestant)
        {
            BlackContestant = blackContestant;
            WhiteContestant = whiteContestant;
        }
        
        public override string ToString()
        {
            return WhiteContestant.Id + " vs " + BlackContestant.Id;
        }
    }
}