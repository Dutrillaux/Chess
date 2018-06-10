using System;
using Chess.Core;
using ConsoleKey = System.ConsoleKey;

namespace Chess.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var tournamentDeBerger = new TournamentDeBerger();

            tournamentDeBerger.AddPlayer("Victor", "Dutrillaux", 8);
            tournamentDeBerger.AddPlayer("Arthur", "Dutrillaux", 10);
            tournamentDeBerger.AddPlayer("Adam", "Oualalou", 10);
            tournamentDeBerger.AddPlayer("Raphael", "Perret", 10);
            tournamentDeBerger.AddPlayer("Octave", "Perret", 10);

            System.Console.WriteLine("Nombre de joeur inscrits :" + tournamentDeBerger.Players.Count);
            System.Console.WriteLine("Nomre de Ronde : " + tournamentDeBerger.TotalRoundNumber);

            System.Console.WriteLine();
            System.Console.WriteLine();

            tournamentDeBerger.StartTournement();

            var keyPressed = new ConsoleKeyInfo();
            while (keyPressed.Key != ConsoleKey.Q)
            {
                System.Console.WriteLine("V pour saisie des résultats, A pour Afficher toutes les rondes, Q pour quitter");
                System.Console.WriteLine("S pour ronde suivante");
                keyPressed = System.Console.ReadKey();
                System.Console.WriteLine();

                switch (keyPressed.Key)
                {
                    case ConsoleKey.V:
                        tournamentDeBerger.SetResultForCurrentRound();
                        break;
                    case ConsoleKey.A:
                        tournamentDeBerger.DisplayAllRounds();
                        break;
                    case ConsoleKey.R:
                        tournamentDeBerger.NextRound();
                        break;
                    case ConsoleKey.S:
                        new RankingService().SetRanking(tournamentDeBerger);
                        break;
                }
            }
        }
    }
}
