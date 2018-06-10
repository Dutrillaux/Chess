using System;
using Chess.Core.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Chess.Core.Tests
{
    [TestClass]
    public class Ranking_Service_Tests
    {
        private readonly TournamentDeBerger _tournamentdeBerger = new TournamentDeBerger();

        private void Initialize()
        {
            _tournamentdeBerger.AddPlayer("Victor", "Dutrillaux", 8);
            _tournamentdeBerger.AddPlayer("Arthur", "Dutrillaux", 10);
            _tournamentdeBerger.AddPlayer("Adam", "Oualalou", 10);
            _tournamentdeBerger.AddPlayer("Raphael", "Perret", 10);
            _tournamentdeBerger.AddPlayer("Octave", "Perret", 10);

            _tournamentdeBerger.StartTournement();

            var cpt = 0;
            foreach (var round in _tournamentdeBerger.Rounds)
            {

                foreach (var game in round.Games)
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
            _tournamentdeBerger.DisplayAllRounds();
        }
    }
}
