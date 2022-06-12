using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class Tile_Template_Editor : MonoBehaviour {
    public Tile_Template tile;
    [HideInInspector]
    public bool tile_settings_foldout;
    public MeshFilter mf;
    public MeshRenderer mr;
    public string filename;
    [SerializeField]
    public List<GameObject> access_editor;
    void OnEnable() {
        mf.mesh = tile.mesh;
        mr.materials = tile.materials;
        transform.rotation = Quaternion.Euler(tile.euler_rotation);
    }

    void OnValidate() {
        On_Settings_Update();
    }

    bool vector_equals(Vector3Int v, Vector3Int v0) {
        if (v.x == v0.x && v.y == v0.y && v.z == v0.z) { return true; }
        else { return false; }
    }

    Tile_Access_Editor Get_Access_Editor(Vector3Int v) {
        foreach (GameObject go in access_editor) {
            if (vector_equals(go.GetComponent<Tile_Access_Editor>().vec, v)) { return go.GetComponent<Tile_Access_Editor>(); }
        }
        return access_editor[0].GetComponent<Tile_Access_Editor>();
    }

    public void On_Settings_Update() {
        mf.mesh = tile.mesh;
        mr.materials = tile.materials;
        transform.rotation = Quaternion.Euler(tile.euler_rotation);
        transform.localScale = tile.scale;
        for (int x = -1; x < 2; x++) {
            for (int y = -1; y < 2; y++) {
                for (int z = -1; z < 2; z++) {
                    Vector3Int v = new Vector3Int(x, y, z);
                    Tile_Access_Editor tae = Get_Access_Editor(v);
                    tae.weight = tile.Get_Access(v);
                    if (tile.Get_Access(v) == -1) { tae.access = false; } else { tae.access = true; }
                    tae.OnValidate();
                }
            }
        }
    }

    public void Save_Tile() {
        File_Manager.Save_Tile(tile, filename);
        tile = File_Manager.Load_Tile(filename);
    }

    public void Update_Access() {
        tile.Set_Inaccessible();
        for (int x = -1; x < 2; x++) {
            for (int y = -1; y < 2; y++) {
                for (int z = -1; z < 2; z++) {
                    Vector3Int v = new Vector3Int(x, y, z);
                    if (!(v.x == 0 && v.y == 0 && v.z == 0)) { tile.Set_Vec_Accessible(v, Get_Access_Editor(v).weight); }
                }
            }
        }
    }

    public void Gen_Rotations() {
        Tile_Template t1 = tile.Rotation(1);
        Tile_Template t2 = tile.Rotation(2);
        Tile_Template t3 = tile.Rotation(3);
        string p1 = "Assets/Tiles/" + filename + "_r90" + ".asset";
        string p2 = "Assets/Tiles/" + filename + "_r180" + ".asset";
        string p3 = "Assets/Tiles/" + filename + "_r270" + ".asset";
        File_Manager.Save_Tile(t1, filename + "_r90");
        File_Manager.Save_Tile(t2, filename + "_r180");
        File_Manager.Save_Tile(t3, filename + "_r270");
    }

}
