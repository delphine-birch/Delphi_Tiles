using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Tile_Renderer))]
public class Tile_Cell_Editor : MonoBehaviour
{
    public Tile_Renderer tr;
    public int t;
    public Tile_Map_Editor e;
    public Vector3Int pos;

    void OnValidate() {
        if (e != null) { e.map.Set_Tile(pos, t); e.Render_Cell(pos); }
    }
}
