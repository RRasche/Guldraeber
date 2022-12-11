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
        WATER = 2,
        NEEDLE_SMALL = 3,
        NEEDLE_LARGE = 4,
        LEAVES_SMALL = 5,
        LEAVES_LARGE = 6,
        BURN_NEEDLE_SMALL = 7,
        BURN_NEEDLE_LARGE = 8,
        BURN_LEAVES_SMALL = 9,
        BURN_LEAVES_LARGE = 10,
        EX_NEEDLE_SMALL = 11,
        EX_NEEDLE_LARGE = 12,
        EX_LEAVES_SMALL = 13,
        EX_LEAVES_LARGE = 14,
        DEAD_NEEDLE_SMALL = 15,
        DEAD_NEEDLE_LARGE = 16,
        DEAD_LEAVES_SMALL = 17,
        DEAD_LEAVES_LARGE = 18,
        VILLAGE_SMALL = 19,
        VILLAGE_LARGE = 20,
        BURN_VILLAGE_SMALL = 21,
        BURN_VILLAGE_LARGE = 22,
        EX_VILLAGE_SMALL = 23,
        EX_VILLAGE_LARGE = 24,
        DEAD_VILLAGE_SMALL = 25,
        DEAD_VILLAGE_LARGE = 26,
        MOUNTAIN_SMALL = 27,
        MOUNTAIN_LARGE = 28,
        DIRT = 29

    }

    [SerializeField]
    public float burn_increase = 1.0f;
    public TileType type;
    public float burning_state; 
    public float extinguish_state; 
    public float demolish_state;
    public float life = 100.0f;
    private float life_change = 1.0f/15.0f;

    private int range;
    private int off_row;
    private int max_x;
    private int max_y;
    private int typeNr;

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
        extinguish_state = 100.0f;
        typeNr = (int)type;
    }

    void FixedUpdate()
    {
        //burning tiles are 7,8,9,10,21,22
        if((typeNr >= 7 && typeNr <=10) || typeNr == 21 || typeNr == 22)
        {
            life -= life_change;
            if(life <= 0)
            {
                type = (TileType)(typeNr + (typeNr <= 10 ? 8 : 4));
                ChangeTile();
            }

            wind_direction = wind_controller.cur_wind_direction;
            
            range = wind_direction.x > 0.5f ? 2 : 1;

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
                        int burn_tile_typeNr = (int) burn_tile.type;
                        //3,4,5,6,11,12,13,14,19,20,23,24
                        if((burn_tile_typeNr >= 3 && burn_tile_typeNr <=6) || (burn_tile_typeNr >= 11 && burn_tile_typeNr <=14) 
                        || burn_tile_typeNr == 19 || burn_tile_typeNr == 20 || burn_tile_typeNr == 23 || burn_tile_typeNr == 24)
                        {
                            burn_tile.burning_state += burn_increase - burn_increase/2.0f * (i_range - 1);

                            if(burn_tile.burning_state >= 100.0f)
                            {
                                Debug.Log("Burn");
                                if(burn_tile_typeNr <= 6)
                                    burn_tile.type = ((TileType)(burn_tile_typeNr + 4));
                                else if(burn_tile_typeNr <= 14)
                                    burn_tile.type = ((TileType )burn_tile_typeNr - 4);
                                else if(burn_tile_typeNr <= 20)
                                    burn_tile.type = ((TileType )burn_tile_typeNr + 2);
                                else if(burn_tile_typeNr <= 24)
                                    burn_tile.type = ((TileType )burn_tile_typeNr - 2);
                                
                                burn_tile.ChangeTile();
                            }      
                        }
                    }
                }              
            }

        }
    }

    private bool is_burning(int typeNr)
    {
        return (typeNr >= 7 && typeNr <=10) || typeNr == 21 || typeNr == 22;
    }

    private bool is_wood(int typeNr)
    {
        return (typeNr >= 3 && typeNr <= 6) || (typeNr >= 11 && typeNr <= 14);
    }
    public void Extinguish_Me_a_BIT(float strength)
    {
        if(is_burning(typeNr))
        {
            extinguish_state -= strength;
            if(extinguish_state <= 0.0f)
            {
                if(typeNr <= 10)
                    type = ((TileType)(type + 4));
                else
                    type = ((TileType )type + 2);

                ChangeTile();              
            }

        }
    }
    public void Demolish_Me_a_BIT(float strength) 
    {
        if(is_wood(typeNr))
        {
            demolish_state -= strength;
            if(demolish_state <= 0.0f)
            {
                type = TileType.DIRT;  
                ChangeTile();              
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
