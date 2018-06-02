using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            tournoi.StartTournement();

            System.Console.WriteLine("Nombre de joeur inscrits :" + tournoi.Players.Count);
            System.Console.WriteLine("Nomre de Ronde : " + tournoi.TotalRondeNumber);

            tournoi.NextRonde();

            System.Console.ReadKey();
        }

    }
}
