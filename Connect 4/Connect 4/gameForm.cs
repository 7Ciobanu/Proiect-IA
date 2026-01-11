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

        private int maxDepth;

        private bool gameOver = false;

        private int[,] board = new int[ROWS, COLS];
        private Button[] columnButtons = new Button[COLS];


        public gameForm()
        {
            InitializeComponent();
            maxDepth = GameSettings.SearchDepth;
            InitializeBoard();
        }

        private void gameForm_Load(object sender, EventArgs e)
        {
            //hallo
        }
        private void InitializeBoard()
        {
            int buttonWidth = 75;
            int buttonHeight = 45;
            int spacing = 80;

            int totalWidth = COLS * spacing;
            int startX = (this.ClientSize.Width - totalWidth) / 2;
            int startY = 350;

            for (int c = 0; c < COLS; c++)
            {
                Button btn = new Button();
                btn.Width = buttonWidth;
                btn.Height = buttonHeight;
                btn.Left = startX + c * spacing;
                btn.Top = startY;
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

            int bestCol = GetBestMove();
            MakeMove(bestCol, 2); // 2 = AI
            DrawBoard();

            if (CheckWin(2))
            {
                gameOver = true;
                MessageBox.Show("Calculatorul a castigat!");
            }

        }
        private void DrawBoard()
        {
            this.Invalidate(); // redraw
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;

            int circleSize = 75;
            int spacing = 80;

            int totalWidth = COLS * spacing;
            int startX = (this.ClientSize.Width - totalWidth) / 2;
            int startY = 400;

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

        private void MakeMove(int col, int player)
        {
            for (int r = ROWS - 1; r >= 0; r--)
                if (board[r, col] == 0)
                {
                    board[r, col] = player;
                    break;
                }
        }
        private void UndoMove(int col)
        {
            for (int r = 0; r < ROWS; r++)
                if (board[r, col] != 0)
                {
                    board[r, col] = 0;
                    break;
                }
        }
        private List<int> GetValidMoves()
        {
            List<int> moves = new List<int>();
            for (int c = 0; c < COLS; c++)
                if (board[0, c] == 0)
                    moves.Add(c);
            return moves;
        }

        private int GetBestMove()
        {
            int bestScore = int.MinValue;
            int bestCol = 0;

            foreach (int col in GetValidMoves())
            {
                MakeMove(col, 2);
                int score = Minimax(maxDepth - 1, int.MinValue, int.MaxValue, false);
                UndoMove(col);

                if (score > bestScore)
                {
                    bestScore = score;
                    bestCol = col;
                }
            }
            return bestCol;
        }

        private int Minimax(int depth, int alpha, int beta, bool maximizing)
        {
            if (depth == 0 || CheckWin(2) || CheckWin(1) || GetValidMoves().Count == 0)
                return EvaluateBoard();

            if (maximizing)
            {
                int maxEval = int.MinValue;
                foreach (int col in GetValidMoves())
                {
                    MakeMove(col, 2);
                    int eval = Minimax(depth - 1, alpha, beta, false);
                    UndoMove(col);

                    maxEval = Math.Max(maxEval, eval);
                    alpha = Math.Max(alpha, eval);
                    if (beta <= alpha)
                        break; // retezare alpha-beta
                }
                return maxEval;
            }
            else
            {
                int minEval = int.MaxValue;
                foreach (int col in GetValidMoves())
                {
                    MakeMove(col, 1);
                    int eval = Minimax(depth - 1, alpha, beta, true);
                    UndoMove(col);

                    minEval = Math.Min(minEval, eval);
                    beta = Math.Min(beta, eval);
                    if (beta <= alpha)
                        break;
                }
                return minEval;
            }
        }
        private int EvaluateBoard()
        {
            if (CheckWin(2)) return 100000;
            if (CheckWin(1)) return -100000;

            int score = 0;
            score += CountPatterns(2, 3) * 100;
            score += CountPatterns(2, 2) * 10;
            score -= CountPatterns(1, 3) * 100;
            score -= CountPatterns(1, 2) * 10;

            return score;
        }

        private int CountPatterns(int player, int count)
        {
            int patterns = 0;

            for (int r = 0; r < ROWS; r++)
                for (int c = 0; c < COLS - 3; c++)
                    patterns += CheckLine(player, count, r, c, 0, 1);

            for (int r = 0; r < ROWS - 3; r++)
                for (int c = 0; c < COLS; c++)
                    patterns += CheckLine(player, count, r, c, 1, 0);

            for (int r = 0; r < ROWS - 3; r++)
                for (int c = 0; c < COLS - 3; c++)
                    patterns += CheckLine(player, count, r, c, 1, 1);

            for (int r = 3; r < ROWS; r++)
                for (int c = 0; c < COLS - 3; c++)
                    patterns += CheckLine(player, count, r, c, -1, 1);

            return patterns;
        }

        private int CheckLine(int player, int count, int r, int c, int dr, int dc)
        {
            int playerCount = 0;
            int emptyCount = 0;

            for (int i = 0; i < 4; i++)
            {
                int cell = board[r + dr * i, c + dc * i];
                if (cell == player) playerCount++;
                else if (cell == 0) emptyCount++;
            }

            return (playerCount == count && emptyCount == 4 - count) ? 1 : 0;
        }



    }
}
