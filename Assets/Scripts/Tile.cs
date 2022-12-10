using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public enum TileType{
        GRASS,
        WOOD,
        WATER
    }
    public TileType type;
}
