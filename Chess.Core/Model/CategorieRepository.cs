using System;

namespace Chess.Core.Model
{
    public class CategorieRepository
    {
        public Categorie GetByAge(int age)
        {
            switch (age)
            {
                case 8:
                case 9:
                    return new Categorie("Petit Poussin");
                case 10:
                    return new Categorie("Poussin");
                case 11:
                    return new Categorie("Pupille");
                default:
                    throw new NotImplementedException();
            }
        }
    }
}