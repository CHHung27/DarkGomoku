using UnityEngine;

public class AIPlayer
{
    private int boardSize;
    private Difficulty difficulty;

    public AIPlayer(int boardSize, Difficulty difficulty = Difficulty.Easy)
    {
        this.boardSize = boardSize;
        this.difficulty = difficulty;
    }

    public enum Difficulty
    {
        Easy,
        Medium,
        Hard
    }

    public Vector2Int GetMove(int[,] board)
    {
        switch (difficulty)
        {
            case Difficulty.Easy:
                return FindEasyMove(board);
            case Difficulty.Medium:
                // Implement Medium Difficulty AI
                break;
            case Difficulty.Hard:
                // Implement Hard Difficulty AI
                break;
        }
        return new Vector2Int(-1, -1); // Fallback
    }

    private Vector2Int FindEasyMove(int[,] board)
    {
        // Implement Easy AI Logic
        return new Vector2Int(
            UnityEngine.Random.Range(0, boardSize),
            UnityEngine.Random.Range(0, boardSize)
        );
    }
}
