using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Connect_4
{
    public partial class gameForm : Form
    {
        private const int ROWS = 6;
        private const int COLS = 7;

        private bool gameOver = false;

        private int[,] board = new int[ROWS, COLS];
        private Button[] columnButtons = new Button[COLS];


        public gameForm()
        {
            InitializeComponent();
            InitializeBoard();
        }

        private void gameForm_Load(object sender, EventArgs e)
        {

        }
        private void InitializeBoard()
        {
            int buttonWidth = 50;
            int buttonHeight = 30;
            int spacing = 55;

            int totalWidth = COLS * spacing;
            int startX = (this.ClientSize.Width - totalWidth) / 2;
            int startY = 50; 

            for (int c = 0; c < COLS; c++)
            {
                Button btn = new Button();
                btn.Width = buttonWidth;
                btn.Height = buttonHeight;
                btn.Left = startX + c * spacing;
                btn.Top = startY;
                btn.Text = (c + 1).ToString();
                btn.Tag = c; 
                btn.Click += ColumnButton_Click;
                this.Controls.Add(btn);
                columnButtons[c] = btn;
            }
        }

        private void ColumnButton_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            int col = (int)btn.Tag;

            if (gameOver) return;

            for (int r = ROWS - 1; r >= 0; r--)
            {
                if (board[r, col] == 0)
                {
                    board[r, col] = 1; // 1 = player
                    break;
                }
            }
            DrawBoard();

            if (CheckWin(1))
            {
                gameOver = true;
                MessageBox.Show("Ai castigat!");
                return;
            }

            AIMoveRandom();
            DrawBoard();

            if (CheckWin(2))
            {
                gameOver = true;
                MessageBox.Show("Calculatorul a castigat!");
            }

            // TODO: aici vom apela AI-ul
        }
        private void DrawBoard()
        {
            this.Invalidate(); // redraw
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;

            int circleSize = 50;
            int spacing = 55;

            int totalWidth = COLS * spacing;
            int startX = (this.ClientSize.Width - totalWidth) / 2;
            int startY = 100; 

            for (int r = 0; r < ROWS; r++)
            {
                for (int c = 0; c < COLS; c++)
                {
                    Brush brush;
                    switch (board[r, c])
                    {
                        case 1: brush = Brushes.Red; break;   // player
                        case 2: brush = Brushes.Yellow; break; // AI
                        default: brush = Brushes.White; break;
                    }

                    int x = startX + c * spacing;
                    int y = startY + r * spacing;

                    g.FillEllipse(brush, x, y, circleSize, circleSize);
                    g.DrawEllipse(Pens.Black, x, y, circleSize, circleSize);
                }
            }
        }
        private bool CheckWin(int player)
        {
            // orizontal
            for (int r = 0; r < ROWS; r++)
                for (int c = 0; c < COLS - 3; c++)
                    if (Enumerable.Range(0, 4).All(i => board[r, c + i] == player))
                        return true;

            // vertical
            for (int r = 0; r < ROWS - 3; r++)
                for (int c = 0; c < COLS; c++)
                    if (Enumerable.Range(0, 4).All(i => board[r + i, c] == player))
                        return true;

            // diagonala \
            for (int r = 0; r < ROWS - 3; r++)
                for (int c = 0; c < COLS - 3; c++)
                    if (Enumerable.Range(0, 4).All(i => board[r + i, c + i] == player))
                        return true;

            // diagonala /
            for (int r = 3; r < ROWS; r++)
                for (int c = 0; c < COLS - 3; c++)
                    if (Enumerable.Range(0, 4).All(i => board[r - i, c + i] == player))
                        return true;

            return false;
        }
        private void AIMoveRandom()
        {
            Random rnd = new Random();
            List<int> validCols = new List<int>();

            for (int c = 0; c < COLS; c++)
                if (board[0, c] == 0)
                    validCols.Add(c);

            if (validCols.Count == 0) return;

            int col = validCols[rnd.Next(validCols.Count)];

            for (int r = ROWS - 1; r >= 0; r--)
                if (board[r, col] == 0)
                {
                    board[r, col] = 2;
                    break;
                }
        }


    }
}
