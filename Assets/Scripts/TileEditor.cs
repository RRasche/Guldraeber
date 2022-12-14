using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

#if UNITY_EDITOR
[CustomEditor(typeof(Tile))]
[CanEditMultipleObjects]
public class TileEditor : Editor
{

    void OnEnable(){
        serializedObject.FindProperty("type");
    }
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Tile myScript = (Tile)target;
        //if(GUILayout.Button("Change Tile"))
        //{
        //    myScript.ChangeTile();
        //}
        if(GUILayout.Button("Get Index"))
        {
            Vector2Int ind = myScript.GetIndex();
            Debug.Log("[" + ind.x + ", " + ind.y + "]");
        }
    }
}
#endif