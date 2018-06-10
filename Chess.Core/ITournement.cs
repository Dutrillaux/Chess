using System.Collections.Generic;
using Chess.Core.Model;

namespace Chess.Core
{
    public interface ITournement
    {
        List<Player> Players { get; }
        List<Round> Rounds { get; }
        int ContestantNumber();
    }
}