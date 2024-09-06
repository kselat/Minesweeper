namespace Minesweeper.Models
{
    public class NewGameRequest
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public int Mines_count { get; set; }

        public NewGameRequest(int width, int height, int mines_count)
        {
            Width = width;
            Height = height;
            Mines_count = mines_count;
        }
    }
}
