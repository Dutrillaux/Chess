namespace Chess.Core.Model
{
    public static class GameResultHelper
    {
        public static void GetResult(GameResult gameResult, out decimal whiteContestantPoints,
            out decimal blackContestantPoints)
        {
            whiteContestantPoints = 0m;
            blackContestantPoints = 0m;
            switch (gameResult)
            {
                case GameResult.WinnerBlack:
                    whiteContestantPoints = 0m;
                    blackContestantPoints = 1m;
                    break;
                case GameResult.WinnerWhite:
                    whiteContestantPoints = 1m;
                    blackContestantPoints = 0m;
                    break;
                case GameResult.NoWinnerPat:
                    whiteContestantPoints = 0.5m;
                    blackContestantPoints = 0.5m;
                    break;
            }
        }
    }
}