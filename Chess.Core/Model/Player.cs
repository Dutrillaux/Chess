using System;

namespace Chess.Core.Model
{
    public class Player
    {
        public Player(int id, string firstName, string lastName, int age)
        {
            Id = id;
            LastName = lastName;
            FirstName = firstName;
            Age = age;

            Points = 0m;
            Rank = PlayerRanking.DefaultRank;
        }

        public int Age;
        public string LastName;
        public string FirstName;
        public int DisplayLength => LastName.Length + FirstName.Length;
        public int Id;
        public DateTime BirthDate;
        public Club Club;
        public Sex Sex;
        public Title Title;
        public Categorie Categorie;


        public decimal Points;
        public decimal PointsForRanking => Points + (FirstDepartage / 10) + (SecondDepartage / 1000);
        public int Rank;

        public decimal FirstDepartage;
        public decimal SecondDepartage;

        public override string ToString()
        {
            return $" [{Rank}] ({Id}) {LastName} {FirstName}";
        }

        public Player Clone()
        {
            return new Player(Id, FirstName, LastName , Age);
        }
    }
}