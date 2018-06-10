using System;
using Chess.Core;
using ConsoleKey = System.ConsoleKey;

namespace Chess.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var tournoiDeBerger = new TournoiDeBerger();

            tournoiDeBerger.AddPlayer("Victor", "Dutrillaux", 8);
            tournoiDeBerger.AddPlayer("Arthur", "Dutrillaux", 10);
            tournoiDeBerger.AddPlayer("Adam", "Oualalou", 10);
            tournoiDeBerger.AddPlayer("Raphael", "Perret", 10);
            tournoiDeBerger.AddPlayer("Octave", "Perret", 10);

            System.Console.WriteLine("Nombre de joeur inscrits :" + tournoiDeBerger.Players.Count);
            System.Console.WriteLine("Nomre de Ronde : " + tournoiDeBerger.TotalRondeNumber);

            System.Console.WriteLine();
            System.Console.WriteLine();

            tournoiDeBerger.StartTournement();

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
                        tournoiDeBerger.SetResultForCurrentRonde();
                        break;
                    case ConsoleKey.A:
                        tournoiDeBerger.DisplayAllRondes();
                        break;
                    case ConsoleKey.R:
                        tournoiDeBerger.NextRonde();
                        break;
                    case ConsoleKey.S:
                        new RankingService().SetRanking(tournoiDeBerger);
                        break;
                }
            }
        }
    }
}
