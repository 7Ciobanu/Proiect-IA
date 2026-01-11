using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect_4
{
    public class Minimax
    {
        private int ROWS;
        private int COLS;
        private int maxDepth;

        public Minimax(int Rows, int Cols,int maxdepth)
        {
            ROWS=Rows; COLS=Cols; maxDepth=maxdepth;
        }
        public bool CheckWin(int player, int[,]board)
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
        public void MakeMove(int col, int player, int[,] board)
        {
            for (int r = ROWS - 1; r >= 0; r--)
                if (board[r, col] == 0)
                {
                    board[r, col] = player;
                    break;
                }
        }
        public void UndoMove(int col, int[,] board)
        {
            for (int r = 0; r < ROWS; r++)
                if (board[r, col] != 0)
                {
                    board[r, col] = 0;
                    break;
                }
        }
        public List<int> GetValidMoves(int[,] board)
        {
            List<int> moves = new List<int>();
            for (int c = 0; c < COLS; c++)
                if (board[0, c] == 0)
                    moves.Add(c);
            return moves;
        }

        public int GetBestMove(int[,] board)
        {
            int bestScore = int.MinValue;
            int bestCol = 0;

            foreach (int col in GetValidMoves(board))
            {
                MakeMove(col, 2,board);
                int score = MiniMax(maxDepth - 1, int.MinValue, int.MaxValue, false, board);
                UndoMove(col,board);

                if (score > bestScore)
                {
                    bestScore = score;
                    bestCol = col;
                }
            }
            return bestCol;
        }

        public int MiniMax(int depth, int alpha, int beta, bool maximizing, int[,] board)
        {
            if (depth == 0 || CheckWin(2,board) || CheckWin(1,board) || GetValidMoves(board).Count == 0)
                return EvaluateBoard(board);

            if (maximizing)
            {
                int maxEval = int.MinValue;
                foreach (int col in GetValidMoves(board))
                {
                    MakeMove(col, 2,board);
                    int eval = MiniMax(depth - 1, alpha, beta, false,board);
                    UndoMove(col,board);

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
                foreach (int col in GetValidMoves(board))
                {
                    MakeMove(col, 1,board);
                    int eval = MiniMax(depth - 1, alpha, beta, true, board);
                    UndoMove(col,board);

                    minEval = Math.Min(minEval, eval);
                    beta = Math.Min(beta, eval);
                    if (beta <= alpha)
                        break;
                }
                return minEval;
            }
        }
        public int EvaluateBoard(int[,] board)
        {
            if (CheckWin(2,board)) return 100000;
            if (CheckWin(1,board)) return -100000;

            int score = 0;
            score += CountPatterns(2, 3,board) * 100;
            score += CountPatterns(2, 2, board) * 10;
            score -= CountPatterns(1, 3, board) * 100;
            score -= CountPatterns(1, 2, board) * 10;

            return score;
        }

        public int CountPatterns(int player, int count, int[,] board)
        {
            int patterns = 0;

            for (int r = 0; r < ROWS; r++)
                for (int c = 0; c < COLS - 3; c++)
                    patterns += CheckLine(player, count, r, c, 0, 1, board);

            for (int r = 0; r < ROWS - 3; r++)
                for (int c = 0; c < COLS; c++)
                    patterns += CheckLine(player, count, r, c, 1, 0, board);

            for (int r = 0; r < ROWS - 3; r++)
                for (int c = 0; c < COLS - 3; c++)
                    patterns += CheckLine(player, count, r, c, 1, 1, board);

            for (int r = 3; r < ROWS; r++)
                for (int c = 0; c < COLS - 3; c++)
                    patterns += CheckLine(player, count, r, c, -1, 1, board);

            return patterns;
        }

        public int CheckLine(int player, int count, int r, int c, int dr, int dc, int[,] board)
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
