using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
using System.Threading;

namespace Connect_4
{
    public partial class gameForm : Form
    {
        private const int ROWS = 6;
        private const int COLS = 7;
        private Minimax mm;
        private int maxDepth;

        private bool gameOver = false;

        private int[,] board = new int[ROWS, COLS];
        private Button[] columnButtons = new Button[COLS];
        
        private bool aiThinking = false;
        private int gameId = 0; 


        SoundPlayer calculatorSound = new SoundPlayer(@"Sounds\calculator_sound.wav");
        SoundPlayer playerSound = new SoundPlayer(@"Sounds\player_sound.wav");
        SoundPlayer gameOverSound = new SoundPlayer(@"Sounds\game_over.wav");
        SoundPlayer gameWinSound = new SoundPlayer(@"Sounds\game_win.wav");

        public gameForm()
        {
            InitializeComponent();
            this.FormClosing += GameForm_FormClosing;
            maxDepth = GameSettings.SearchDepth;
            mm = new Minimax(ROWS, COLS, maxDepth);
            InitializeBoard();
        }
        private void GameForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
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

        private async void ColumnButton_Click(object sender, EventArgs e)
        {
            if (gameOver || aiThinking) return;

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
            playerSound.Play();
            DrawBoard();

            if (mm.CheckWin(1, board))
            {
                gameOver = true;
                gameWinSound.Play();
                MessageBox.Show("Ai castigat!");
                return;
            }

            aiThinking = true;
            SetButtonsEnabled(false);
            int currentGameId = gameId;

            int bestCol = mm.GetBestMove(board);
            Random rnd = new Random();
            int delay = rnd.Next(500, 1500);
            await Task.Delay(delay);

            if (currentGameId != gameId)
            {
                aiThinking = false;
                return;
            }


            mm.MakeMove(bestCol, 2, board); // 2 = AI
            calculatorSound.Play();
            DrawBoard();
            aiThinking = false;
            SetButtonsEnabled(true);

            if (mm.CheckWin(2, board))
            {
                gameOver = true;
                gameOverSound.Play();
                MessageBox.Show("Ai pierdut!");
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

        private void btnRestart_Click(object sender, EventArgs e)
        {
            gameId++;
            gameOver = false;
            SetButtonsEnabled(true);
            board = new int[ROWS, COLS];
            DrawBoard();
        }

        private void btnMenu_Click(object sender, EventArgs e)
        {
            gameId++;
            this.Hide();
            mainMenuForm menu = new mainMenuForm();
            menu.Show();
        }
        private void SetButtonsEnabled(bool enabled)
        {
            foreach (var btn in columnButtons)
                btn.Enabled = enabled;
        }

    }
}
