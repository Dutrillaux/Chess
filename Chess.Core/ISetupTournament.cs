using System.Collections.Generic;
using Chess.Core.Model;

namespace Chess.Core
{
    public interface ISetupTournament : ITournament
    {
        void AddPlayer(string prenom, string nom, int age);
        void StartTournement();
    }
}