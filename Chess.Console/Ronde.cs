using System.Collections.Generic;
using System.Text;

namespace Chess.Console
{
    public class Ronde
    {
        public List<Game> Games = new List<Game>();

        public override string ToString()
        {
            var result = new StringBuilder();

            foreach (var game in Games)
            {
                result.Append(game.WhiteContestant);
                result.Append("-");
                result.Append(game.BlackContestant);

                result.Append("  |  ");
            }

            return result.ToString();
        }
    }
}