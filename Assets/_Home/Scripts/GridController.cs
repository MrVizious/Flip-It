using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class GridController : MonoBehaviour
{
    public int gridSize = 7;
    public int currentTurn = 1;

    public GameObject tileObjectPrefab;
    private Tile[,] tiles;

    private void Start()
    {
        tiles = new Tile[gridSize, gridSize];
        for (int i = 0; i < gridSize; i++)
        {
            for (int j = 0; j < gridSize; j++)
            {
                GameObject tileModel = Instantiate(
                    tileObjectPrefab,
                    new Vector3((i - 1f) / 2f + i, 0, (j - 1f) / 2f + j),
                    Quaternion.identity);
                tiles[i, j] = new Tile(1, 0, tileModel.GetComponent<TileModelController>());
            }
        }
    }

    public void NextTurn()
    {
        currentTurn = currentTurn == 1 ? 2 : 1;
    }

    public async void FlipEverything()
    {
        Debug.Log("Flip everything");
        List<Task> tasks = new List<Task>();
        for (int i = 0; i < gridSize; i++)
        {
            for (int j = 0; j < gridSize; j++)
            {
                tasks.Add(tiles[i, j].Flip());
            }
        }
        // Wait until all tasks are finished
        await Task.WhenAll(tasks);
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(GridController))]
public class GameControllerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        GridController gameController = (GridController)target;
        if (GUILayout.Button("Flip Everything"))
        {
            gameController.FlipEverything();
        }
    }
}
#endif