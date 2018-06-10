using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Chess.Core.Model;

namespace Chess.Core
{
    public class RankingService
    {
        public void SetRanking(ITournement tournament)
        {
            var players = new List<Player>(tournament.Players);

            var playerRankings = new List<PlayerRanking>();
            foreach (var player in players)
            {
                playerRankings.Add(new PlayerRanking(player.Id, player.Nom, player.Prenom));
            }

            SetRankingByDirectConfrontation(playerRankings, players, tournament.Rounds);
            SetClassement(playerRankings);
            DisplayRanking(playerRankings);

            if (playerRankings.Count(x => x.Rank == PlayerRanking.DefaultRank) == 0)
                return;

            Console.WriteLine(" Calcul du 2e départage");
            SetRankingWithDepartageKoya(playerRankings, players, tournament.Rounds, tournament.ContestantNumber());
            SetClassement(playerRankings);
            DisplayRanking(playerRankings);
        }

        private void SetRankingWithDepartageKoya(List<PlayerRanking> playerRankings,
            IReadOnlyCollection<Player> players,
            IReadOnlyCollection<Round> rounds, int contestantNumber)
        {
            IEnumerable<decimal> listResultDistinct = playerRankings.Select(x => x.Points).Distinct().OrderByDescending(x => x);

            //var rank = 0;
            foreach (var result in listResultDistinct)
            {
                var playerToDepartage = playerRankings.Where(x => x.Points == result).ToList();
                if (playerToDepartage.Count > 1)
                {
                    var halfPoint = contestantNumber / 2;
                    var ids = playerRankings.Where(x => x.Points >= halfPoint).Select(x=>x.PlayerId);

                    var halfPointOrMorePointPlayers = new List<Player>();
                    foreach (var id in ids)
                    {
                        halfPointOrMorePointPlayers.Add(players.First(x=>x.Id == id));
                    }
                    SetRankingByDirectConfrontation(playerRankings, halfPointOrMorePointPlayers, rounds, true);
                    SetClassement(playerRankings);
                    
                    //var halfPointOrMorePointPlayers = players.All(x => x.Id == ids).ToList();

                }
            }
        }

        private static void SetClassement(IReadOnlyCollection<PlayerRanking> playerRankings)
        {
            IEnumerable<decimal> listResultDistinct = playerRankings
                .Where(x=>x.Rank == PlayerRanking.DefaultRank)
                .Select(x => x.Points)
                .Distinct()
                .OrderByDescending(x => x);

            var rank = 0;
            foreach (var result in listResultDistinct)
            {
                var playersWithThisResult = playerRankings.Where(x => x.Points == result).ToList();
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

        private static void DisplayRanking(IEnumerable<PlayerRanking> playerRankings)
        {
            Console.WriteLine($" Position [Points] {new NullPlayer(0)}");
            Console.WriteLine();
            foreach (var playerRanking in playerRankings.OrderByDescending(x => x.Points))
            {
                Console.WriteLine($" {playerRanking.Rank} [{playerRanking.Points}] {playerRanking}");
            }
        }

        private static void SetRankingByDirectConfrontation(List<PlayerRanking> playerRankings,
            IReadOnlyCollection<Player> players, IEnumerable<Round> rounds, bool ignoreNull = false)
        {
            foreach (var round in rounds)
            {
                foreach (var game in round.Games)
                {
                    int winnerId;
                    Player winner;
                    switch (game.GameResult)
                    {
                        case GameResult.WinnerBlack:
                            winner = players.First(x => x.Id == game.BlackContestant.Id);
                            if (ignoreNull && winner == null) continue;
                            winnerId = winner.Id;
                            playerRankings.First(x => x.PlayerId == winnerId).Points += 1m;
                            break;
                        case GameResult.WinnerWhite:
                            winner = players.First(x => x.Id == game.WhiteContestant.Id);
                            if (ignoreNull && winner == null) continue;
                            winnerId = winner.Id;
                            playerRankings.First(x => x.PlayerId == winnerId).Points += 1m;
                            break;
                        case GameResult.NoWinnerPat:
                            winner = players.First(x => x.Id == game.BlackContestant.Id);
                            if (ignoreNull && winner == null) continue;
                            winnerId = winner.Id;
                            playerRankings.First(x => x.PlayerId == winnerId).Points += 0.5m;

                            winner = players.First(x => x.Id == game.WhiteContestant.Id);
                            if (ignoreNull && winner == null) continue;
                            winnerId = winner.Id;
                            playerRankings.First(x => x.PlayerId == winnerId).Points += 0.5m;
                            break;
                        case GameResult.None:
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
            }
        }
    }
}
