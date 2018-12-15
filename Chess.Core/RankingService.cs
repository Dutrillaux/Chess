using System.Collections.Generic;
using System.Linq;
using Chess.Core.Model;
using Tools;

namespace Chess.Core
{
    public class RankingService
    {
        public void ComputeRanking(ITournament tournament)
        {
            var players = new List<Player>(tournament.Players);

            Logger.WriteLine(" Calcul du classement");
            ComputeRankBasedPoints(players);

            if (players.Count(x => x.Rank == PlayerRanking.DefaultRank) == 0)
            {
                DisplayRanking(players);
                return;
            }

            Logger.WriteLine(" Départage nécessaire ...");
            Logger.WriteLine(" Calcul du 1e départage");
            ComputeFirstDepartageDirectConfrontation(tournament);
            ComputeRankBasedPoints(players);

            if (players.Count(x => x.Rank == PlayerRanking.DefaultRank) == 0)
            {
                DisplayRanking(players);
                return;
            }

            ComputeRankingWithDepartageKoya(players, tournament.Rounds, tournament.ContestantNumber());

            //Logger.WriteLine(" Calcul du 2e départage");
            //ComputeRanking(players);
            DisplayRanking(players);

            //return players.ToPlayerRankings();
        }

        private void DisplayMatrix(decimal[,] matrixOfResults)
        {
            Logger.WriteLine("matrix content:");

            var columnsNumber = matrixOfResults.GetLength(0);
            var rowsNumber = matrixOfResults.GetLength(1);

            for (int columnIndex = 0; columnIndex < columnsNumber; columnIndex++)
            {
                for (var rowIndex = 0; rowIndex < rowsNumber; rowIndex++)
                {
                    Logger.Write($"{matrixOfResults.GetValue(rowIndex, columnIndex)} | ");
                }

                Logger.WriteLine();
                Logger.WriteLine("------------------------------------");
            }
        }

        private void ComputeFirstDepartageDirectConfrontation(ITournament tournament)
        {
            Logger.WriteLine(" Calcul départage : Confrontation directe");

            // calculate points
            var playersWithoutRanking = tournament.Players.Where(x => x.Rank.Equals(PlayerRanking.DefaultRank)).ToList();

            var matrixOfResults = new decimal[tournament.Players.Count, tournament.Players.Count];

            foreach (var round in tournament.Rounds)
            {
                foreach (var game in round.Games)
                {
                    if (!playersWithoutRanking.Contains(game.BlackContestant) ||
                        !playersWithoutRanking.Contains(game.WhiteContestant)) continue;

                    GameResultHelper.GetResult(game.GameResult, out var whiteContestantPoints, out var blackContestantPoints);

                    matrixOfResults[game.WhiteContestant.Id, game.BlackContestant.Id] = whiteContestantPoints;
                    matrixOfResults[game.BlackContestant.Id, game.WhiteContestant.Id] = blackContestantPoints;
                }
            }

            DisplayMatrix(matrixOfResults);

            var resultFromMatrix = new decimal[tournament.Players.Count];

            var columnsNumber = matrixOfResults.GetLength(0);
            var rowsNumber = matrixOfResults.GetLength(1);
            const decimal defaultCumulValue = 0m;
            for (var columnIndex = 0; columnIndex < columnsNumber; columnIndex++)
            {
                decimal cumul = defaultCumulValue;
                for (var rowIndex = 0; rowIndex < rowsNumber; rowIndex++)
                {
                    cumul += (decimal)matrixOfResults.GetValue(rowIndex, columnIndex);
                }

                resultFromMatrix[columnIndex] = cumul;
                Logger.WriteLine(" cumul = " + cumul);
            }

            for (int i = 0; i < tournament.Players.Count; i++)
            {
                var cumul = resultFromMatrix[i];
                if (!cumul.Equals(defaultCumulValue))
                {
                    tournament.Players[i].FirstDepartage = cumul;
                }
            }

            //// calculate ranking
            //// délégue responsabilité au calcul général de ranking
            //var listOfUniqueResults = new List<decimal>();
            //foreach (var t in resultFromMatrix)
            //{
            //    var cpt = resultFromMatrix.Count(x => x.Equals(t));
            //    if (cpt.Equals(1))
            //    {
            //        listOfUniqueResults.Add(t);
            //    }
            //}

            //foreach (var player in tournament.Players)
            //{
            //    if (player.Rank != PlayerRanking.DefaultRank)
            //        continue;
            //}
        }

        private void ComputeRankingWithDepartageKoya(IReadOnlyCollection<Player> players,
            IReadOnlyCollection<Round> rounds, int contestantNumber)
        {
            Logger.WriteLine(" Calcul départage : Koya 50%");

            IEnumerable<decimal> listResultDistinct = players
                .Where(x => x.Rank.Equals(PlayerRanking.DefaultRank))
                .Select(x => x.Points)
                .Distinct()
                .OrderByDescending(x => x);

            foreach (var result in listResultDistinct)
            {
                var playerToDepartage = players.Where(x => x.Points == result).ToList();

                if (playerToDepartage.Count <= 1) continue;

                var halfPoint = contestantNumber / 2;
                var ids = players.Where(x => x.Points >= halfPoint).Select(x => x.Id);

                //var halfPointOrMorePointPlayers = new List<Player>();
                //foreach (var id in ids)
                //{
                //    halfPointOrMorePointPlayers.Add(players.First(x => x.Id == id));
                //}
                ComputeRankBasedPoints(players);

                //var halfPointOrMorePointPlayers = players.All(x => x.Id == ids).ToList();
            }
        }

        /// <summary>
        /// Compute Ranking based only on player points
        /// </summary>
        /// <param name="players"></param>
        private void ComputeRankBasedPoints(IReadOnlyCollection<Player> players)
        {
            IEnumerable<decimal> listResultDistinct = players
                .Where(x => x.Rank == PlayerRanking.DefaultRank)
                .Select(x => x.PointsForRanking)
                .Distinct()
                .OrderByDescending(x => x);

            var rank = 0;
            foreach (var result in listResultDistinct)
            {
                var playersWithThisResult = players.Where(x => x.Points == result).ToList();
                if (playersWithThisResult.Count == 1)
                {
                    rank++;
                    playersWithThisResult.First().Rank = rank;
                }
                else
                {
                    rank += playersWithThisResult.Count;
                }
            }
        }

        public void DisplayRanking(IEnumerable<Player> players)
        {
            Logger.WriteLine($" Position [Points] {new NullPlayer(0)}");
            Logger.WriteLine();
            foreach (var playerRanking in players.OrderByDescending(x => x.Points))
            {
                Logger.WriteLine($" {playerRanking.Rank} [{playerRanking.Points}] {playerRanking}");
            }
        }
    }
}
