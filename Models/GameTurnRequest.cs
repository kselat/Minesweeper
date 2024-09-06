namespace Minesweeper.Models
{
    public class GameTurnRequest
    {
        public string Game_id { get; set; }
        public int Col { get; set; }
        public int Row { get; set; }
    }
}
