using System;
using System.Collections.Generic;
using Chess.Core.Model;

namespace Chess.Core
{
    public class TournoideBerger
    {
        public readonly Tournoi Tournoi = new Tournoi();

        public List<Player> Players => Tournoi.Players;

        public List<Ronde> Rondes => Tournoi.Rondes;

        public int TotalRondeNumber => ContestantNumber() - 1;

        public void AddPlayer(string prenom, string nom, int age)
        {
            Tournoi.AddPlayer(prenom, nom, age);
        }

        public void StartTournement()
        {
            Console.WriteLine("Type de tournoi : Table de Berger");
            Console.WriteLine();

            var rondes = PopulateRondes(Players);

            Tournoi.Rondes = rondes;

            DisplayAllRondes();
        }

        public void SetResultForCurrentRonde()
        {
            Tournoi.SetResultForCurrentRonde();

            DisplayAllRondes();

            NextRonde();
        }
        public void NextRonde()
        {
            Tournoi.NextRonde();
        }

        private List<Ronde> PopulateRondes(List<Player> players)
        {
            var result = new List<Ronde>();

            for (var currentRondeNumber = 1; currentRondeNumber <= TotalRondeNumber; currentRondeNumber++)
            {
                var ronde = GetRonde(players, currentRondeNumber, ContestantNumber());

                result.Add(ronde);
            }

            return result;
        }


        public void DisplayAllRondes()
        {
            var currentRondeNumber = 1;
            foreach (var ronde in Rondes)
            {
                ronde.Display(currentRondeNumber, Tournoi.MaxDisplayLenght);
                currentRondeNumber++;
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

        private static Ronde GetRonde(List<Player> players, int currentRonde, int contestantNumber)
        {
            var result = new Ronde();

            var playersAlreadyInRonde = new List<Player>();

            foreach (var playerA in players)
            {
                if (playersAlreadyInRonde.Contains(playerA)) continue;

                var bNumber = currentRonde - playerA.Id + contestantNumber;
                if (bNumber > contestantNumber)
                {
                    bNumber = currentRonde - playerA.Id + 1;
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

                if (playersAlreadyInRonde.Contains(playerB))
                    throw new Exception("ne doit pas arriver !!!");

                playersAlreadyInRonde.Add(playerA);
                playersAlreadyInRonde.Add(playerB);
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