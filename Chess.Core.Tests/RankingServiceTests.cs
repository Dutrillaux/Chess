using System;
using Chess.Core.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Chess.Core.Tests
{
    [TestClass]
    public class Ranking_Service_Tests
    {
        private TournoideBerger _tournoideBerger = new TournoideBerger();

        private void Initialize()
        {
            _tournoideBerger.AddPlayer("Victor", "Dutrillaux", 8);
            _tournoideBerger.AddPlayer("Arthur", "Dutrillaux", 10);
            _tournoideBerger.AddPlayer("Adam", "Oualalou", 10);
            _tournoideBerger.AddPlayer("Raphael", "Perret", 10);
            _tournoideBerger.AddPlayer("Octave", "Perret", 10);

            _tournoideBerger.StartTournement();

            var cpt = 0;
            foreach (var ronde in _tournoideBerger.Rondes)
            {

                foreach (var game in ronde.Games)
                {
                    cpt++;
                    if (game.BlackContestant.Id == 2)
                    {
                        game.GameResult = GameResult.WinnerBlack;
                    }
                    else if(cpt%2 == 0)
                    {
                        game.GameResult = GameResult.WinnerWhite;
                    }
                    else
                    {
                        game.GameResult = GameResult.NoWinnerPat;
                    }
                }
            }
        }

        [TestMethod]
        public void Test_Ranking_Order()
        {
            _tournoideBerger.DisplayAllRondes();
        }
    }
}
