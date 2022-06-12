using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Tile_Map_Editor : MonoBehaviour
{
    public Tile_Map_Data tmd;
    public Tile_Map map;
    public Tile_Set_Save tss;
    public Tile_Set ts;
    public Vector3Int dimensions;
    public float scale = 1;
    public string filename;
    public Dictionary<Vector3Int, Tile_Cell_Editor> tile_editors;
    [HideInInspector]
    public bool settings_foldout;
    public Tile_Template marker_tile;

    public void Initialise() {
        if (tss != null) {
            var templist = transform.Cast<Transform>().ToList();
            foreach (Transform child in templist) {
                DestroyImmediate(child.gameObject);
            }
            ts = new Tile_Set(tss);
            map = new Tile_Map(dimensions.x, dimensions.y, dimensions.z, ts);
            tile_editors = new Dictionary<Vector3Int, Tile_Cell_Editor>();
            for (int x = 0; x < dimensions.x; x++) {
                for (int y = 0; y < dimensions.y; y++) {
                    for (int z = 0; z < dimensions.z; z++) {
                        GameObject go = new GameObject("Cell Editor");
                        go.transform.parent = transform;
                        go.AddComponent(typeof(MeshFilter));
                        go.AddComponent(typeof(MeshRenderer));
                        GameObject go0 = new GameObject("Render");
                        go0.transform.parent = go.transform;
                        GameObject go1 = new GameObject("Marker");
                        go1.transform.parent = go.transform;
                        Tile_Renderer tr = go0.AddComponent(typeof(Tile_Renderer)) as Tile_Renderer;
                        Tile_Renderer marker = go1.AddComponent(typeof(Tile_Renderer)) as Tile_Renderer;
                        Tile_Cell_Editor tec = go1.AddComponent(typeof(Tile_Cell_Editor)) as Tile_Cell_Editor;
                        
                        tec.tr = tr;
                        tec.e = this;
                        Vector3Int v = new Vector3Int(x, y, z);
                        tec.pos = v;
                        tile_editors[v] = tec;
                        Render_Cell(v);
                        marker.Render(marker_tile, new Vector3(v.x*scale, v.y*scale, v.z*scale), new Vector3(scale, scale, scale));
                    }
                }
            }
        }
    }

    public void Render_Cell(Vector3Int v) {
        Tile_Template tt = map.Get_Tile(v);
        Tile_Cell_Editor tec = tile_editors[v];
        tec.t = map.map[v.x, v.y, v.z];
        tec.tr.Render(tt, new Vector3(v.x*scale, v.y*scale, v.z*scale), new Vector3(scale, scale, scale));
    }

    public void On_Update() {
        for (int x = 0; x < dimensions.x; x++) {
            for (int y = 0; y < dimensions.y; y++) {
                for (int z = 0; z < dimensions.z; z++) {
                    Render_Cell(new Vector3Int(x, y, z));
                }
            }
        }
    }

    public void Save() {
        if (filename != null) {
            tmd = map.Save_Data();
            File_Manager.Save_Map_Data(tmd, filename);
        }
    }

    public void Load() {
        if (tmd != null) {
            var templist = transform.Cast<Transform>().ToList();
            foreach (Transform child in templist) {
                DestroyImmediate(child.gameObject);
            }
            tss = tmd.tiles;
            ts = new Tile_Set(tss);
            map = new Tile_Map(tmd);
            tile_editors = new Dictionary<Vector3Int, Tile_Cell_Editor>();
            for (int x = 0; x < dimensions.x; x++) {
                for (int y = 0; y < dimensions.y; y++) {
                    for (int z = 0; z < dimensions.z; z++) {
                        GameObject go = new GameObject("Cell Editor");
                        go.transform.parent = transform;
                        go.AddComponent(typeof(MeshFilter));
                        go.AddComponent(typeof(MeshRenderer));
                        Tile_Renderer tr = go.AddComponent(typeof(Tile_Renderer)) as Tile_Renderer;
                        Tile_Cell_Editor tec = go.AddComponent(typeof(Tile_Cell_Editor)) as Tile_Cell_Editor;
                        tec.tr = tr;
                        tec.e = this;
                        Vector3Int v = new Vector3Int(x, y, z);
                        tec.pos = v;
                        tile_editors[v] = tec;
                        Render_Cell(v);
                    }
                }
            }
        }
    }

}
