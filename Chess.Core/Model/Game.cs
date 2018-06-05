namespace Chess.Core.Model
{
    public class Game
    {
        public Game(Player whiteContestant, Player blackContestant)
        {
            BlackContestant = blackContestant;
            WhiteContestant = whiteContestant;
        }
        public Player BlackContestant;
        public Player WhiteContestant;
        public GameResult GameResult = GameResult.None;

        public override string ToString()
        {
            return WhiteContestant.Id + " vs " + BlackContestant.Id;
        }
    }
}