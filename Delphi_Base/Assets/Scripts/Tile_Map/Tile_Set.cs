using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

//0 Empty
//1 Default Floor
//2 Default Corridor NS
//3 Default Corridor EW
//4, 5, 6, 7 Default Corner NESW (Sides on North and West)
//8, 9, 10, 11 Default Edge NESW (Side on North)
//12, 13, 14, 15 Default End NESW (End on North)
//15, 16, 17, 18 Default Door NESW (Exit on North)

[System.Serializable]
public class Tile_Set : ScriptableObject
{
    public Dictionary<int, Tile_Template> tiles;
    public List<Tile_Template> tiles_list;
    public Tile_Set() { tiles = new Dictionary<int, Tile_Template>(); }
    public Tile_Set(List<Tile_Template> t) {
        tiles = new Dictionary<int, Tile_Template>();
        int count = 0;
        foreach (Tile_Template tt in t) { tiles[count] = tt; count++; }
        tiles[-1] = e;
        Update_List();
    }
    public Tile_Set(List<Tile_Set_Struct> t) {
        tiles = new Dictionary<int, Tile_Template>();
        foreach (Tile_Set_Struct tss in t) {
            tiles[tss.i] = tss.tt;
        }
        Update_List();
    }
    public Tile_Template Get_Tile(int n) { return tiles[n]; }
    public void Save(string s) {
        Update_Dict();
        List<Tile_Set_Struct> tiles0 = new List<Tile_Set_Struct>();
        foreach (KeyValuePair<int, Tile_Template> kv in tiles) {
            tiles0.Add(new Tile_Set_Struct { tt = kv.Value, i = kv.Key });
        }
        Debug.Log(tiles0.Count);
        File_Manager.Save_Tile_Set(new Tile_Set_Save(tiles0), s);
    }

    void Normalize_Dict() {
        int count = 0;
        Dictionary<int, Tile_Template> n = new Dictionary<int, Tile_Template>();
        foreach (KeyValuePair<int, Tile_Template> kv in tiles) {
            n[count]  = kv.Value;
            count++;
        }
        tiles = n;
    }

    public void Update_List() {
        tiles_list = new List<Tile_Template>();
        Normalize_Dict();
        for (int i = 0; i < tiles.Count; i++) {
            tiles_list.Add(tiles[i]);
        }
    }

    public void Update_Dict() {
        tiles = new Dictionary<int, Tile_Template>();
        for (int i = 0; i < tiles_list.Count; i++) {
            tiles[i] = tiles_list[i];
        }
    }
}

[System.Serializable]
public struct Tile_Set_Struct {
    public Tile_Template tt;
    public int i;
}
