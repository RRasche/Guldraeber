using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
#if UNITY_EDITOR
[CanEditMultipleObjects]
#endif
public class Tile : MonoBehaviour
{
    
    public GameObject[] prefabs;
    public Tile[][] map;
    public enum TileType{
        DEFAULT = 0,
        GRASS = 1,
        WOOD = 2,
        WATER = 3,
        BURNING = 4
    }

    [SerializeField]
    public float burn_increase = 10.0f;
    public TileType type;
    public float burning_state; 

    private int range;
    private int off_row;
    private int max_x;
    private int max_y;

    private int neighbor_case;
    public Vector2Int self_idx = new Vector2Int(0, 0);
    private Vector2Int burn_idx = new Vector2Int(0, 0);
    private Tile burn_tile;
    private Vector2 wind_direction;

    public Vector2Int GetIndex(){
        return self_idx;
    }

    public void SetMap(Tile[][] m){
        map = m;
    }

    public void SetIndex(int x, int y){
        self_idx.x = x;
        self_idx.y = y;
    }
    
    public void ChangeTile(){
        #if UNITY_EDITOR
        GameObject newTile = PrefabUtility.InstantiatePrefab(prefabs[(int)type]) as GameObject;
        #else
        GameObject newTile = Instantiate(prefabs[(int)type], new Vector3(0, 0, 0), Quaternion.Euler(-90.0f,0.0f,0.0f));
        #endif
        newTile.transform.position = transform.position;
        newTile.transform.parent = transform.parent;
        Tile tl = newTile.GetComponent<Tile>();
        map[self_idx.y][self_idx.x] = tl;
        tl.SetIndex(self_idx.x, self_idx.y);
        tl.SetMap(map);
        tl.type = type;
        tl.prefabs = prefabs;
        DestroyImmediate(gameObject);
    }

    void Start()
    {
        off_row = self_idx.y % 2;
        max_y = map.Length;
        max_x = map[0].Length; 
        burning_state = 0.0f;
    }

    void FixedUpdate()
    {
        if(type == TileType.BURNING)
        {
            wind_direction = wind_controller.cur_wind_direction;
            
            range = wind_direction.x > 0.8f ? 2 : 1;

            for(int i_range = 1; i_range <= range; ++i_range)
            {
                neighbor_case = (int) ((wind_direction.y + Random.Range(-Mathf.PI/3.0f, Mathf.PI/3.0f)) * i_range * 3.0f / Mathf.PI + 
                    0.5f);
                neighbor_case = neighbor_case % (6 * i_range);

                burn_idx = self_idx;

                update_burn_idx(i_range);
                
                if(burn_idx.x < (max_x - burn_idx.y % 2) && burn_idx.y < max_y)
                {
                    if(burn_idx.x >= 0 && burn_idx.y >= 0)
                    {
                        burn_tile = map[burn_idx.y][burn_idx.x];
                        if(burn_tile.type == TileType.WOOD)
                        {
                            burn_tile.burning_state += burn_increase - burn_increase/2.0f * (i_range - 1);

                            if(burn_tile.burning_state >= 100.0f)
                            {
                                Debug.Log("Burn");
                                burn_tile.type = TileType.BURNING;
                                burn_tile.ChangeTile();
                            }      
                        }
                    }
                }              
            }

        }
    }

    private void update_burn_idx(int range)
    {
         if(range == 1)
        {
            switch(neighbor_case)
            {
                //        / \     / \
                //      /     \ /     \
                //     |   2   |   1  |
                //     |       |       |
                //    / \     / \     / \
                //  /     \ /     \ /     \
                // |   3   |   c   |   0   |
                // |       |       |       |
                //  \     / \     / \     /
                //    \ /     \ /     \ /
                //     |   4   |   5   |
                //     |       |       |
                //      \     / \     /
                //        \ /     \ /
                case 0:
                    burn_idx.x += 1;
                    break;

                case 1:
                    burn_idx.x += off_row;
                    burn_idx.y += 1;
                    break;
                
                case 2:
                    burn_idx.x -= 1 - off_row;
                    burn_idx.y += 1;
                    break;
                
                case 3:
                    burn_idx.x -= 1;
                    break;
                
                case 4:
                    burn_idx.x -= 1 - off_row;
                    burn_idx.y -= 1;
                    break;

                case 5:
                    burn_idx.x += off_row;
                    burn_idx.y -= 1;
                    break;
            }
        }
        else
        {
            switch(neighbor_case)
            {
                case 0:
                    burn_idx.x += 2;
                    break;
                case 1:
                    burn_idx.x += 1 + off_row;
                    burn_idx.y += 1;
                    break;
                case 2:
                    burn_idx.x += 1;
                    burn_idx.y += 2;
                    break;
                case 3:
                    burn_idx.y += 2;
                    break;
                case 4:
                    burn_idx.x -= 1;
                    burn_idx.y += 2;
                    break;
                case 5:
                    burn_idx.x -= 2 - off_row;
                    burn_idx.y += 1;
                    break;
                case 6:
                    burn_idx.x -= 2;
                    break;
                case 7:
                    burn_idx.x -= 2 - off_row;
                    burn_idx.y -= 1;
                    break;
                case 8:
                    burn_idx.x -= 1;
                    burn_idx.y -= 2;
                    break;
                case 9:
                    burn_idx.y -= 2;
                    break;
                case 10:
                    burn_idx.x += 1;
                    burn_idx.y -= 2;
                    break;
                case 11:
                    burn_idx.x += 1 + off_row;
                    burn_idx.y -= 1;
                    break;
            }
        } 
    }
    
}
