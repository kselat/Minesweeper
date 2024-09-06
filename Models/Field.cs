namespace Minesweeper.Models
{
    public class Field
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public int MinesCount { get; set; }
        public Cell[,] Cells { get; set; }
        public bool IsCompleted { get; set; }

        public Field(int width, int height, int minesCount)
        {
            Width = width;
            Height = height;
            MinesCount = minesCount;
            Cells = new Cell[Width, Height];
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Cells[x, y] = new Cell();
                }
            }
        }
    }
}
