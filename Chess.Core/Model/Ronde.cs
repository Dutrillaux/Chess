using System.Collections.Generic;
using System.Text;

namespace Chess.Core.Model
{
    public class Ronde
    {
        public List<Game> Games = new List<Game>();

        public override string ToString()
        {
            return GetRondeAsString(null, null);
        }

        private string GetRondeAsString(int? currentRondeNumber, int? maxLength)
        {
            var result = new StringBuilder();

            var rondeDescrition = currentRondeNumber.HasValue 
                ? $"Ronde {currentRondeNumber.Value} || " 
                : "Ronde || ";
            result.Append(rondeDescrition);

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
                case GameResult.Null:
                    return " [.5]-[.5] ";
                default:
                    return "     -     ";
            }
        }

        private static string FormatContestant(Player player, int? maxLength)
        {
            var result = $"({player.Id}) {player.Prenom} {player.Nom}";

            if (maxLength.HasValue)
                return result.PadRight(maxLength.Value + 7, ' ');

            return result;
        }

        public void Display(int currentRondeNumber, int tournoiMaxDisplayLenght)
        {
            System.Console.WriteLine(GetRondeAsString(currentRondeNumber, tournoiMaxDisplayLenght));
        }
    }
}