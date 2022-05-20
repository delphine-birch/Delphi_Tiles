using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map_Manager : MonoBehaviour
{
    public Tile_Map tile_map;
    public Tile_Set tile_set;
    public Dictionary<Vector3Int, Tile_Renderer> renderers;
    
    public void Initialise(Tile_Map_Data tmd) { 
        tile_map = new Tile_Map(tmd);
        tile_set = tmd.tile_set;
    }

    public void Generate_Renderers(Vector3 origin, Vector3 scale) {
        renderers = new Dictionary<Vector3Int, Tile_Renderer>();
        for (int i = 0; i < tile_map.dim.x; i++) {
            for (int j = 0; j < tile_map.dim.y; j++) {
                for(int k = 0; k < tile_map.dim.z; k++) {
                    Vector3Int c = new Vector3Int(i, j, k);
                    GameObject go = new GameObject("Tile_Renderer");
                    go.transform.parent = transform;
                    go.AddComponent(typeof(MeshFilter));
                    go.AddComponent(typeof(MeshRenderer));
                    renderers[c] = go.AddComponent(typeof(Tile_Renderer)) as Tile_Renderer;
                    renderers[c].Render(tile_map.Get_Tile(c), origin + (Vector3)c*scale.x, scale*100);
                }
            }
        }
    }
}
