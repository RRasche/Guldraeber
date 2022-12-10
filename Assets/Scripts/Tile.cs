using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CanEditMultipleObjects]
public class Tile : MonoBehaviour
{
    
    public GameObject[] prefabs;
    private Tile[][] map;
    public enum TileType{
        DEFAULT = 0,
        GRASS = 1,
        WOOD = 2,
        WATER = 3
    }
    
    public TileType type;
    private int[] index = new int[2];

    public int[] GetIndex(){
        return index;
    }

    public void SetMap(Tile[][] m){
        map = m;
    }

    public void SetIndex(int x, int y){
        index[0] = x;
        index[1] = y;
    }
    




    public void ChangeTile(GameObject[] pre){
        
        GameObject newTile = PrefabUtility.InstantiatePrefab(prefabs[(int)type]) as GameObject;
        newTile.transform.position = transform.position;
        newTile.transform.parent = transform.parent;
        Tile tl = newTile.GetComponent<Tile>();
        map[index[0]][index[1]] = tl;
        tl.SetIndex(index[0],index[1]);
        tl.SetMap(map);
        tl.prefabs = prefabs;
        DestroyImmediate(gameObject);
    }
}
