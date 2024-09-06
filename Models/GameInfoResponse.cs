namespace Minesweeper.Models
{
    public class GameInfoResponse
    {
        public string Game_id { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int Mines_count { get; set; }
        public bool Completed { get; set; }
        public string[][] Field { get; set; }

        public GameInfoResponse(GameState gameState) 
        {
            Game_id = gameState.GameId;
            Width = gameState.Width;
            Height = gameState.Height;
            Mines_count = gameState.Mines_count;
            Completed = gameState.Completed;
            Field = gameState.Field;
        }
    }
}
