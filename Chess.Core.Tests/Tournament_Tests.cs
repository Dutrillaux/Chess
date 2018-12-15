using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Chess.Core.Tests
{
    [TestClass]
    public class Tournament_Tests
    {
        [TestMethod]
        public void Adding_Players()
        {
            ISetupTournament tournament = new TournamentDeBerger();
            Assert.AreEqual(0, tournament.Players.Count);

            tournament.AddPlayer("John", "Doe", 25);
            Assert.AreEqual(1, tournament.Players.Count);

            tournament.AddPlayer("Johnny", "Good", 25);
            Assert.AreEqual(2, tournament.Players.Count);
        }
    }
}
