using System;
using System.Collections.Generic;
using Chess.Core.Command;
using Tools;

namespace Chess.Core.Model
{
    public class Tournament : ITournament
    {
        public List<Player> Players { get; } = new List<Player>();

        public List<Round> Rounds { get; set; } = new List<Round>();

        public int ContestantNumber()
        {
            return Players.Count;
        }

        public int MaxDisplayLenght;
        public int CurrentRoundNumber { get; private set; }

        public Tournament()
        {
            CurrentRoundNumber = 1;
        }

        public void AddPlayer(string prenom, string nom, int age)
        {
            AddPlayer(new Player(Players.Count, prenom, nom, age));
        }

        internal void AddPlayer(Player player)
        {
            Players.Add(player);
            MaxDisplayLenght = Math.Max(MaxDisplayLenght, player.DisplayLength);
        }

        public void SetResultForCurrentRound(Action<ICommandGameResult> setGameResultForGame)
        {
            var currentRound = CurrentRoundNumber;

            foreach (var game in Rounds[currentRound - 1].Games)
            {
                if (game.IsPlayedGame)
                    setGameResultForGame(game);
                else
                    ResultForNonPlayedGame(game);
            }
        }

        private void ResultForNonPlayedGame(Game game)
        {
            game.Display();

            if (game.BlackContestant is NullPlayer)
            {
                game.SetGameResultCommand(GameResult.WinnerWhite);
                Logger.WriteLine("Blanc vainqueur par abandon");
            }

            if (game.WhiteContestant is NullPlayer)
            {
                game.SetGameResultCommand(GameResult.WinnerBlack);
                Logger.WriteLine("Noir vainqueur par abandon");
            }
        }
        
        public void NextRound()
        {
            if (CurrentRoundNumber >= Rounds.Count)
            {
                EndOfTournament();
            }
            else
            {
                CurrentRoundNumber++;
            }
        }

        private void EndOfTournament()
        {
            Logger.WriteLine(" Le tournoi est termin�.");
        }
    }
}