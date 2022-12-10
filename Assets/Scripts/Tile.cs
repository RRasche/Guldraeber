using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CanEditMultipleObjects]
public class Tile : MonoBehaviour
{
    
    [SerializeField]
    public GameObject[] prefabs = new GameObject[4];
    public enum TileType{
        DEFAULT = 0,
        GRASS = 1,
        WOOD = 2,
        WATER = 3
    }
    
    public TileType type;




    public void ChangeTile(){
        GameObject newTile = PrefabUtility.InstantiatePrefab(prefabs[(int)type]) as GameObject;
        newTile.transform.position = this.transform.position;
        newTile.transform.parent = this.transform.parent;
        DestroyImmediate(gameObject);
    }
}
