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
                        if (_board[column - 1, row] == 0)
                        {
                            _board[column - 1, row] = _board[column, row];
                            _board[column, row] = 0;
                            workDone = true;
                        }
                        else if (_board[column - 1, row] == _board[column, row] && !merged[column - 1, row] && !merged[column, row])
                        {
                            _board[column - 1, row] = (byte)(_board[column - 1, row] + 1);
                            _board[column, row] = 0;
                            merged[column - 1, row] = true;
                            workDone = true;
                        }
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
                        if (_board[column + 1, row] == 0)
                        {
                            _board[column + 1, row] = _board[column, row];
                            _board[column, row] = 0;
                            workDone = true;
                        }
                        else if (_board[column + 1, row] == _board[column, row] && !merged[column + 1, row] && !merged[column, row])
                        {
                            _board[column + 1, row] = (byte)(_board[column + 1, row] + 1);
                            _board[column, row] = 0;
                            merged[column + 1, row] = true;
                            workDone = true;
                        }
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
                        if (_board[column, row - 1] == 0)
                        {
                            _board[column, row - 1] = _board[column, row];
                            _board[column, row] = 0;
                            workDone = true;
                        }
                        else if (_board[column, row - 1] == _board[column, row] && !merged[column, row - 1] && !merged[column, row])
                        {
                            _board[column, row - 1] = (byte)(_board[column, row - 1] + 1);
                            _board[column, row] = 0;
                            merged[column, row - 1] = true;
                            workDone = true;
                        }
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
                        if (_board[column, row + 1] == 0)
                        {
                            _board[column, row + 1] = _board[column, row];
                            _board[column, row] = 0;
                            workDone = true;
                        }
                        else if (_board[column, row + 1] == _board[column, row] && !merged[column, row + 1] && !merged[column, row])
                        {
                            _board[column, row + 1] = (byte)(_board[column, row + 1] + 1);
                            _board[column, row] = 0;
                            merged[column, row + 1] = true;
                            workDone = true;
                        }
                    }
                }

                if (!workDone) break;

                _drawBoard();
            }
            AddCounter();
        }
    }
}
