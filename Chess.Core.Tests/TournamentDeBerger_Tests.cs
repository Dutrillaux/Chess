using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Chess.Core.Tests
{
    [TestClass]
    public class TournamentDeBerger_Tests
    {
        [TestMethod]
        public void Has_4_Contestants_When_3_Players()
        {
            var tournamentDeBerger = new TournamentDeBerger();

            Assert.AreEqual(0, tournamentDeBerger.Players.Count);
            tournamentDeBerger.AddPlayer("John", "Doe", 25);
            tournamentDeBerger.AddPlayer("Johnny", "Good", 25);
            tournamentDeBerger.AddPlayer("Billy", "The Kid", 18);

            Assert.AreEqual(3, tournamentDeBerger.Players.Count);
            Assert.AreEqual(4, tournamentDeBerger.ContestantNumber());
        }

        [TestMethod]
        public void Has_3_Rounds_When_3_Players()
        {
            var tournamentDeBerger = new TournamentDeBerger();

            tournamentDeBerger.AddPlayer("John", "Doe", 25);
            tournamentDeBerger.AddPlayer("Johnny", "Good", 25);
            tournamentDeBerger.AddPlayer("Billy", "The Kid", 18);

            Assert.AreEqual(3, tournamentDeBerger.Players.Count);

            Assert.AreEqual(0, tournamentDeBerger.Rounds.Count);
            tournamentDeBerger.StartTournement();
            Assert.AreEqual(3, tournamentDeBerger.Rounds.Count);
        }

        [TestMethod]
        public void Has_Game_Rankin_When_Games_Results_Are_Filled()
        {
            var tournamentDeBerger = new TournamentDeBerger();

            tournamentDeBerger.AddPlayer("John", "Doe", 25);
            tournamentDeBerger.AddPlayer("Johnny", "Good", 25);
            tournamentDeBerger.AddPlayer("Billy", "The Kid", 18);

            Assert.AreEqual(3, tournamentDeBerger.Players.Count);

            tournamentDeBerger.StartTournement();

            tournamentDeBerger.SetResultForCurrentRound();

            var rankingService = new RankingService();
            var ranking = rankingService.CalculateRanking(tournamentDeBerger);
            
            Assert.IsNotNull(ranking);
        }
    }
}
