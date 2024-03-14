using UnityEngine;

public class GomokuManager : MonoBehaviour
{
    [SerializeField]
    private GameSettings gameSettings;

    [SerializeField]
    private Camera gameCamera;

    [SerializeField]
    private float cameraDistanceMultiplier = 1.1f;

    private AIPlayer aiPlayer;
    private int[,] board;
    private int currentPlayer = 1; // Player 1 starts
    private bool isPlayerTurn = true; // True if it's the human player's turn, false for AI

    void Start()
    {
        board = new int[gameSettings.boardSize + 1, gameSettings.boardSize + 1];
        InitializeBoard();
        AdjustCameraBasedOnBoard();
        // aiPlayer = new AIPlayer(gameSettings.boardSize, currentPlayer, (currentPlayer + 1) % 2);
    }

    // TODO add start game function (calls menu ui)
    // public void StartGame() 
    // {
        
    // }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            PlayerMakeMove();
    }

    private void InitializeBoard()
    {
        for (int i = 0; i < gameSettings.boardSize; i++)
        {
            for (int j = 0; j < gameSettings.boardSize; j++)
            {
                board[i, j] = 0; // Initialize all board positions to 0
            }
        }
    }

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
        // This is a placeholder calculation. You may need to adjust this based on your camera's field of view and the desired visibility.
        return boardSize * cameraDistanceMultiplier;
    }

    private void PlayerMakeMove()
    {
        Vector2Int move = GetBoardPositionFromInput();
        if (IsValidMove(move))
        {
            PlacePiece(move, currentPlayer);
            if (CheckWin(move, currentPlayer))
            {
                Debug.Log($"Player {currentPlayer} wins!");
                // Implement game over logic or reset the game
            }
            else
            {
                SwitchTurn();
            }
        }
    }

    private Vector2Int GetBoardPositionFromInput()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 worldPos = gameCamera.ScreenToWorldPoint(
            new Vector3(mousePos.x, mousePos.y, Mathf.Abs(gameCamera.transform.position.z))
        );
        int x = Mathf.RoundToInt(worldPos.x + gameSettings.boardSize / 2.0f);
        int y = Mathf.RoundToInt(worldPos.y + gameSettings.boardSize / 2.0f);
        return new Vector2Int(x, y);
    }

    private void AIMakeMove()
    {
        // Vector2Int aiMove = aiPlayer.GetMove(board, 2); // Assuming AI is player 2
        // if (IsValidMove(aiMove))
        // {
        //     PlacePiece(aiMove, 2);
        //     if (CheckWin(aiMove, 2))
        //     {
        //         Debug.Log("AI Wins!");
        //         // Implement game over logic or reset the game
        //     }
        //     else
        //     {
        //         SwitchTurn();
        //     }
        // }
        // Optionally include a small delay before AI moves for better gameplay experience
    }

    private bool IsValidMove(Vector2Int move)
    {
        if (
            move.x >= 0
            && move.x <= gameSettings.boardSize
            && move.y >= 0
            && move.y <= gameSettings.boardSize
        )
        {
            return board[move.x, move.y] == 0; // The spot is valid if it's empty
        }
        return false;
    }

    private void PlacePiece(Vector2Int move, int player)
    {
        // Update the board state
        board[move.x, move.y] = player;

        // Calculate the world position for the piece based on the board coordinates
        float halfBoardSize = gameSettings.boardSize / 2.0f;
        Vector3 spawnPosition = new Vector3(move.x - halfBoardSize, move.y - halfBoardSize, 0);

        // Instantiate the piece prefab for the current player
        GameObject piecePrefab =
            player == 1 ? gameSettings.pieceWhitePrefab : gameSettings.pieceBlackPrefab;
        GameObject piece = Instantiate(piecePrefab, spawnPosition, Quaternion.identity, transform); // Parent the piece to the game manager or a specific board object
        piece.transform.localScale = Vector3.one * gameSettings.pieceScale; // Adjust scale if necessary
    }

    private bool CheckWin(Vector2Int move, int player)
    {
        int[] directionsX = { 1, 0, 1, 1 };
        int[] directionsY = { 0, 1, 1, -1 };
        const int WIN_CONDITION = 5;

        for (int i = 0; i < directionsX.Length; i++)
        {
            int count = 1; // Start with the current piece

            // Check in the positive direction
            count += CountInDirection(move, directionsX[i], directionsY[i], player);

            // Check in the negative direction (reverse the direction)
            count += CountInDirection(move, -directionsX[i], -directionsY[i], player);

            if (count >= WIN_CONDITION)
                return true; // Found a winning line
        }

        return false; // No win found
    }

    private int CountInDirection(Vector2Int start, int dx, int dy, int player)
    {
        int count = 0;
        int x = start.x + dx;
        int y = start.y + dy;

        while (
            x >= 0
            && x <= gameSettings.boardSize
            && y >= 0
            && y <= gameSettings.boardSize
            && board[x, y] == player
        )
        {
            count++; // Increment count if the piece belongs to the player
            x += dx;
            y += dy;
        }

        return count;
    }

    private void SwitchTurn()
    {
        isPlayerTurn = !isPlayerTurn; // Toggle turn
        currentPlayer = isPlayerTurn ? 1 : 2; // Toggle between player 1 and 2/AI
    }
}
