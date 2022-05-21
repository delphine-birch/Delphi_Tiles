using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Tile_Set_Editor : MonoBehaviour
{
    public List<Tile_Template> tiles;
    public float scale;
    public Vector3Int origin;
    bool updated = true;

    void Update() {
        if (updated) {
            foreach (Transform child in transform) {
                DestroyImmediate(child.gameObject);
            }
            int x = 0;
            int y = 0;
            foreach (Tile_Template tt in tiles) {
                x++;
                if (x > 8) { x = 0; y++; }
                GameObject go = new GameObject("Tile_Renderer");
                go.transform.parent = transform;
                go.AddComponent(typeof(MeshFilter));
                go.AddComponent(typeof(MeshRenderer));
                Tile_Renderer r = go.AddComponent(typeof(Tile_Renderer)) as Tile_Renderer;
                r.Render(tt, origin + new Vector3(x, 0, y)*scale, new Vector3(scale, scale, scale)*100);
            }
            updated = false;
        }
    }
}
