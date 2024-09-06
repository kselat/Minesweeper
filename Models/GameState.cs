namespace Minesweeper.Models
{
    public class GameState
    {
        public string GameId { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int Mines_count { get; set; }
        public bool Completed { get; set; }
        public string[][] Field { get; set; }
        public string Error { get; set; }

        public GameState(string gameId, int width, int height, int minesCount, bool completed, string[][] field)
        {
            GameId = gameId;
            Width = width;
            Height = height;
            Mines_count = minesCount;
            Completed = completed;
            Field = field;
            Error = string.Empty;
        }

        public GameState(string error)
        {
            Error = error;
        }
    }
}
