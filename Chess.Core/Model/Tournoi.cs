using System;
using System.Collections.Generic;

namespace Chess.Core.Model
{
    public class Tournoi
    {
        public readonly List<Player> Players = new List<Player>();
        //public int NombreDeRonde { get; set; }
        //public int CurrentRondeNumber { get; set; }
        public List<Ronde> Rondes = new List<Ronde>();

        public int MaxDisplayLenght;

        public void AddPlayer(string prenom, string nom, int age)
        {
            var player = new Player(Players.Count + 1, prenom, nom, age);
            Players.Add(player);

            MaxDisplayLenght = Math.Max(MaxDisplayLenght, player.DisplayLength);
        }
    }
}