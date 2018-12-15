using System;
using Chess.Core.Command;
using Tools;

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
            return $"(Blanc) {WhiteContestant?.FirstName + WhiteContestant?.LastName} versus (Noir) {BlackContestant?.FirstName + BlackContestant?.LastName}";
        }

        public void SetGameResultCommand(GameResult gameResult)
        {
            GameResult = gameResult;
            SetPointsFromResult(gameResult);
        }

        public void Display()
        {
            Logger.WriteLine(ToString());
        }

        private void SetPointsFromResult(GameResult gameResult)
        {
            GameResultHelper.GetResult(gameResult, out var whiteContestantPoints, out var blackContestantPoints);
            
            BlackContestant.Points += blackContestantPoints;
            WhiteContestant.Points += whiteContestantPoints;
        }

        public Game Clone()
        {
            var gameClone =  new Game(WhiteContestant.Clone(), BlackContestant.Clone());
            gameClone.SetGameResultCommand(GameResult);
            return gameClone;
        }
    }
}