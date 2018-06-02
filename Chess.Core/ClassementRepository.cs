using System.Collections.Generic;

namespace Chess.Core
{
    public class ClassementRepository
    {
        public List<Classement> Classements = new List<Classement>();

        public ClassementRepository()
        {
            Classements.AddRange(new List<Classement>
                {
                    new Classement("Aucun Classement"),
                    new Classement("Elo FIDE")
                }
            );
        }
    }
}