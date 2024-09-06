namespace Minesweeper.Models
{
    public class Cell
    {
        public bool IsOpen { get; set; }
        public bool IsMine { get; set; }
        public int CountMine { get; set; }
    }
}
