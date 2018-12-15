using System;
using System.Linq;
using Chess.Core.Command;
using Chess.Core.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Chess.Core.Tests
{
    [TestClass]
    public class Tournament_De_Berger_Tests
    {
        [TestMethod]
        public void Should_Ranking_Based_On_Points()
        {
            var tournamentDeBerger = GetTournamentDeBergerWithResults();

            var rankingService = new RankingService();
            rankingService.ComputeRanking(tournamentDeBerger);

            var players = tournamentDeBerger.Players;

            Assert.AreEqual(1, players.First(x => x.Id == 1).Rank);
            Assert.AreEqual(7, players.First(x => x.Id == 5).Rank);
            Assert.AreEqual(8, players.First(x => x.Id == 9).Rank);
            Assert.AreEqual(9, players.First(x => x.Id == 8).Rank);
            Assert.AreEqual(10, players.First(x => x.Id == 10).Rank);
        }

        [TestMethod]
        public void Should_Ranking_Based_On_Confrontation_Directe()
        {
            var tournamentDeBerger = GetTournamentDeBergerWithResults();

            var rankingService = new RankingService();
            rankingService.ComputeRanking(tournamentDeBerger);

            var players = tournamentDeBerger.Players;

            Assert.AreEqual(4, players.First(x => x.Id == 7).Rank);
        }

        [TestMethod]
        public void Should_Ranking_Based_On_Koya_Extended_4_5()
        {
            var tournamentDeBerger = GetTournamentDeBergerWithResults();

            var rankingService = new RankingService();
            rankingService.ComputeRanking(tournamentDeBerger);

            var players = tournamentDeBerger.Players;

            Assert.AreEqual(3, players.First(x => x.Id == 2).Rank);
            Assert.AreEqual(2, players.First(x => x.Id == 3).Rank);
        }

        [TestMethod]
        public void Should_Ranking_Based_On_Koya_Extended_3()
        {
            var tournamentDeBerger = GetTournamentDeBergerWithResults();

            var rankingService = new RankingService();
            rankingService.ComputeRanking(tournamentDeBerger);

            var players = tournamentDeBerger.Players;

            Assert.AreEqual(5, players.First(x => x.Id == 4).Rank);
            Assert.AreEqual(6, players.First(x => x.Id == 6).Rank);
        }

        [TestMethod]
        public void Should_Ranking_Based_On_Direct_Confrontation()
        {
            var tournamentDeBerger = GetTournamentDeBergerWithBasicResults();

            var rankingService = new RankingService();
            rankingService.ComputeRanking(tournamentDeBerger);

            var players = tournamentDeBerger.Players;

            Assert.AreEqual(PlayerRanking.DefaultRank, players.First(x => x.Id == 1).Rank);
            Assert.AreEqual(1, players.First(x => x.Id == 2).Rank);
            Assert.AreEqual(PlayerRanking.DefaultRank, players.First(x => x.Id == 3).Rank);
            Assert.AreEqual(5, players.First(x => x.Id == 4).Rank);
            Assert.AreEqual(4, players.First(x => x.Id == 5).Rank);
        }

        [TestMethod]
        public void Should_Points_Based_On_GameResults()
        {
            var tournamentDeBerger = GetTournamentDeBergerWithBasicResults();

            var rankingService = new RankingService();
            rankingService.ComputeRanking(tournamentDeBerger);

            var players = tournamentDeBerger.Players;

            Assert.AreEqual(3.0m, players.First(x => x.Id == 1).Points);
            Assert.AreEqual(4.5m, players.First(x => x.Id == 2).Points);
            Assert.AreEqual(3.0m, players.First(x => x.Id == 3).Points);
            Assert.AreEqual(2.0m, players.First(x => x.Id == 4).Points);
            Assert.AreEqual(2.5m, players.First(x => x.Id == 5).Points);
        }

        private static ISetupTournament GetTournamentDeBergerWithResults()
        {
            ISetupTournament tournamentDeBerger = new TournamentDeBerger();

            tournamentDeBerger.AddPlayer("Albert", "1", 25);
            tournamentDeBerger.AddPlayer("Bernard", "2", 25);
            tournamentDeBerger.AddPlayer("Claude", "3", 18);
            tournamentDeBerger.AddPlayer("Denis", "4", 25);
            tournamentDeBerger.AddPlayer("Eric", "5", 18);
            tournamentDeBerger.AddPlayer("Franck", "6", 18);
            tournamentDeBerger.AddPlayer("Guy", "7", 18);
            tournamentDeBerger.AddPlayer("Herbert", "8", 18);
            tournamentDeBerger.AddPlayer("Isidore", "9", 18);
            tournamentDeBerger.AddPlayer("Jack", "10", 18);

            tournamentDeBerger.StartTournement();

            foreach (var round in tournamentDeBerger.Rounds)
            {
                foreach (var game in round.Games)
                {
                    var gameResult = GetResultFor(game.WhiteContestant, game.BlackContestant);
                    game.SetGameResultCommand(gameResult);
                }
            }

            return tournamentDeBerger;
        }

        private static GameResult GetResultFor(Player whitePlayer, Player blackPlayer)
        {
            if (whitePlayer.Id == 1)
            {
                if (blackPlayer.Id == 2) return GameResult.NoWinnerPat;
                if (blackPlayer.Id == 3) return GameResult.NoWinnerPat;
                if (blackPlayer.Id == 4) return GameResult.NoWinnerPat;
                if (blackPlayer.Id == 5) return GameResult.NoWinnerPat;
                if (blackPlayer.Id == 6) return GameResult.WinnerWhite;
                if (blackPlayer.Id == 7) return GameResult.NoWinnerPat;
                if (blackPlayer.Id == 8) return GameResult.WinnerWhite;
                if (blackPlayer.Id == 9) return GameResult.WinnerWhite;
                if (blackPlayer.Id == 10) return GameResult.WinnerWhite;
            }
            if (whitePlayer.Id == 2)
            {
                if (blackPlayer.Id == 1) return GameResult.NoWinnerPat;
                if (blackPlayer.Id == 3) return GameResult.NoWinnerPat;
                if (blackPlayer.Id == 4) return GameResult.NoWinnerPat;
                if (blackPlayer.Id == 5) return GameResult.WinnerBlack;
                if (blackPlayer.Id == 6) return GameResult.WinnerWhite;
                if (blackPlayer.Id == 7) return GameResult.NoWinnerPat;
                if (blackPlayer.Id == 8) return GameResult.NoWinnerPat;
                if (blackPlayer.Id == 9) return GameResult.NoWinnerPat;
                if (blackPlayer.Id == 10) return GameResult.WinnerWhite;
            }
            if (whitePlayer.Id == 3)
            {
                if (blackPlayer.Id == 1) return GameResult.NoWinnerPat;
                if (blackPlayer.Id == 2) return GameResult.NoWinnerPat;
                if (blackPlayer.Id == 4) return GameResult.NoWinnerPat;
                if (blackPlayer.Id == 5) return GameResult.WinnerBlack;
                if (blackPlayer.Id == 6) return GameResult.WinnerWhite;
                if (blackPlayer.Id == 7) return GameResult.NoWinnerPat;
                if (blackPlayer.Id == 8) return GameResult.WinnerWhite;
                if (blackPlayer.Id == 9) return GameResult.NoWinnerPat;
                if (blackPlayer.Id == 10) return GameResult.NoWinnerPat;
            }
            if (whitePlayer.Id == 4)
            {
                if (blackPlayer.Id == 1) return GameResult.NoWinnerPat;
                if (blackPlayer.Id == 2) return GameResult.NoWinnerPat;
                if (blackPlayer.Id == 3) return GameResult.NoWinnerPat;
                if (blackPlayer.Id == 5) return GameResult.WinnerWhite;
                if (blackPlayer.Id == 6) return GameResult.WinnerBlack;
                if (blackPlayer.Id == 7) return GameResult.NoWinnerPat;
                if (blackPlayer.Id == 8) return GameResult.WinnerWhite;
                if (blackPlayer.Id == 9) return GameResult.NoWinnerPat;
                if (blackPlayer.Id == 10) return GameResult.NoWinnerPat;
            }
            if (whitePlayer.Id == 5)
            {
                if (blackPlayer.Id == 1) return GameResult.NoWinnerPat;
                if (blackPlayer.Id == 2) return GameResult.WinnerWhite;
                if (blackPlayer.Id == 3) return GameResult.WinnerWhite;
                if (blackPlayer.Id == 4) return GameResult.WinnerBlack;
                if (blackPlayer.Id == 6) return GameResult.NoWinnerPat;
                if (blackPlayer.Id == 7) return GameResult.NoWinnerPat;
                if (blackPlayer.Id == 8) return GameResult.WinnerBlack;
                if (blackPlayer.Id == 9) return GameResult.WinnerWhite;
                if (blackPlayer.Id == 10) return GameResult.WinnerBlack;
            }
            if (whitePlayer.Id == 6)
            {
                if (blackPlayer.Id == 1) return GameResult.WinnerBlack;
                if (blackPlayer.Id == 2) return GameResult.WinnerBlack;
                if (blackPlayer.Id == 3) return GameResult.WinnerBlack;
                if (blackPlayer.Id == 4) return GameResult.WinnerWhite;
                if (blackPlayer.Id == 5) return GameResult.NoWinnerPat;
                if (blackPlayer.Id == 7) return GameResult.NoWinnerPat;
                if (blackPlayer.Id == 8) return GameResult.WinnerWhite;
                if (blackPlayer.Id == 9) return GameResult.WinnerWhite;
                if (blackPlayer.Id == 10) return GameResult.WinnerWhite;
            }
            if (whitePlayer.Id == 7)
            {
                if (blackPlayer.Id == 1) return GameResult.NoWinnerPat;
                if (blackPlayer.Id == 2) return GameResult.NoWinnerPat;
                if (blackPlayer.Id == 3) return GameResult.NoWinnerPat;
                if (blackPlayer.Id == 4) return GameResult.NoWinnerPat;
                if (blackPlayer.Id == 5) return GameResult.NoWinnerPat;
                if (blackPlayer.Id == 6) return GameResult.NoWinnerPat;
                if (blackPlayer.Id == 8) return GameResult.NoWinnerPat;
                if (blackPlayer.Id == 9) return GameResult.WinnerWhite;
                if (blackPlayer.Id == 10) return GameResult.NoWinnerPat;
            }
            if (whitePlayer.Id == 8)
            {
                if (blackPlayer.Id == 1) return GameResult.WinnerBlack;
                if (blackPlayer.Id == 2) return GameResult.NoWinnerPat;
                if (blackPlayer.Id == 3) return GameResult.WinnerBlack;
                if (blackPlayer.Id == 4) return GameResult.WinnerBlack;
                if (blackPlayer.Id == 5) return GameResult.WinnerBlack;
                if (blackPlayer.Id == 6) return GameResult.WinnerBlack;
                if (blackPlayer.Id == 7) return GameResult.NoWinnerPat;
                if (blackPlayer.Id == 9) return GameResult.WinnerBlack;
                if (blackPlayer.Id == 10) return GameResult.WinnerBlack;
            }
            if (whitePlayer.Id == 9)
            {
                if (blackPlayer.Id == 1) return GameResult.WinnerBlack;
                if (blackPlayer.Id == 2) return GameResult.NoWinnerPat;
                if (blackPlayer.Id == 3) return GameResult.NoWinnerPat;
                if (blackPlayer.Id == 4) return GameResult.NoWinnerPat;
                if (blackPlayer.Id == 5) return GameResult.WinnerBlack;
                if (blackPlayer.Id == 6) return GameResult.WinnerBlack;
                if (blackPlayer.Id == 7) return GameResult.WinnerBlack;
                if (blackPlayer.Id == 8) return GameResult.WinnerWhite;
                if (blackPlayer.Id == 10) return GameResult.WinnerWhite;
            }
            if (whitePlayer.Id == 10)
            {
                if (blackPlayer.Id == 1) return GameResult.WinnerBlack;
                if (blackPlayer.Id == 2) return GameResult.WinnerBlack;
                if (blackPlayer.Id == 3) return GameResult.NoWinnerPat;
                if (blackPlayer.Id == 4) return GameResult.NoWinnerPat;
                if (blackPlayer.Id == 5) return GameResult.WinnerWhite;
                if (blackPlayer.Id == 6) return GameResult.WinnerBlack;
                if (blackPlayer.Id == 7) return GameResult.NoWinnerPat;
                if (blackPlayer.Id == 8) return GameResult.WinnerBlack;
                if (blackPlayer.Id == 9) return GameResult.WinnerBlack;
            }

            throw new NotImplementedException();
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
                    if (game.BlackContestant is NullPlayer)
                    {
                        SetWhiteContestantWinner(game);
                    }
                    else if (game.WhiteContestant is NullPlayer)
                    {
                        SetBlackContestantWinner(game);
                    }
                    else if (game.BlackContestant.Id == 2)
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
