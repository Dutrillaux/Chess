using System;
using Chess.Core.Command;

namespace Chess.Core.Model
{
    public class Game : ICommandGameResult
    {
        public readonly Player BlackContestant;
        public readonly Player WhiteContestant;

        public bool IsPlayedGame => !(BlackContestant is NullPlayer) && !(WhiteContestant is NullPlayer);

        public GameResult GameResult { get; private set; }

        public Game(Player whiteContestant, Player blackContestant)
        {
            GameResult = GameResult.None;
            BlackContestant = blackContestant;
            WhiteContestant = whiteContestant;
        }

        public override string ToString()
        {
            return $"(Blanc) {WhiteContestant?.Prenom + WhiteContestant?.Nom} versus (Noir) {BlackContestant?.Prenom + BlackContestant?.Nom}";
        }

        public void SetGameResultCommand(GameResult gameResult)
        {
            GameResult = gameResult;
        }

        public void Display()
        {
            Console.WriteLine(this);
        }
    }
}