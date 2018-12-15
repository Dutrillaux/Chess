namespace Chess.Core.Model
{
    public class PlayerRanking
    {
        public int PlayerId;
        private readonly string _lastName;
        private readonly string _firstName;
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

        public const int DefaultRank = -1;
        public int Rank;
        public decimal Departage1;
        public decimal Departage2;

        public PlayerRanking(int id, string firstName, string lastName)
        {
            PlayerId = id;
            _lastName = lastName;
            _firstName = firstName;

            Rank = DefaultRank;
        }

        public void InitializeRanking()
        {
            _points = 0;
            _numberOfResultSetted = 0;
            Rank = DefaultRank;
        }

        public override string ToString()
        {
            return $"[{Rank}] ({PlayerId}) {_lastName} {_firstName}";
        }
    }
}