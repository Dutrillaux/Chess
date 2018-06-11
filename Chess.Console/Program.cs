using System;
using Chess.Core;
using Chess.Core.Command;
using Chess.Core.Model;
//using ConsoleKey = System.ConsoleKey;

namespace Chess.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var tournamentDeBerger = new TournamentDeBerger();

            tournamentDeBerger.AddPlayer("John", "Doe", 25);
            tournamentDeBerger.AddPlayer("Johnny", "Good", 25);
            tournamentDeBerger.AddPlayer("Billy", "The Kid", 18);
            tournamentDeBerger.AddPlayer("Robert", "Michoum", 25);
            tournamentDeBerger.AddPlayer("Mike", "Orson", 18);

            System.Console.WriteLine("Nombre de joeur inscrits :" + tournamentDeBerger.Players.Count);
            System.Console.WriteLine("Nomre de Ronde : " + tournamentDeBerger.TotalRoundNumber);

            System.Console.WriteLine();
            System.Console.WriteLine();

            tournamentDeBerger.StartTournement();

            var keyPressed = new ConsoleKeyInfo();
            while (keyPressed.Key != ConsoleKey.Q)
            {
                System.Console.WriteLine(
                    "V pour saisie des résultats, A pour Afficher toutes les rondes, Q pour quitter");
                System.Console.WriteLine("S pour ronde suivante");
                keyPressed = System.Console.ReadKey();
                System.Console.WriteLine();

                switch (keyPressed.Key)
                {
                    case ConsoleKey.V:
                        tournamentDeBerger.SetResultForCurrentRound(SetGameResultForGame);
                        break;
                    case ConsoleKey.A:
                        tournamentDeBerger.DisplayAllRounds();
                        break;
                    case ConsoleKey.R:
                        tournamentDeBerger.NextRound();
                        break;
                    case ConsoleKey.S:
                        new RankingService().CalculateRanking(tournamentDeBerger);
                        break;
                }
            }
        }

        private static void SetGameResultForGame(ICommandGameResult game)
        {
            System.Console.WriteLine(" Saisie du résultat de la partie : " + game);

            const string possibleKeyPressed = "Resultat ? B (Blanc vainqueur), N (Noir vainqueur), P (Pat ou nul), Q (quitter)";
            System.Console.WriteLine(possibleKeyPressed);

            var keyPressed = new ConsoleKeyInfo();
            while (keyPressed.Key != ConsoleKey.Q)
            {
                keyPressed = System.Console.ReadKey();
                switch (keyPressed.Key)
                {
                    case ConsoleKey.B:
                        game.SetGameResultCommand(GameResult.WinnerWhite);
                        return;
                    case ConsoleKey.N:
                        game.SetGameResultCommand(GameResult.WinnerBlack);
                        return;
                    case ConsoleKey.P:
                        game.SetGameResultCommand(GameResult.NoWinnerPat);
                        return;
                    case ConsoleKey.Q:
                        return;
                    default:
                        System.Console.WriteLine(possibleKeyPressed);
                        break;
                }
            }
        }
    }
}
