using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MapGenerator : MonoBehaviour
{
    public GameObject hexPrefab;
    public const float outerRadius = 1.0f;
    public const float innerRadius = outerRadius * 0.866025404f;
    public int width = 50;
    public int height = 31;
    [SerializeField]
    public GameObject mapObj;

    public Vector3 startingLocation = new Vector3(0,0,0);
    [SerializeField]
    public static Tile[][] map;
    [SerializeField]
    private GameObject[] prefabs;

    public void GenerateMap(){
        map = new Tile[height][];
        for(int i = 0; i < height; i++){
            map[i] = new Tile[width - i % 2];
        }
        mapObj = new GameObject();
        mapObj.name = "TileMap";

        float x = - ((float) width - 1) / 2 * innerRadius;
        float z = - ((float) height - 1) / 2 * outerRadius;

        for(int i = 0; i < map.Length; i++){
            float h = startingLocation.y - ((float) height - 1) / 2 * outerRadius + i * outerRadius * 1.5f;
            for(int j = 0; j < map[i].Length; j++){
                float w = startingLocation.x - ((float) width - 1) / 2 * innerRadius + innerRadius * (i % 2) + j * 2 * innerRadius;
                #if UNITY_EDITOR
                GameObject tile = PrefabUtility.InstantiatePrefab(hexPrefab) as GameObject;
                #else
                GameObject tile = Instantiate(hexPrefab, new Vector3(0, 0, 0), Quaternion.identity);
                #endif
                tile.transform.position = new Vector3(w, h, 0.5f);
                tile.transform.parent = mapObj.transform;
                Tile tl = tile.GetComponent<Tile>();
                tile.name = tl.type + (i * width + j).ToString();
                map[i][j] = tl;
                tl.SetIndex(j, i);
                tl.SetMap(map);
                tl.prefabs = prefabs;
            }
        }
        //mapObj.transform.rotation = Quaternion.Euler(0.0f,180.0f,0.0f);

    }

    public void UpdateTiles(){
        for(int i = 0; i < map.Length; i++){
            for(int j = 0; j < map[i].Length; j++){
                Debug.Log("changing Tile: [" + i + ", "+ j+ "]");
                map[i][j].ChangeTile();
            }
        }
    }

    public static Tile GetTileAtPosition(Vector2 pos)
    {
        int y = Mathf.RoundToInt(pos.y * 2.0f/3.0f);
        int x = Mathf.RoundToInt((pos.x - Mathf.Sqrt(3.0f)/2.0f * (y % 2)) / Mathf.Sqrt(3));
    
        return map[y][x];
    }

    public static Tile GetTileByIndex(Vector2Int ind){
        return map[ind.y][ind.x];
    }

    public void RebuildMap()
    {
        map = new Tile [height][];
        for(int i = 0; i < height; ++i)
        {
            map[i] = new Tile [width - i % 2];
        }

        foreach ( Tile t in mapObj.GetComponentsInChildren<Tile>())
        {
            map[t.self_idx.y][t.self_idx.x] = t;
            t.map = map;
            t.prefabs = prefabs;
        }
    }

    void OnEnable()
    {
       RebuildMap();
    }
}
