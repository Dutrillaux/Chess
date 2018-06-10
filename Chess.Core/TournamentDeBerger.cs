using System;
using System.Collections.Generic;
using Chess.Core.Model;

namespace Chess.Core
{
    public class TournamentDeBerger : ITournament
    {
        public readonly Tournament Tournament = new Tournament();
        
        public int TotalRoundNumber => ContestantNumber() - 1;

        public List<Player> Players => Tournament.Players;

        public List<Round> Rounds => Tournament.Rounds;

        public void AddPlayer(string prenom, string nom, int age)
        {
            Tournament.AddPlayer(prenom, nom, age);
        }

        public void StartTournement()
        {
            Console.WriteLine("Type de tournoi : Table de Berger");
            Console.WriteLine();

            var rounds = PopulateRounds(Players);

            Tournament.Rounds = rounds;

            DisplayAllRounds();
        }

        public void SetResultForCurrentRound()
        {
            Tournament.SetResultForCurrentRound();

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
            //if (playerB == null)
            //{
            //    playerB = new NullPlayer();
            //    //var game = new Game(playerA, null)
            //    //{
            //    //    GameResult = GameResult.WinnerWhite
            //    //};
            //    //return game;
            //}

            if ((playerA.Id + playerB.Id) % 2 == 1)
            {
                if (playerA.Id > playerB.Id)
                {
                    return new Game(playerB, playerA);
                }
                else
                {
                    return new Game(playerA, playerB);
                }
            }
            else
            {
                if (playerA.Id < playerB.Id)
                {
                    return new Game(playerB, playerA);
                }
                else
                {
                    return new Game(playerA, playerB);
                }
            }
        }
    }
}