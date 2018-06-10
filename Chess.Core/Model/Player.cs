using System;

namespace Chess.Core.Model
{
    public class NullPlayer : Player
    {
        public NullPlayer(int id) : base(id, string.Empty, "Personne", 0)
        {
        }
    }

    public class PlayerRanking
    {
        public int PlayerId;
        private readonly string _nom;
        private readonly string _prenom;
        private decimal _points = 0;
        private int _numberOfResultSetted = 0;

        public decimal Points
        {
            get => _points;
            set
            {
                _points = value;
                _numberOfResultSetted++;
            }
        }

        public const int DefaultRank = 0;
        public int Rank;
        public decimal Departage1;
        public decimal Departage2;

        public PlayerRanking(int id, string nom, string prenom)
        {
            PlayerId = id;
            _nom = nom;
            _prenom = prenom;
        }

        public void InitializeRanking()
        {
            _points = 0;
            _numberOfResultSetted = 0;
            Rank = DefaultRank;
        }

        public override string ToString()
        {
            return $"({PlayerId}) {_nom} {_prenom}";
        }
    }

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
        public int DisplayLength => Nom.Length + Prenom.Length;
        public int Id;
        public DateTime BirthDateTime;
        public Club Club;
        public Sex Sex;
        public Title Title;
        public Classement Classement;
        public Categorie Categorie;
        //private decimal _points = 0;
        private int _numberOfResultSetted = 0;
        public int NumberOfResultSetted => _numberOfResultSetted;

        //private readonly PlayerRanking _playerRanking = new PlayerRanking();

        //public decimal Points => _playerRanking.Points;
        //public int Rank => _playerRanking.Rank;
        //public decimal Departage1 => _playerRanking.Departage1;
        //public decimal Departage2 => _playerRanking.Departage2;

        public override string ToString()
        {
            return $"({Id}) {Nom} {Prenom}";
        }
    }

    //public class Result
}