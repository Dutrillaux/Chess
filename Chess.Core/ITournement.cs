using System.Collections.Generic;
using Chess.Core.Model;

namespace Chess.Core
{
    public interface ITournement
    {
        List<Player> Players { get; }
        List<Ronde> Rondes { get; }
        int ContestantNumber();
    }
}