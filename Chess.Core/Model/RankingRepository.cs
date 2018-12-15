using System.Collections.Generic;

namespace Chess.Core.Model
{
    public class RankingRepository
    {
        public List<Ranking> Classements = new List<Ranking>();

        public RankingRepository()
        {
            Classements.AddRange(new List<Ranking>
                {
                    new Ranking("Aucun Classement"),
                    new Ranking("Elo FIDE")
                }
            );
        }
    }
}