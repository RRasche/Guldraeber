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

    public Vector3 startingLocation = new Vector3(0,0,0);

    public Tile[][] map;
    public Vector3[][] coordinateMap;

    public void GenerateMap(){
        map = new Tile[height][];
        coordinateMap = new Vector3[height][];
        for(int i = 0; i < height; i++){
            map[i] = new Tile[width - i % 2];
            coordinateMap[i] = new Vector3[width - i % 2];
        }
        GameObject mapObj = new GameObject();
        mapObj.name = "TileMap";

        float x = - ((float) width - 1) / 2 * innerRadius;
        float z = - ((float) height - 1) / 2 * outerRadius;

        for(int i = 0; i < map.Length; i++){
            float h = startingLocation.y - ((float) height - 1) / 2 * outerRadius + i * outerRadius * 1.5f;
            for(int j = 0; j < map[i].Length; j++){
                float w = startingLocation.x - ((float) width - 1) / 2 * innerRadius + innerRadius * (i % 2) + j * 2 * innerRadius;
                GameObject tile = PrefabUtility.InstantiatePrefab(hexPrefab) as GameObject;
                tile.transform.position = new Vector3(w, h, 0.5f);
                tile.transform.parent = mapObj.transform;
                map[i][j] = tile.GetComponent<Tile>();
                coordinateMap[i][j] = tile.transform.position;
            }
        }
        mapObj.transform.rotation = Quaternion.Euler(0.0f,180.0f,0.0f);

    }


}
