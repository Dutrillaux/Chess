using System.Collections.Generic;
using System.Text;

namespace Chess.Core.Model
{
    public class Ronde
    {
        public List<Game> Games = new List<Game>();

        public override string ToString()
        {
            return GetRondeAsString();
        }

        private string GetRondeAsString()
        {
            return GetRondeAsString(null, null);
        }
        private string GetRondeAsString(int? currentRondeNumber, int? maxLength)
        {
            var result = new StringBuilder();

            if(currentRondeNumber.HasValue)
                result.Append($"Ronde {currentRondeNumber.Value} || ");

            foreach (var game in Games)
            {
                result.Append(FormatContestant(game.WhiteContestant, maxLength));
                result.Append(" - ");
                result.Append(FormatContestant(game.BlackContestant, maxLength));

                result.Append(" | ");
            }

            return result.ToString();
        }

        private static string FormatContestant(Player player, int? maxLength)
        {
            string result;

            if (player == null)
                result = "personne";
            else
                result = $"({player.Id}) {player.Prenom} {player.Nom}";

            if (maxLength.HasValue)
                return result.PadRight(maxLength.Value + 5, ' ');

            return result;
        }

        public void Display(int currentRondeNumber, int tournoiMaxDisplayLenght)
        {
            System.Console.WriteLine(GetRondeAsString(currentRondeNumber, tournoiMaxDisplayLenght));
        }
    }
}