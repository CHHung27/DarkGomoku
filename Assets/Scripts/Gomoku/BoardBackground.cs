using UnityEngine;

public class BoardBackground : MonoBehaviour
{
    public GameSettings gameSettings;
    public Material lineMaterial; // Assign a basic material in the inspector

    void AdjustCamera()
    {
        Camera.main.orthographicSize = gameSettings.boardSize / 2.0f + 1; // Plus one to ensure a margin
        // Camera.main.transform.position = new Vector3(
        //     (gameSettings.boardSize - 1) / 2.0f,
        //     (gameSettings.boardSize - 1) / 2.0f,
        //     -10
        // );
    }

    private void Start()
    {
        DrawGrid();
        AdjustCamera();
    }

    void DrawGrid()
    {
        for (int i = 0; i <= gameSettings.boardSize; i++)
        {
            // Vertical lines
            CreateLine(
                new Vector2(i - gameSettings.boardSize / 2.0f, -gameSettings.boardSize / 2.0f),
                new Vector2(i - gameSettings.boardSize / 2.0f, gameSettings.boardSize / 2.0f)
            );
            // Horizontal lines
            CreateLine(
                new Vector2(-gameSettings.boardSize / 2.0f, i - gameSettings.boardSize / 2.0f),
                new Vector2(gameSettings.boardSize / 2.0f, i - gameSettings.boardSize / 2.0f)
            );
        }
    }

    void CreateLine(Vector2 start, Vector2 end)
    {
        GameObject line = new GameObject("Line");
        line.transform.parent = transform;
        LineRenderer lr = line.AddComponent<LineRenderer>();
        lr.material = lineMaterial;
        lr.startWidth = 0.05f; // Adjust line width as needed
        lr.endWidth = 0.05f;
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
    }
}
