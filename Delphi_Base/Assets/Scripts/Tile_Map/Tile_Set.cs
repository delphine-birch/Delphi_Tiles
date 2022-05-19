using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[System.Serializable]
public class Tile_Set : ScriptableObject
{
    public Dictionary<int, Tile_Template> tiles;
    public Tile_Set() { tiles = new Dictionary<int, Tile_Template>(); }
    public Tile_Set(Dictionary<int, Tile_Template> t) { tiles = t; }
    public Tile_Set(List<Tile_Template> t) {
        tiles = new Dictionary<int, Tile_Template>();
        int count = 0;
        foreach (Tile_Template tt in t) { tiles[count] = tt; count++; }
    }
    public Tile_Template Get_Tile(int n) { return tiles[n]; }
}
