using Chess.Core.Model;

namespace Chess.Core.Command
{
    public interface ICommandGameResult
    {
        void SetGameResultCommand(GameResult gameResult);
    }
}