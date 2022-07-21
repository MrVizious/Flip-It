using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class GameController : MonoBehaviour
{
    public int gridSize = 7;
    public int currentTurn = 1;

    public GameObject tileModelPrefab;
    private Tile[,] tiles;

    private void Start()
    {
        tiles = new Tile[gridSize, gridSize];
        for (int i = 0; i < gridSize; i++)
        {
            for (int j = 0; j < gridSize; j++)
            {
                GameObject tileModel = Instantiate(tileModelPrefab, new Vector3((i - 1f) / 2f + i, 0, (j - 1f) / 2f + j), Quaternion.identity);
                tiles[i, j] = new Tile(1, tileModel.GetComponent<TileModel>());
            }
        }
    }

    public void NextTurn()
    {
        currentTurn = currentTurn == 1 ? 2 : 1;
    }

    public async void FlipEverything()
    {
        for (int i = 0; i < gridSize; i++)
        {
            for (int j = 0; j < gridSize; j++)
            {
                tiles[i, j].Flip();
            }
        }
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(GameController))]
public class GameControllerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        GameController gameController = (GameController)target;
        if (GUILayout.Button("Flip Everything"))
        {
            gameController.FlipEverything();
        }
    }
}
#endif