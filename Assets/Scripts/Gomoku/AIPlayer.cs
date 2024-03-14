using UnityEngine;
using System.Collections.Generic;

public enum Difficulty
{
    Easy,
    Medium,
    Hard
}

public class AIPlayer
{
    //     private int[,] board;
    //     private int boardSize;
    //     private Vector2Int lastPlayerMove;
    //     private int aiNumber;
    //     private int playerNumber;

    //     public AIPlayer(int boardSize, int aiNumber, int playerNumber)
    //     {
    //         this.boardSize = boardSize;
    //         this.aiNumber = aiNumber;
    //         this.playerNumber = playerNumber;
    //         this.board = new int[boardSize, boardSize];
    //     }

    //     public void UpdateBoard(int[,] currentBoard)
    //     {
    //         this.board = currentBoard;
    //     }

    //     public void SetLastPlayerMove(Vector2Int move)
    //     {
    //         this.lastPlayerMove = move;
    //     }

    //     public Vector2Int GetMove()
    //     {
    //         Vector2Int move = FindWinningMove(board, aiNumber);
    //         if (move.x != -1)
    //             return move;

    //         move = FindBlockingMove(board, playerNumber);
    //         if (move.x != -1)
    //             return move;

    //         move = FindStrategicMove(board, aiNumber);
    //         if (move.x != -1)
    //             return move;

    //         return FindRandomMoveAroundLastPlayerMove();
    //     }

    //     private Vector2Int FindWinningMove(int[,] board, int playerNumber)
    //     {
    //         for (int x = 0; x < board.GetLength(0); x++)
    //         {
    //             for (int y = 0; y < board.GetLength(1); y++)
    //             {
    //                 if (board[x, y] == 0)
    //                 { // Check only empty spots
    //                     board[x, y] = playerNumber; // Temporarily make the move
    //                     if (CheckWin(new Vector2Int(x, y), playerNumber))
    //                     {
    //                         board[x, y] = 0; // Undo the move
    //                         return new Vector2Int(x, y);
    //                     }
    //                     board[x, y] = 0; // Undo the move
    //                 }
    //             }
    //         }
    //         return new Vector2Int(-1, -1); // No winning move found
    //     }

    //     private bool CheckWin(Vector2Int move, int playerNumber)
    //     {
    //         // Directions to check: horizontal, vertical, diagonal (\ and /)
    //         int[][] directions = new int[][]
    //         {
    //             new int[] { 1, 0 }, // Horizontal
    //             new int[] { 0, 1 }, // Vertical
    //             new int[] { 1, 1 }, // Diagonal \
    //             new int[] { 1, -1 } // Diagonal /
    //         };

    //         foreach (var dir in directions)
    //         {
    //             int count = 1; // Start with the current piece

    //             // Check in the positive direction
    //             count += CountDirection(move, dir[0], dir[1], playerNumber);
    //             // Check in the negative direction
    //             count += CountDirection(move, -dir[0], -dir[1], playerNumber);

    //             if (count >= 5)
    //             { // Need at least 5 for a win
    //                 return true;
    //             }
    //         }

    //         return false; // No win found
    //     }

    //     private int CountDirection(Vector2Int start, int dx, int dy, int playerNumber)
    //     {
    //         int count = 0;
    //         int x = start.x;
    //         int y = start.y;

    //         // Move in the direction specified by dx and dy
    //         while (true)
    //         {
    //             x += dx;
    //             y += dy;

    //             // Check bounds
    //             if (x < 0 || y < 0 || x >= boardSize || y >= boardSize)
    //             {
    //                 break;
    //             }

    //             // Check if the piece belongs to the player
    //             if (board[x, y] == playerNumber)
    //             {
    //                 count++;
    //             }
    //             else
    //             {
    //                 break;
    //             }
    //         }

    //         return count;
    //     }

    //     private Vector2Int FindBlockingMove(int[,] board, int opponentNumber)
    //     {
    //         return FindWinningMove(board, opponentNumber);
    //     }

    //     private Vector2Int FindStrategicMove(int[,] board, int playerNumber)
    //     {
    //         int bestScore = -1;
    //         Vector2Int bestMove = new Vector2Int(-1, -1);

    //         for (int x = 0; x < board.GetLength(0); x++)
    //         {
    //             for (int y = 0; y < board.GetLength(1); y++)
    //             {
    //                 if (board[x, y] == 0)
    //                 { // Empty spot
    //                     int score = ScoreMove(board, x, y, playerNumber);
    //                     if (score > bestScore)
    //                     {
    //                         bestScore = score;
    //                         bestMove = new Vector2Int(x, y);
    //                     }
    //                 }
    //             }
    //         }

    //         return bestMove; // This might still return (-1, -1) if no strategic move is found
    //     }

    //     private Vector2Int FindRandomMoveAroundLastPlayerMove()
    //     {
    //         List<Vector2Int> availableMoves = new List<Vector2Int>();
    //         int searchRadius = 2; // Can be adjusted

    //         // Only check within a certain radius around the last player move
    //         for (
    //             int x = Mathf.Max(lastPlayerMove.x - searchRadius, 0);
    //             x <= Mathf.Min(lastPlayerMove.x + searchRadius, boardSize - 1);
    //             x++
    //         )
    //         {
    //             for (
    //                 int y = Mathf.Max(lastPlayerMove.y - searchRadius, 0);
    //                 y <= Mathf.Min(lastPlayerMove.y + searchRadius, boardSize - 1);
    //                 y++
    //             )
    //             {
    //                 if (board[x, y] == 0)
    //                 {
    //                     availableMoves.Add(new Vector2Int(x, y));
    //                 }
    //             }
    //         }

    //         // If no move found within the radius, consider any available move
    //         if (availableMoves.Count == 0)
    //         {
    //             for (int x = 0; x < boardSize; x++)
    //             {
    //                 for (int y = 0; y < boardSize; y++)
    //                 {
    //                     if (board[x, y] == 0)
    //                     {
    //                         availableMoves.Add(new Vector2Int(x, y));
    //                     }
    //                 }
    //             }
    //         }

    //         if (availableMoves.Count > 0)
    //         {
    //             int index = Random.Range(0, availableMoves.Count);
    //             return availableMoves[index];
    //         }

    //         return new Vector2Int(-1, -1); // If no available move
    // }
}
