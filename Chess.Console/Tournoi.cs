using System.Collections.Generic;
using Chess.Core;

namespace Chess.Console
{
    public class Tournoi
    {
        public readonly List<Player> Players = new List<Player>();
        //public int NombreDeRonde { get; set; }
        //public int CurrentRondeNumber { get; set; }
        public List<Ronde> Rondes = new List<Ronde>();

        public void AddPlayer(string prenom, string nom, int age)
        {
            Players.Add(new Player(Players.Count + 1, prenom, nom, age));
        }
    }
}