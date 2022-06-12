using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[ExecuteInEditMode]
public class Tile_Set_Editor : MonoBehaviour
{
    public float scale;
    public Vector3Int origin;
    public string filename;
    public Tile_Set tileset;
    public Tile_Set_Save save;
    [HideInInspector]
    public bool settings_foldout;

    public void On_Settings_Update() {
        var templist = transform.Cast<Transform>().ToList();
        foreach (var child in templist) {
            DestroyImmediate(child.gameObject);
        }
        int x = 0;
        int y = 0;
        foreach (Tile_Template tt in tileset.tiles_list) {
            GameObject go = new GameObject("Tile_Renderer");
            go.transform.parent = transform;
            go.AddComponent(typeof(MeshFilter));
            go.AddComponent(typeof(MeshRenderer));
            Tile_Renderer r = go.AddComponent(typeof(Tile_Renderer)) as Tile_Renderer;
            r.Render(tt, origin + new Vector3(x, 0, y)*scale, new Vector3(scale, scale, scale));
            x++;
            if (x > 7) { x = 0; y++; }
        }
        tileset.Update_Dict();
    }
}
