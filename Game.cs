using System;

namespace _2048Game
{
    public class Game
    {
        private readonly byte[,] _board = new byte[4, 4];
        private readonly Action _drawBoard;
        private readonly Random _rnd;

        public Game(Action drawBoard)
        {
            _drawBoard = drawBoard;
            _rnd = new Random();
        }

        public void NewGame()
        {
            for (int row = 0; row < 4; row++)
            {
                for (int column = 0; column < 4; column++)
                {
                    _board[column, row] = 0;
                }
            }
            Score = 0;
            AddCounter();
        }

        private void AddCounter()
        {
            var value = (byte)_rnd.Next(1, 3);  //Gives 1,2  ie 2 or 4

            int column;
            int row;
            do
            {
                column = _rnd.Next(0, 4);
                row = _rnd.Next(0, 4);

            } while (_board[column, row] != 0);

            _board[column, row] = value;

            _drawBoard();
        }

        public byte Cell(int column, int row) => _board[column, row];

        public int Score { get; private set; }

        public void MoveLeft()
        {
            var merged = new bool[4, 4];

            for (int iteration = 0; iteration < 3; iteration++)
            {
                var workDone = false;
                for (int column = 1; column < 4; column++)
                {
                    for (int row = 0; row < 4; row++)
                    {
                        workDone |= TryToSlide(column, row, column - 1, row, merged);
                    }
                }

                if (!workDone) break;

                _drawBoard();
            }
            AddCounter();
        }

        public void MoveRight()
        {
            var merged = new bool[4, 4];

            for (int iteration = 0; iteration < 3; iteration++)
            {
                var workDone = false;
                for (int column = 2; column >= 0; column--)
                {
                    for (int row = 0; row < 4; row++)
                    {
                        workDone |= TryToSlide(column, row, column + 1, row, merged);
                    }
                }

                if (!workDone) break;

                _drawBoard();
            }
            AddCounter();
        }

        public void MoveUp()
        {
            var merged = new bool[4, 4];

            for (int iteration = 0; iteration < 3; iteration++)
            {
                var workDone = false;
                for (int row = 1; row < 4; row++)
                {
                    for (int column = 0; column < 4; column++)
                    {
                        workDone |= TryToSlide(column, row, column, row - 1, merged);
                    }
                }

                if (!workDone) break;

                _drawBoard();
            }
            AddCounter();
        }

        public void MoveDown()
        {
            var merged = new bool[4, 4];

            for (int iteration = 0; iteration < 3; iteration++)
            {
                var workDone = false;
                for (int row = 2; row >= 0; row--)
                {
                    for (int column = 0; column < 4; column++)
                    {
                        workDone |= TryToSlide(column, row, column, row + 1, merged);
                    }
                }

                if (!workDone) break;

                _drawBoard();
            }
            AddCounter();
        }

        private bool TryToSlide(int fromColumn, int fromRow, int toColumn, int toRow, bool[,] merged)
        {
            if (_board[toColumn, toRow] == 0)
            {
                _board[toColumn, toRow] = _board[fromColumn, fromRow];
                _board[fromColumn, fromRow] = 0;
                return true;
            }
            else if (_board[toColumn, toRow] == _board[fromColumn, fromRow] && !merged[toColumn, toRow] && !merged[fromColumn, fromRow])
            {
                _board[toColumn, toRow] = (byte)(_board[toColumn, toRow] + 1);
                _board[fromColumn, fromRow] = 0;
                merged[toColumn, toRow] = true;
                Score += (int)Math.Pow(2, _board[toColumn, toRow]);
                return true;
            }
            return false;
        }
    }
}
