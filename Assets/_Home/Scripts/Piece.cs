using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(fileName = "Piece", menuName = "Piece", order = 0)]
public class Piece : ScriptableObject
{
    public int size = 7;
    [HideInInspector]
    public Vector2Int mainTilePosition;
    public bool hasMainTilePosition
    {
        get
        {
            return mainTilePosition.x >= 0
                && mainTilePosition.x < tiles.GetLength(0)
                && mainTilePosition.y >= 0
                && mainTilePosition.y < tiles.GetLength(1);
        }
    }
    private bool[,] _tiles;
    public bool[,] tiles
    {
        get
        {
            if (_tiles == null
                || _tiles.GetLength(0) != size
                || _tiles.GetLength(1) != size)
            {
                _tiles = new bool[size, size];
            }
            return _tiles;
        }
        set
        {
            _tiles = value;
        }
    }
    private void Awake()
    {
        mainTilePosition = new Vector2Int(tiles.GetLength(0) / 2, tiles.GetLength(1) / 2);
        tiles[tiles.GetLength(0) / 2, tiles.GetLength(1) / 2] = true;
    }

    /// <summary>
    /// Rotates the piece clockwise.
    /// Taken from https://www.cyotek.com/blog/rotating-an-array-using-csharp
    /// </summary>
    /// <returns></returns>
    public bool[,] RotateClockwise()
    {
        int width;
        int height;
        bool[,] dst;

        width = tiles.GetUpperBound(0) + 1;
        height = tiles.GetUpperBound(1) + 1;
        dst = new bool[height, width];

        for (int row = 0; row < height; row++)
        {
            for (int col = 0; col < width; col++)
            {
                int newRow;
                int newCol;

                newRow = col;
                newCol = height - (row + 1);

                dst[newCol, newRow] = tiles[col, row];
            }
        }
        mainTilePosition = new Vector2Int(
            height - (mainTilePosition.y + 1),
            mainTilePosition.x
        );
        tiles = dst;
        return dst;
    }

    /// <summary>
    /// Rotates the piece counter-clockwise.
    /// Taken from https://www.cyotek.com/blog/rotating-an-array-using-csharp
    /// </summary>
    /// <returns></returns>
    public bool[,] RotateCounterClockwise()
    {
        int width;
        int height;
        bool[,] dst;

        width = tiles.GetUpperBound(0) + 1;
        height = tiles.GetUpperBound(1) + 1;
        dst = new bool[height, width];

        for (int row = 0; row < height; row++)
        {
            for (int col = 0; col < width; col++)
            {
                int newRow;
                int newCol;

                newRow = width - (col + 1);
                newCol = row;

                dst[newCol, newRow] = tiles[col, row];
            }
        }
        mainTilePosition = new Vector2Int(
            mainTilePosition.y,
            width - (mainTilePosition.x + 1)
        );
        tiles = dst;
        return dst;
    }


}

#if UNITY_EDITOR
[CustomEditor(typeof(Piece))]
public class EditorTestEditor : Editor
{
    Piece piece;
    public override void OnInspectorGUI()
    {
        piece = (Piece)target;

        // Get reference to the script object
        base.OnInspectorGUI();
        // Button
        if (GUILayout.Button("Rotate Clockwise ./\\>"))
        {
            piece.RotateClockwise();
        }
        if (GUILayout.Button("Rotate Counter Clockwise </\\."))
        {
            piece.RotateCounterClockwise();
        }
        EditorGUILayout.LabelField("Choose main tile", EditorStyles.boldLabel);
        DrawMainGrid();
        // Section 
        EditorGUILayout.Separator();
        EditorGUILayout.LabelField("Choose tiles", EditorStyles.boldLabel);
        DrawRegularGrid();

    }

    private void DrawRegularGrid()
    {
        for (int y = 0; y < piece.tiles.GetLength(1); y++)
        {
            EditorGUILayout.BeginHorizontal();
            for (int x = 0; x < piece.tiles.GetLength(0); x++)
            {
                if (piece.hasMainTilePosition)
                {
                    if (x == piece.mainTilePosition.x &&
                        y == piece.mainTilePosition.y)
                    {
                        GUI.enabled = false;
                        piece.tiles[x, y] = EditorGUILayout.Toggle(true);
                        GUI.enabled = true;
                        continue;
                    }
                }
                piece.tiles[x, y] = EditorGUILayout.Toggle(piece.tiles[x, y]);
            }
            EditorGUILayout.EndHorizontal();
        }
    }


    private void DrawMainGrid()
    {

        for (int y = 0; y < piece.tiles.GetLength(1); y++)
        {

            EditorGUILayout.BeginHorizontal();
            if (piece.hasMainTilePosition)
                GUI.enabled = false;

            for (int x = 0; x < piece.tiles.GetLength(0); x++)
            {
                if (piece.hasMainTilePosition)
                {
                    if (x == piece.mainTilePosition.x &&
                        y == piece.mainTilePosition.y)
                    {
                        GUI.enabled = true;
                        bool isMain = EditorGUILayout.Toggle(true);
                        if (!isMain)
                        {
                            piece.mainTilePosition = new Vector2Int(-1, -1);
                            piece.tiles[x, y] = false;
                        }
                        GUI.enabled = false;
                    }
                    else
                    {
                        EditorGUILayout.Toggle(false);
                    }
                }
                else
                {
                    bool isMain = EditorGUILayout.Toggle(false);
                    if (isMain)
                    {
                        piece.mainTilePosition = new Vector2Int(x, y);
                        piece.tiles[x, y] = true;
                        GUI.enabled = false;
                        Debug.Log(piece.mainTilePosition);
                    }
                }
            }

            EditorGUILayout.EndHorizontal();

        }

        GUI.enabled = true;
    }
}
#endif