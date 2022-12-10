using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Tile))]
[CanEditMultipleObjects]
public class TileEditor : Editor
{

    SerializedProperty type;

    void OnEnable(){
        serializedObject.FindProperty("type");
    }
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        DrawDefaultInspector();

        Tile myScript = (Tile)target;
        if(GUILayout.Button("Change Tile"))
        {
            myScript.ChangeTile();
        }
        serializedObject.ApplyModifiedProperties();
    }
}
