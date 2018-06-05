using System;
using Chess.Core;
using ConsoleKey = System.ConsoleKey;

namespace Chess.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var tournoi = new TournoideBerger();

            tournoi.AddPlayer("Victor", "Dutrillaux", 8);
            tournoi.AddPlayer("Arthur", "Dutrillaux", 10);
            tournoi.AddPlayer("Adam", "Oualalou", 10);
            tournoi.AddPlayer("Raphael", "Perret", 10);
            tournoi.AddPlayer("Octave", "Perret", 10);

            System.Console.WriteLine("Nombre de joeur inscrits :" + tournoi.Players.Count);
            System.Console.WriteLine("Nomre de Ronde : " + tournoi.TotalRondeNumber);

            System.Console.WriteLine();
            System.Console.WriteLine();

            tournoi.StartTournement();

            var keyPressed = new ConsoleKeyInfo();
            while (keyPressed.Key != ConsoleKey.Q)
            {
                System.Console.WriteLine("R pour saisie des résultats, A pour Afficher toutes les rondes, Q pour quitter");
                System.Console.WriteLine("S pour ronde suivante");
                keyPressed = System.Console.ReadKey();

                switch (keyPressed.Key)
                {
                    case ConsoleKey.R:
                        tournoi.SetResultForCurrentRonde();
                        break;
                    case ConsoleKey.A:
                        tournoi.DisplayAllRondes();
                        break;
                    case ConsoleKey.S:
                        tournoi.NextRonde();
                        break;
                }
            }
        }
    }
}
