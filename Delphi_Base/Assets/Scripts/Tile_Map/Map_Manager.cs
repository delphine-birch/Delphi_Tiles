using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map_Manager : MonoBehaviour
{
    public Tile_Map tile_map;
    public Tile_Set tile_set;
    public Dictionary<Vector3Int, Tile_Renderer> renderers;
    public Vector3 origin;
    public float scale;
    
    public void Initialise(Tile_Map_Data tmd) { 
        tile_map = new Tile_Map(tmd);
        tile_set = tmd.tiles;
    }

    public Vector3Int World_To_Cell_Pos(Vector3 p) {
        Vector3 n = ((p + origin)*scale);
        return new Vector3Int(Mathf.RoundToInt(n.x), Mathf.RoundToInt(n.y) - 1, Mathf.RoundToInt(n.z));
    }

    public Vector3 Map_To_World_Pos(Vector3 p) {
        return (p/scale) - origin;
    }

    public Vector3 Cell_To_World_Pos(Vector3Int p) {
        return Map_To_World_Pos(new Vector3(p.x, p.y, p.z));
    }
}
