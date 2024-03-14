using UnityEngine;

[CreateAssetMenu(fileName = "GameSettings", menuName = "Gomoku/GameSettings", order = 1)]
public class GameSettings : ScriptableObject
{
    public int boardSize = 15; // Default 15x15 board
    public GameObject pieceBlackPrefab;
    public GameObject pieceWhitePrefab;
    public float pieceScale = 1.0f; // Adjust the scale of pieces
    public GameMode gameMode;

    public enum GameMode
    {
        AI,
        Players
    };
}
