using Minesweeper.Models;

namespace Minesweeper.Services
{
    public interface IGameServices
    {
        public GameState NewGame(NewGameRequest newGameReq);

        public GameState Turn(GameTurnRequest gameTurnReq);
    }
}
