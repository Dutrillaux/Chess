using System;
using System.Collections.Generic;
using Chess.Core.Command;
using Chess.Core.Model;
using Tools;

namespace Chess.Core
{
    public class TournamentDeBerger : ITournament, ISetupTournament
    {
        public readonly Tournament Tournament;

        public int TotalRoundNumber => ContestantNumber() - 1;

        public List<Player> Players => Tournament.Players;

        public List<Round> Rounds => Tournament.Rounds;

        public TournamentDeBerger()
        {
            Tournament = new Tournament();
        }

        public void AddPlayer(string prenom, string nom, int age)
        {
            Tournament.AddPlayer(prenom, nom, age);
        }
        public void AddPlayer(Player player)
        {
            Tournament.AddPlayer(player);
        }

        public void StartTournement()
        {
            Logger.WriteLine("Type de tournoi : Table de Berger");
            Logger.WriteLine();

            var rounds = PopulateRounds(Players);

            Tournament.Rounds = rounds;

            DisplayAllRounds();
        }

        public void ForceRounds(List<Round> rounds)
        {
            Tournament.Rounds = rounds;
        }

        public void SetResultForCurrentRound(Action<ICommandGameResult> setGameResultForGame)
        {
            Tournament.SetResultForCurrentRound(setGameResultForGame);

            DisplayAllRounds();

            NextRound();
        }

        public void NextRound()
        {
            Tournament.NextRound();
        }

        private List<Round> PopulateRounds(List<Player> players)
        {
            var result = new List<Round>();

            for (var currentRoundNumber = 1; currentRoundNumber <= TotalRoundNumber; currentRoundNumber++)
            {
                var round = GetRound(players, currentRoundNumber, ContestantNumber());

                result.Add(round);
            }

            return result;
        }


        public void DisplayAllRounds()
        {
            var currentRoundNumber = 1;
            foreach (var round in Rounds)
            {
                round.Display(currentRoundNumber, Tournament.MaxDisplayLenght);
                currentRoundNumber++;
            }
        }

        public int ContestantNumber()
        {
            int contestantNumber;
            if (Players.Count % 2 == 1)
            {
                contestantNumber = Players.Count + 1;
            }
            else
            {
                contestantNumber = Players.Count;
            }

            return contestantNumber;
        }

        private static Round GetRound(List<Player> players, int currentRound, int contestantNumber)
        {
            var result = new Round();

            var playersAlreadyInRound = new List<Player>();

            foreach (var playerA in players)
            {
                if (playersAlreadyInRound.Contains(playerA)) continue;

                var bNumber = currentRound - playerA.Id + contestantNumber;
                if (bNumber > contestantNumber)
                {
                    bNumber = currentRound - playerA.Id + 1;
                }

                if (bNumber == playerA.Id)
                {
                    bNumber = contestantNumber;
                }

                var playerB = players.Find(x => x.Id == bNumber);

                if (playerB == null)
                {
                    playerB = new NullPlayer(bNumber);
                }

                var game = GetGame(playerB, playerA);
                result.Games.Add(game);

                if (playersAlreadyInRound.Contains(playerB))
                    throw new Exception("ne doit pas arriver !!!");

                playersAlreadyInRound.Add(playerA);
                playersAlreadyInRound.Add(playerB);
            }

            return result;
        }

        private static Game GetGame(Player playerB, Player playerA)
        {
            Player whiteContestant;
            Player blancContestant;

            if ((playerA.Id + playerB.Id) % 2 == 1)
            {
                if (playerA.Id > playerB.Id)
                {
                    whiteContestant = playerB;
                    blancContestant = playerA;
                }
                else
                {
                    whiteContestant = playerA;
                    blancContestant = playerB;
                }
            }
            else
            {
                if (playerA.Id < playerB.Id)
                {
                    whiteContestant = playerB;
                    blancContestant = playerA;
                }
                else
                {
                    whiteContestant = playerA;
                    blancContestant = playerB;
                }
            }

            return new Game(whiteContestant, blancContestant);
        }
    }
}