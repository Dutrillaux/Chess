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

        private static GameResult GetResultForGame(Game game)
        {
            Console.WriteLine(
                $" Saisie du résultat de la partie : (Blanc) {game.WhiteContestant.Prenom + game.WhiteContestant.Nom} versus (Noir) {game.BlackContestant.Prenom + game.BlackContestant.Nom}");

            const string possibleKeyPressed = "Resultat ? B (Blanc vainqueur), N (Noir vainqueur), P (Pat ou nul)";
            Console.WriteLine(possibleKeyPressed);

            var keyPressed = new ConsoleKeyInfo();

            while (!(keyPressed.Key == ConsoleKey.B 
                     || keyPressed.Key == ConsoleKey.N 
                     || keyPressed.Key == ConsoleKey.P))
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
            if (CurrentRondeNumber == (Rondes.Count - 1))
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
            throw new NotImplementedException();
        }
    }
}