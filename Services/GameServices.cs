using Minesweeper.Models;
using System;

namespace Minesweeper.Services
{
    public class GameServices : IGameServices
    {
        private Dictionary<string, Field> _games;

        private enum StateVisible
        {
            NotMine,
            Mine,
            EndGame
        }

        public GameServices()
        {
            _games = new Dictionary<string, Field>();
        }

        public GameState NewGame(NewGameRequest newGameReq)
        {
            GameState result;

            if (newGameReq.Width <= 30 && newGameReq.Height <= 30)
            {
                if (newGameReq.Mines_count < newGameReq.Width * newGameReq.Height)
                {
                    string guid = Guid.NewGuid().ToString();
                    Field field = GeneratorField(newGameReq.Width, newGameReq.Height, newGameReq.Mines_count);
                    _games.Add(guid, field);
                    result = new GameState(guid.ToString(), newGameReq.Width, newGameReq.Height,
                        newGameReq.Mines_count, false, ConvertFieldToArray(field, StateVisible.NotMine));
                }
                else
                    result = new GameState(string.Format("Количество мин должно быть меньше: {0}", newGameReq.Width * newGameReq.Height));
            }
            else
                result = new GameState("Ширина и высота не должна иметь значение больше 30");

            return result;
        }

        public GameState Turn(GameTurnRequest gameTurnReq)
        {
            GameState result = null;
            Field field = null;

            if(_games.TryGetValue(gameTurnReq.Game_id, out field))
            {
                if (!field.IsCompleted) 
                {
                    if(!IsOpen(gameTurnReq.Row, gameTurnReq.Col, field))
                    {
                        StateVisible state = StateVisible.NotMine;

                        if (IsMine(gameTurnReq.Row, gameTurnReq.Col, field))
                        {
                            EndGame(field);
                            state = StateVisible.Mine;
                            field.IsCompleted = true;
                        }
                        else if (IsCompleted(field))
                        {
                            state = StateVisible.EndGame;
                            field.IsCompleted = true;
                        }

                        result = new GameState(gameTurnReq.Game_id, field.Width, field.Height,
                            field.MinesCount, field.IsCompleted, ConvertFieldToArray(field, state));
                    }
                    else
                        result = new GameState("Ячейка уже проверена");
                }
                else
                    result = new GameState("Игра завершена");
            }
            else
                result = new GameState("Игры с данным game_id не найдено");

            return result;
        }

        private string[][] ConvertFieldToArray(Field field, StateVisible state)
        {
            string[][] result = new string[field.Width][];

            for (int x = 0; x < field.Width; x++)
            {
                result[x] = new string[field.Height];
                for (int y = 0; y < field.Height; y++)
                {
                    Cell cell = field.Cells[x, y];
                    switch (state)
                    {
                        case StateVisible.NotMine:
                            result[x][y] = cell.IsOpen ? cell.CountMine.ToString() : " ";
                            break;
                        case StateVisible.Mine:
                            result[x][y] = cell.IsMine ? "X" : cell.CountMine.ToString();
                            break;
                        case StateVisible.EndGame:
                            result[x][y] = cell.IsMine ? "M" : cell.CountMine.ToString();
                            break;
                    }
                }
            }

            return result;
        }

        private Field GeneratorField(int width, int height, int minesCount)
        {
            Field field = new Field(width, height, minesCount);
            Random r = new Random();

            for (int i = 0; i < minesCount; i++)
            {
                int x = r.Next(width);
                int y = r.Next(height);
                if (field.Cells[x, y].IsMine)
                {
                    i--;
                    continue;
                }
                field.Cells[x, y].IsMine = true;
            }

            return field;
        }

        private bool IsMine(int row, int col, Field field)
        {
            if (field.Cells[row, col].IsMine)
                return true;

            OpenCell(row, col, field);

            return false;
        }

        private void OpenCell(int row, int col, Field field)
        {
            Cell cell = field.Cells[row, col];
            cell.IsOpen = true;
            cell.CountMine = MinesAround(row, col, field);

            if (cell.CountMine == 0)
                for (int x = 0; x < field.Width; x++)
                    for (int y = 0; y < field.Height; y++)
                        if (Math.Abs(row - x) <= 1 && Math.Abs(y - col) <= 1 && field.Cells[x, y].IsOpen == false)
                            OpenCell(x, y, field);
        }

        private int MinesAround(int row, int col, Field field)
        {
            int countMine = 0;
            for (int x = 0; x < field.Width; x++)
            {
                for (int y = 0; y < field.Height; y++)
                {
                    if (Math.Abs(row - x) <= 1 && Math.Abs(y - col) <= 1 && field.Cells[x, y].IsMine)
                        countMine++;
                }
            }
            return countMine;
        }

        private bool IsCompleted (Field field)
        {
            for (int x = 0; x < field.Width; x++)
            {
                for (int y = 0; y < field.Height; y++)
                {
                    Cell cell = field.Cells[x, y];
                    if (!cell.IsOpen && !cell.IsMine)
                        return false;
                }
            }

            return true;
        }

        private bool IsOpen(int row, int col, Field field)
        {
            return field.Cells[row, col].IsOpen;
        }

        private void EndGame(Field field)
        {
            for (int x = 0; x < field.Width; x++)
            {
                for (int y = 0; y < field.Height; y++)
                {
                    Cell cell = field.Cells[x, y];
                    if (!cell.IsOpen && !cell.IsMine)
                    {
                        cell.IsOpen = true;
                        cell.CountMine = MinesAround(x, y, field);
                    }
                }
            }
        }
    }
}
