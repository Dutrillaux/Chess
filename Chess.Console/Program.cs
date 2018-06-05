using System;
using Chess.Core;

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
            
            var keyPressed = System.Console.ReadKey();
            while (keyPressed.Key != ConsoleKey.Q)
            {
                switch (keyPressed.Key)
                {
                    case ConsoleKey.R:
                        tournoi.SetResultForCurrentRonde();
                        break;
                }

                tournoi.NextRonde();
            }
        }
    }
}
