using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Render_Manager : MonoBehaviour
{
    public void Initialise(Delphi_Tiles dt) {
        Generate_Map_Renderers(dt.Map_Manager.tile_map);
    }
    
    public void Generate_Map_Renderers(Vector3 origin, Vector3 scale, Tile_Map tile_map) {
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
