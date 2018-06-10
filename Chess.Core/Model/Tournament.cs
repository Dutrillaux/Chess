using System;
using System.Collections.Generic;

namespace Chess.Core.Model
{
    public class Tournament : ISetupTournament
    {
        public List<Player> Players { get; }= new List<Player>();

        public List<Round> Rounds { get; set; } = new List<Round>();

        public int MaxDisplayLenght;
        public int CurrentRoundNumber { get; private set; }

        public Tournament()
        {
            CurrentRoundNumber = 1;
        }

        public void AddPlayer(string prenom, string nom, int age)
        {
            var player = new Player(Players.Count + 1, prenom, nom, age);
            Players.Add(player);

            MaxDisplayLenght = Math.Max(MaxDisplayLenght, player.DisplayLength);
        }

        public void SetResultForCurrentRound()
        {
            var currentRound = CurrentRoundNumber;

            foreach (var game in Rounds[currentRound - 1].Games)
            {
                game.GameResult = GetResultForGame(game);
            }
        }

        private GameResult GetResultForGame(Game game)
        {
            return game.IsPlayedGame
                ? ResultForPlayedGame(game)
                : ResultForNonPlayedGame(game);
        }

        private GameResult ResultForNonPlayedGame(Game game)
        {
            if (game.BlackContestant is NullPlayer)
            {
                return GameResult.WinnerWhite;
            }

            if (game.WhiteContestant is NullPlayer)
            {
                return GameResult.WinnerBlack;
            }

            return GameResult.None;
        }

        private GameResult ResultForPlayedGame(Game game)
        {
            Console.WriteLine(
                $" Saisie du résultat de la partie : (Blanc) {game.WhiteContestant.Prenom + game.WhiteContestant.Nom} versus (Noir) {game.BlackContestant.Prenom + game.BlackContestant.Nom}");

            const string possibleKeyPressed = "Resultat ? B (Blanc vainqueur), N (Noir vainqueur), P (Pat ou nul)";
            Console.WriteLine(possibleKeyPressed);

            var keyPressed = new ConsoleKeyInfo();
            while (keyPressed.Key != ConsoleKey.Q)
            {
                keyPressed = Console.ReadKey();
                switch (keyPressed.Key)
                {
                    case ConsoleKey.B:
                        return GameResult.WinnerWhite;
                    case ConsoleKey.N:
                        return GameResult.WinnerBlack;
                    case ConsoleKey.P:
                        return GameResult.NoWinnerPat;
                    default:
                        Console.WriteLine(possibleKeyPressed);
                        break;
                }
            }
            return GameResult.None;
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
            Console.WriteLine(" Le tournoi est terminé.");
        }
    }
}