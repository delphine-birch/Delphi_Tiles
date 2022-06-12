using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[System.Serializable]
public class Tile_Map_Data : ScriptableObject
{
    public int dimx;
    public int dimy;
    public int dimz;
    public int[] map;
    public Tile_Set_Save tiles;
    public List<Tile_Connection_Save> special_connections;
    public int default_tile;
    public void Set_Data(Vector3Int d, int[] m, Tile_Set_Save t, int dt, Tile_Connection_Graph sc) {
        dimx = d.x;
        dimy = d.y;
        dimz = d.z;
        map = m;
        tiles = t;
        default_tile = dt;
        special_connections = sc.Save();
    }
}