using System.Collections.Generic;
using Chess.Core.Model;

namespace Chess.Core
{
    public interface ITournament
    {
        List<Player> Players { get; }
        List<Round> Rounds { get; }
        int ContestantNumber();
    }
}