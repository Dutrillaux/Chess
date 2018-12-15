using System;
using System.Collections.Generic;
using System.Text;
using Tools;

namespace Chess.Core.Model
{
    public class Round
    {
        public List<Game> Games = new List<Game>();

        public override string ToString()
        {
            return GetRoundAsString(null, null);
        }

        private string GetRoundAsString(int? currentRoundNumber, int? maxLength)
        {
            var result = new StringBuilder();

            result.Append("Ronde ");
            if (currentRoundNumber.HasValue)
            {
                result.Append(currentRoundNumber.Value + " | ");
            }

            foreach (var game in Games)
            {
                result.Append(FormatContestant(game.WhiteContestant, maxLength));
                result.Append(FormatPoints(game));
                result.Append(FormatContestant(game.BlackContestant, maxLength));

                result.Append(" | ");
            }

            return result.ToString();
        }

        private static string FormatPoints(Game game)
        {
            switch (game.GameResult)
            {
                case GameResult.WinnerBlack:
                    return " [  ]-[+1] ";
                case GameResult.WinnerWhite:
                    return " [+1]-[  ] ";
                case GameResult.NoWinnerPat:
                    return " [.5]-[.5] ";
                default:
                    return " - ";
            }
        }

        private static string FormatContestant(Player player, int? maxLength)
        {
            var result = $"({player.Id}) {player.FirstName} {player.LastName}";

            if (maxLength.HasValue)
                return result.PadRight(maxLength.Value /*+ 7*/, ' ');

            return result;
        }

        public void Display(int currentRoundNumber, int tournamentMaxDisplayLenght)
        {
            Logger.WriteLine(GetRoundAsString(currentRoundNumber, tournamentMaxDisplayLenght));
        }
    }
}