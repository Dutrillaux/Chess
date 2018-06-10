using System.Collections.Generic;
using Chess.Core.Model;

namespace Chess.Core
{
    public interface ISetupTournament
    {
        List<Round> Rounds { get; set; }
        List<Player> Players { get; }
        void AddPlayer(string prenom, string nom, int age);

    }
}