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
    private bool[,] _bools;
    public bool[,] bools
    {
        get
        {
            if (_bools == null
                || _bools.GetLength(0) != size
                || _bools.GetLength(1) != size)
            {
                _bools = new bool[size, size];
            }
            return _bools;
        }
        set
        {
            _bools = value;
        }
    }
    private void Awake()
    {
        bools[bools.GetLength(0) / 2, bools.GetLength(1) / 2] = true;
    }


}

#if UNITY_EDITOR
[CustomEditor(typeof(Piece))]
public class EditorTestEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // Get reference to the script object
        Piece script = (Piece)target;
        base.OnInspectorGUI();
        bool[,] newBools = script.bools;
        for (int i = 0; i < script.bools.GetLength(1); i++)
        {
            EditorGUILayout.BeginHorizontal();
            for (int j = 0; j < script.bools.GetLength(0); j++)
            {
                if (j == script.bools.GetLength(0) / 2 && i == script.bools.GetLength(1) / 2)
                {
                    GUI.enabled = false;
                    newBools[j, i] = true;
                    EditorGUILayout.Toggle(newBools[j, i]);
                    GUI.enabled = true;
                }
                else
                {
                    newBools[j, i] = EditorGUILayout.Toggle(newBools[j, i]);
                }
            }
            EditorGUILayout.EndHorizontal();
        }
        script.bools = newBools;

    }
}
#endif