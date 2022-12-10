using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fire_spreader : MonoBehaviour
{
    Vector2 wind_direction;
    Vector2 wind_direction_cart;
    int [] self_idx;
    int [] burn_idx;

    int range;
    int off_row;

    int neighbor_case;

    int [][] map;

    int max_x;
    int max_y;

    GameObject burn_tile;

    // Start is called before the first frame update
    void Start()
    {
        off_row = self_idx[1] % 2;
        max_y = map.Length;
        max_x = map[0].Length;
    }

    // Update is called once per frame
    void FixeUpdate()
    {
        wind_direction = wind_controller.cur_wind_direction;
        
        range = wind_direction.x > 0.8f ? 2 : 1;

        for(int i_range = 1; i_range <= range; ++i_range)
        {
            neighbor_case = (int) (wind_direction.y * i_range * 3.0f / Mathf.PI + 
                0.5f * (i_range + 1 % 2));
            
            burn_idx = self_idx;

            update_burn_idx(i_range);
            
            if(burn_idx[0] < (max_x - burn_idx[1] % 2) && burn_idx[1] < max_y)
            {
                burn_tile = dummy_get_by_idx();
                float f_increase = 2;
                // burn_tile.increase_burn(f_increase);
            }
        }
    }

    GameObject dummy_get_by_idx()
    {
        return null;
    }

    void update_burn_idx(int range)
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
                    burn_idx[0] += 1;
                    break;

                case 1:
                    burn_idx[0] += off_row;
                    burn_idx[1] += 1;
                    break;
                
                case 2:
                    burn_idx[0] -= 1 - off_row;
                    burn_idx[1] += 1;
                    break;
                
                case 3:
                    burn_idx[0] -= 1;
                    break;
                
                case 4:
                    burn_idx[0] -= 1 - off_row;
                    burn_idx[1] += 1;
                    break;

                case 5:
                    burn_idx[1] -= 1;
                    break;
            }
        }
        else
        {
            switch(neighbor_case)
            {
                case 0:
                    burn_idx[0] += 2;
                    break;
                case 1:
                    burn_idx[0] += 1 + off_row;
                    burn_idx[1] += 1;
                    break;
                case 2:
                    burn_idx[0] += 1;
                    burn_idx[1] += 2;
                    break;
                case 3:
                    burn_idx[1] += 2;
                    break;
                case 4:
                    burn_idx[0] -= 1;
                    burn_idx[1] += 2;
                    break;
                case 5:
                    burn_idx[0] -= 2 + off_row;
                    burn_idx[1] += 1;
                    break;
                case 6:
                    burn_idx[0] -= 2;
                    break;
                case 7:
                    burn_idx[0] -= 2 + off_row;
                    burn_idx[1] -= 1;
                    break;
                case 8:
                    burn_idx[0] -= 1;
                    burn_idx[1] -= 2;
                    break;
                case 9:
                    burn_idx[1] -= 2;
                    break;
                case 10:
                    burn_idx[0] += 1;
                    burn_idx[1] -= 2;
                    break;
                case 11:
                    burn_idx[0] += 1 + off_row;
                    burn_idx[1] -= 1;
                    break;
            }
        }
        
    }
}
