using System;
using System.Collections.Generic;

namespace Chess.Core.Model
{
    public class Tournoi
    {
        public readonly List<Player> Players = new List<Player>();

        public List<Ronde> Rondes { get; set; } = new List<Ronde>();

        public int MaxDisplayLenght;
        public int CurrentRondeNumber { get; private set; }

        public Tournoi()
        {
            CurrentRondeNumber = 1;
        }

        public void AddPlayer(string prenom, string nom, int age)
        {
            var player = new Player(Players.Count + 1, prenom, nom, age);
            Players.Add(player);

            MaxDisplayLenght = Math.Max(MaxDisplayLenght, player.DisplayLength);
        }

        public void SetResultForCurrentRonde()
        {
            var currentRonde = CurrentRondeNumber;

            foreach (var game in Rondes[currentRonde - 1].Games)
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
                        return GameResult.Null;
                    default:
                        Console.WriteLine(possibleKeyPressed);
                        break;
                }
            }
            return GameResult.None;
        }

        public void NextRonde()
        {
            if (CurrentRondeNumber >= Rondes.Count)
            {
                EndOfTournement();
            }
            else
            {
                CurrentRondeNumber++;
            }
        }

        private void EndOfTournement()
        {
            Console.WriteLine(" Le tournoi est terminé.");
        }
    }
}