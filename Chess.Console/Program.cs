using System;
using Chess.Core;
using Chess.Core.Command;
using Chess.Core.Model;
using Tools;

namespace Chess.Console
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var tournamentDeBerger = new TournamentDeBerger();

            tournamentDeBerger.AddPlayer("John", "Doe", 25);
            tournamentDeBerger.AddPlayer("Johnny", "Good", 25);
            tournamentDeBerger.AddPlayer("Billy", "The Kid", 18);
            tournamentDeBerger.AddPlayer("Robert", "Michoum", 25);
            tournamentDeBerger.AddPlayer("Mike", "Orson", 18);

            Logger.WriteLine("Nombre de joueurs" +
                " inscrits :" + tournamentDeBerger.Players.Count);
            Logger.WriteLine("Nombre de Rondes : " + tournamentDeBerger.TotalRoundNumber);

            Logger.WriteLine();
            Logger.WriteLine();

            tournamentDeBerger.StartTournement();

            var keyPressed = new ConsoleKeyInfo();
            while (keyPressed.Key != ConsoleKey.Q)
            {
                Logger.WriteLine("V pour la saisie des résultats");
                Logger.WriteLine("A pour Afficher toutes les rondes");
                Logger.WriteLine("Q pour quitter");
                Logger.WriteLine("S pour la ronde suivante");
                keyPressed = System.Console.ReadKey();
                Logger.WriteLine();

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
                        new RankingService().ComputeRanking(tournamentDeBerger);
                        break;
                }
            }
        }

        private static void SetGameResultForGame(ICommandGameResult game)
        {
            Logger.WriteLine(" Saisie du résultat de la partie : " + game);

            const string possibleKeyPressed = "Resultat ? B (Blanc vainqueur), N (Noir vainqueur), P (Pat ou nul), Q (quitter)";
            Logger.WriteLine(possibleKeyPressed);

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
                        Logger.WriteLine(possibleKeyPressed);
                        break;
                }
            }
        }
    }
}
