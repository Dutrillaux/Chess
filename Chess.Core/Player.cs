using System;

namespace Chess.Core
{
    public class Player
    {
        public Player(int id, string prenom, string nom, int age)
        {
            Id = id;
            Nom = nom;
            Prenom = prenom;
            Age = age;
            }

        public int Age { get; set; }
        public string Nom;
        public string Prenom;
        public int Id;
        public DateTime BirthDateTime;
        public Club Club;
        public Sex Sex;
        public Title Title;
        public Classement Classement;
        public Categorie Categorie;

        public override string ToString()
        {
            return this.Id + " " + this.Nom + " " + this.Prenom;
        }
    }
}