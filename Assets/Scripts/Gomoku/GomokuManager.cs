using System;
using UnityEngine;
using UnityEngine.UI;

public class GomokuManager : MonoBehaviour
{
    // Public variables and serialized fields
    [SerializeField]
    private GameSettings gameSettings; // Consider using [SerializeField] for private fields that need to be set in the Inspector.

    [SerializeField]
    private Camera gameCamera;

    [SerializeField]
    private float cameraDistanceMultiplier = 1.1f; // Adjust based on preference for how much of the board to show.

    // Private variables
    private int[,] board;
    private int currentPlayer = 1;
    private GameSettings.GameMode currentMode;

    // Unity lifecycle methods
    void Start()
    {
        AdjustCameraBasedOnBoard();
        board = new int[gameSettings.boardSize + 1, gameSettings.boardSize + 1];
        InitializeBoard();
        SetGameMode();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            PlacePiece();
        }
    }

    void SetGameMode()
    {
        currentMode = gameSettings.gameMode;
    }

    // Custom methods related to game setup
    private void AdjustCameraBasedOnBoard()
    {
        if (gameCamera.orthographic)
        {
            gameCamera.orthographicSize = gameSettings.boardSize / 2.0f * cameraDistanceMultiplier;
        }
        else
        {
            float distance = CalculateCameraDistance(gameSettings.boardSize);
            gameCamera.transform.position = new Vector3(
                gameCamera.transform.position.x,
                gameCamera.transform.position.y,
                -distance
            );
        }
    }

    private float CalculateCameraDistance(int boardSize)
    {
        return boardSize * cameraDistanceMultiplier;
    }

    private void InitializeBoard()
    {
        for (int i = 0; i < gameSettings.boardSize + 1; i++)
        {
            for (int j = 0; j < gameSettings.boardSize + 1; j++)
            {
                board[i, j] = 0;
            }
        }
    }

    // Custom methods related to gameplay
    private void PlacePiece()
    {
        if (currentPlayer == -1)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mouseScreenPosition = Input.mousePosition;
            float depth = Mathf.Abs(gameCamera.transform.position.z);

            Vector3 mouseWorldPosition = gameCamera.ScreenToWorldPoint(
                new Vector3(mouseScreenPosition.x, mouseScreenPosition.y, depth)
            );

            float halfBoardSize = gameSettings.boardSize / 2.0f;
            int x = Mathf.RoundToInt(mouseWorldPosition.x + halfBoardSize);
            int y = Mathf.RoundToInt(mouseWorldPosition.y + halfBoardSize);

            Debug.Log(x + ", " + y);

            if (
                x >= 0
                && x < gameSettings.boardSize + 1f
                && y >= 0
                && y < gameSettings.boardSize + 1f
                && board[x, y] == 0
            )
            {
                board[x, y] = currentPlayer;
                Vector3 spawnPosition = new Vector3(x - halfBoardSize, y - halfBoardSize, 0);
                GameObject piecePrefab =
                    currentPlayer == 1
                        ? gameSettings.pieceWhitePrefab
                        : gameSettings.pieceBlackPrefab;
                var piece = Instantiate(piecePrefab, spawnPosition, Quaternion.identity);
                piece.transform.localScale = Vector3.one * gameSettings.pieceScale;

                if (CheckWin(x, y))
                {
                    Debug.Log($"Player {currentPlayer} wins!");
                }
                else
                {
                    SwitchPlayer();
                }
            }
        }
    }

    private bool CheckWin(int x, int y)
    {
        int[][] directions = new int[][]
        {
            new int[] { 1, 0 },
            new int[] { 0, 1 },
            new int[] { 1, 1 },
            new int[] { -1, 1 }
        };

        foreach (var dir in directions)
        {
            int count = 1;
            count += CountDirection(x, y, dir[0], dir[1]);
            count += CountDirection(x, y, -dir[0], -dir[1]);

            if (count >= 5)
            {
                return true;
            }
        }

        return false;
    }

    private void SwitchPlayer()
    {
        switch (currentMode)
        {
            case GameSettings.GameMode.AI:
                currentPlayer = currentPlayer == 1 ? -1 : 1;
                break;
            case GameSettings.GameMode.Players:
                currentPlayer = currentPlayer == 1 ? 2 : 1;
                break;
        }
    }

    private int CountDirection(int x, int y, int dx, int dy)
    {
        int count = 0;
        for (int i = 1; i < 5; i++)
        {
            int newX = x + i * dx;
            int newY = y + i * dy;

            if (
                newX >= 0
                && newX <= gameSettings.boardSize
                && newY >= 0
                && newY <= gameSettings.boardSize
                && board[newX, newY] == currentPlayer
            )
            {
                count++;
            }
            else
            {
                break;
            }
        }
        return count;
    }
}
