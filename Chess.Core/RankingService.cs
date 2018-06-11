using System;
using System.Collections.Generic;
using System.Linq;
using Chess.Core.Model;

namespace Chess.Core
{
    public class RankingService
    {
        public List<PlayerRanking> CalculateRanking(ITournament tournament)
        {
            var players = new List<Player>(tournament.Players);
            
            var playerRankings = GetEmptyPlayerRankings(players);

            SetRankingByDirectConfrontation(playerRankings, players, tournament.Rounds, true);
            SetClassement(playerRankings);
            DisplayRanking(playerRankings);

            if (playerRankings.Count(x => x.Rank == PlayerRanking.DefaultRank) == 0)
                return playerRankings;

            Console.WriteLine(" Calcul du 2e départage");
            SetRankingWithDepartageKoya(playerRankings, players, tournament.Rounds, tournament.ContestantNumber());
            SetClassement(playerRankings);
            DisplayRanking(playerRankings);

            return playerRankings;
        }

        private static List<PlayerRanking> GetEmptyPlayerRankings(IEnumerable<Player> players)
        {
            return players.Select(player => new PlayerRanking(player.Id, player.Nom, player.Prenom)).ToList();
        }

        public List<PlayerRanking> GetRankingWithDepartageKoya(ITournament tournament)
        {
            var playerRankings = GetEmptyPlayerRankings(tournament.Players);

            SetRankingWithDepartageKoya(playerRankings, tournament.Players,
                tournament.Rounds, tournament.ContestantNumber());

            return playerRankings;
        }

        private void SetRankingWithDepartageKoya(List<PlayerRanking> playerRankings,
            IReadOnlyCollection<Player> players,
            IReadOnlyCollection<Round> rounds, int contestantNumber)
        {
            IEnumerable<decimal> listResultDistinct = playerRankings.Select(x => x.Points).Distinct().OrderByDescending(x => x);

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

        public void SetClassement(IReadOnlyCollection<PlayerRanking> playerRankings)
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

        public void DisplayRanking(IEnumerable<PlayerRanking> playerRankings)
        {
            Console.WriteLine($" Position [Points] {new NullPlayer(0)}");
            Console.WriteLine();
            foreach (var playerRanking in playerRankings.OrderByDescending(x => x.Points))
            {
                Console.WriteLine($" {playerRanking.Rank} [{playerRanking.Points}] {playerRanking}");
            }
        }

        public List<PlayerRanking> SetRankingByDirectConfrontation(ITournament tournament)
        {
            var playerRankings = GetEmptyPlayerRankings(tournament.Players);

            SetRankingByDirectConfrontation(playerRankings, tournament.Players,
                tournament.Rounds, true);

            return playerRankings;
        }

        private void SetRankingByDirectConfrontation(IReadOnlyCollection<PlayerRanking> playerRankings,
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
                            winner = players.FirstOrDefault(x => x.Id == game.BlackContestant.Id);
                            if (ignoreNull && winner == null) continue;
                            winnerId = winner.Id;
                            playerRankings.First(x => x.PlayerId == winnerId).Points += 1m;
                            break;
                        case GameResult.WinnerWhite:
                            winner = players.FirstOrDefault(x => x.Id == game.WhiteContestant.Id);
                            if (ignoreNull && winner == null) continue;
                            winnerId = winner.Id;
                            playerRankings.First(x => x.PlayerId == winnerId).Points += 1m;
                            break;
                        case GameResult.NoWinnerPat:
                            winner = players.FirstOrDefault(x => x.Id == game.BlackContestant.Id);
                            if (ignoreNull && winner == null) continue;
                            winnerId = winner.Id;
                            playerRankings.First(x => x.PlayerId == winnerId).Points += 0.5m;

                            winner = players.FirstOrDefault(x => x.Id == game.WhiteContestant.Id);
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

            SetClassement(playerRankings);
            DisplayRanking(playerRankings);
        }
    }
}
