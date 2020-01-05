using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace _2048Game
{
    public partial class Form1 : Form
    {
        private Font _scoreFont;
        private Font[] _fonts;
        private Color[] _colours;
        private Game _game;
        private const int _borderSize = 20;
        private const int _cellSize = 150;

        private const int _totalWidth = (4 * _cellSize) + (5 * _borderSize);
        private const int _totalHeight = (4 * _cellSize) + (5 * _borderSize);

        public Form1()
        {
            InitializeComponent();

            this.Text = "2048";

            _scoreFont = new System.Drawing.Font("Segoe UI", 96F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

            _fonts = new[]
            {
                 new System.Drawing.Font("Segoe UI", 96F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0))),
                 new System.Drawing.Font("Segoe UI", 80F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0))),
                 new System.Drawing.Font("Segoe UI", 65F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0))),
                 new System.Drawing.Font("Segoe UI", 50F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0))),
                 new System.Drawing.Font("Segoe UI", 40F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0))),
            };

            var alpha = 200;
            _colours = new[]
            {
                Color.FromArgb(alpha, 2, 63, 165),
                Color.FromArgb(alpha, 125, 135, 185),
                Color.FromArgb(alpha, 190, 193, 212),
                Color.FromArgb(alpha, 214, 188, 192),
                Color.FromArgb(alpha, 187, 119, 132),
                Color.FromArgb(alpha, 142, 6, 59),
                Color.FromArgb(alpha, 74, 111, 227),
                Color.FromArgb(alpha, 133, 149, 225),
                Color.FromArgb(alpha, 181, 187, 227),
                Color.FromArgb(alpha, 230, 175, 185),
                Color.FromArgb(alpha, 224, 123, 145),
                Color.FromArgb(alpha, 211, 63, 106),
                Color.FromArgb(alpha, 17, 198, 56),
                Color.FromArgb(alpha, 141, 213, 147),
                Color.FromArgb(alpha, 198, 222, 199),
                Color.FromArgb(alpha, 234, 211, 198),
                Color.FromArgb(alpha, 240, 185, 141),
                Color.FromArgb(alpha, 239, 151, 8),
                Color.FromArgb(alpha, 15, 207, 192),
                Color.FromArgb(alpha, 156, 222, 214),
                Color.FromArgb(alpha, 213, 234, 231),
                Color.FromArgb(alpha, 243, 225, 235),
                Color.FromArgb(alpha, 246, 196, 225),
                Color.FromArgb(alpha, 247, 156, 212),
            };

            _game = new Game(DrawBoard);
            _game.NewGame();
        }

        private void DrawBoard()
        {
            var currentContext = BufferedGraphicsManager.Current;
            using (var myBuffer = currentContext.Allocate(this.CreateGraphics(), this.DisplayRectangle))
            {
                var g = myBuffer.Graphics;
                DrawBoard(g);
                myBuffer.Render();
            }
        }

        private void DrawBoard(Graphics g)
        {
            g.DrawImage(this.BackgroundImage, this.ClientRectangle);

            g.TranslateTransform((ClientSize.Width - _totalWidth) / 2, (ClientSize.Height - _totalHeight) / 2);

            for (int column = 0; column < 4; column++)
            {
                for (int row = 0; row < 4; row++)
                {
                    var x = _borderSize + (column * (_borderSize + _cellSize));
                    var y = _borderSize + (row * (_borderSize + _cellSize));

                    var tileValue = _game.Cell(column, row);
                    var background = new SolidBrush(_colours[tileValue]);
                    g.FillRectangle(background, x, y, _cellSize, _cellSize);

                    if (tileValue != 0)
                    {
                        var text = Math.Pow(2, tileValue).ToString();
                        var font = _fonts[text.Length - 1];
                        var space = g.MeasureString(text, font);
                        g.DrawString(text, font, Brushes.White, x + ((_cellSize - space.Width) / 2), y + ((_cellSize - space.Height) / 2));
                    }
                }
            }

            Thread.Sleep(100);

            g.DrawString($"Score : {_game.Score}", _scoreFont, new SolidBrush(Color.FromArgb(187, 119, 132)), 0, -150);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    _game.MoveLeft();
                    break;

                case Keys.Right:
                    _game.MoveRight();
                    break;

                case Keys.Up:
                    _game.MoveUp();
                    break;

                case Keys.Down:
                    _game.MoveDown();
                    break;

                default:
                    break;
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            DrawBoard();
        }
    }
}
