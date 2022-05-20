using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

//0: Default Flat
//1: Default Edge
//2: Default Corner
//3: Default Corridor
//4: Default End
//5: Default Full
//6: Default Wall
//7: Default Door
//8: Default Stair
//9: Default Ladder

[System.Serializable]
public class Tile_Set : ScriptableObject
{
    public Dictionary<int, Tile_Template> tiles;
    public Tile_Set() { tiles = new Dictionary<int, Tile_Template>(); }
    public Tile_Set(List<Tile_Template> t, Tile_Template e) {
        tiles = new Dictionary<int, Tile_Template>();
        int count = 0;
        foreach (Tile_Template tt in t) { tiles[count] = tt; count++; }
        tiles[-1] = e;
    }
    public Tile_Set(List<Tile_Set_Struct> t) {
        tiles = new Dictionary<int, Tile_Template>();
        foreach (Tile_Set_Struct tss in t) {
            tiles[t.i] = t.tt;
        }
    }
    public Tile_Template Get_Tile(int n) { return tiles[n]; }
    public void Save(string s) {
        List<Tile_Set_Struct> tiles0 = new List<Tile_Set_Struct>();
        foreach (KeyValuePair<int, Tile_Template> kv in tiles) {
            tiles0.Add(new Tile_Set_Struct { tt = kv.Value, i = kv.Key });
        }
        File_Manager.Save(tiles0, s, "Tile_Set");
    }
}

[System.Serializable]
public struct Tile_Set_Struct {
    public Tile_Template tt;
    public int i;
}
