using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


#if UNITY_EDITOR
[CustomEditor(typeof(MapGenerator))]
public class MapGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        MapGenerator myScript = (MapGenerator)target;
        if(GUILayout.Button("Generate Map"))
        {
            myScript.GenerateMap();
        }
        if(GUILayout.Button("Update Tiles")){
            myScript.UpdateTiles();
        }
    }
}

#endif