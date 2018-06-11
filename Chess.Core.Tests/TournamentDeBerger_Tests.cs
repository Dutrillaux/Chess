using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chess.Core.Command;
using Chess.Core.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Chess.Core.Tests
{
    [TestClass]
    public class TournamentDeBerger_Tests
    {
        [TestMethod]
        public void Should_Ranking_Based_On_Direct_Confrontation()
        {
            var tournamentDeBerger = GetTournamentDeBergerWithBasicResults();

            var rankingService = new RankingService();
            var ranking = rankingService.SetRankingByDirectConfrontation(tournamentDeBerger);

            Assert.IsTrue(ranking.Count == tournamentDeBerger.Players.Count, "Le nombre de joueur dans ronking n'est pas correct");
            Assert.AreEqual(ranking.First(x => x.PlayerId == 1).Rank, 0);
            Assert.AreEqual(ranking.First(x => x.PlayerId == 2).Rank, 1);
            Assert.AreEqual(ranking.First(x => x.PlayerId == 3).Rank, 0);
            Assert.AreEqual(ranking.First(x => x.PlayerId == 4).Rank, 5);
            Assert.AreEqual(ranking.First(x => x.PlayerId == 5).Rank, 2);
        }

        [TestMethod]
        public void Should_Points_Based_On_GameResults()
        {
            var tournamentDeBerger = GetTournamentDeBergerWithBasicResults();

            var rankingService = new RankingService();
            var ranking = rankingService.SetRankingByDirectConfrontation(tournamentDeBerger);

            Assert.IsTrue(ranking.Count == tournamentDeBerger.Players.Count, "Le nombre de joueur dans ronking n'est pas correct");
            Assert.AreEqual(ranking.First(x => x.PlayerId == 1).Points, 2.0m);
            Assert.AreEqual(ranking.First(x => x.PlayerId == 2).Points, 4.5m);
            Assert.AreEqual(ranking.First(x => x.PlayerId == 3).Points, 2.0m);
            Assert.AreEqual(ranking.First(x => x.PlayerId == 4).Points, 1.0m);
            Assert.AreEqual(ranking.First(x => x.PlayerId == 5).Points, 2.5m);
        }

        private static ISetupTournament GetTournamentDeBergerWithBasicResults()
        {
            ISetupTournament tournamentDeBerger = new TournamentDeBerger();

            tournamentDeBerger.AddPlayer("John", "Doe", 25);
            tournamentDeBerger.AddPlayer("Johnny", "Good", 25);
            tournamentDeBerger.AddPlayer("Billy", "The Kid", 18);
            tournamentDeBerger.AddPlayer("Robert", "Michoum", 25);
            tournamentDeBerger.AddPlayer("Mike", "Orson", 18);

            tournamentDeBerger.StartTournement();

            var cpt = 0;
            foreach (var round in tournamentDeBerger.Rounds)
            {
                foreach (var game in round.Games)
                {
                    cpt++;
                    if (game.BlackContestant.Id == 2)
                    {
                        SetBlackContestantWinner(game);
                    }
                    else if (cpt % 2 == 0)
                    {
                        SetWhiteContestantWinner(game);
                    }
                    else
                    {
                        SetNoContestantWinner(game);
                    }
                }
            }

            return tournamentDeBerger;
        }

        private static void SetWhiteContestantWinner(ICommandGameResult gameResultcommand)
        {
            gameResultcommand.SetGameResultCommand(GameResult.WinnerWhite);
        }
        private static void SetBlackContestantWinner(ICommandGameResult gameResultcommand)
        {
            gameResultcommand.SetGameResultCommand(GameResult.WinnerBlack);
        }

        private static void SetNoContestantWinner(ICommandGameResult gameResultcommand)
        {
            gameResultcommand.SetGameResultCommand(GameResult.NoWinnerPat);
        }

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
    }
}
