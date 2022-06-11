using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public static class File_Manager
{
    public static void Save_Tile_Set(Tile_Set_Save tss, string filename) {
        AssetDatabase.CreateAsset(tss, "Assets/Resources/Tile_Sets/" + filename + ".asset");
    }

    public static Tile_Set Load_Tile_Set(string filename) {
        return new Tile_Set((Tile_Set_Save)AssetDatabase.LoadAssetAtPath("Assets/Resources/Tile_Sets/" + filename + ".asset", typeof(Tile_Set_Save)));
    }

    public static void Save_Tile(Tile_Template tt, string filename) {
        AssetDatabase.CreateAsset(tt, "Assets/Resources/Tiles/" + filename + ".asset");
    }
    
    public static Tile_Template Load_Tile(string filename) {
        return (Tile_Template)AssetDatabase.LoadAssetAtPath("Assets/Resources/Tiles/" + filename + ".asset", typeof(Tile_Template));
    }

    public static void Save_Map_Data(Tile_Map_Data tmd, string filename) {
        AssetDatabase.CreateAsset(tmd, "Assets/Resources/Maps/" + filename + ".asset");
    }

    public static Tile_Map_Data Load_Map_Data(string filename) {
        return (Tile_Map_Data)AssetDatabase.LoadAssetAtPath("Assets/Resources/Maps/" + filename + ".asset", typeof(Tile_Map_Data));
    }

    public static List<DT_Entity_Type> Load_Entity_Types() {
        Object[] obj = Resources.LoadAll("Entity_Types", typeof(GameObject));
        List<DT_Entity_Type> ret = new List<DT_Entity_Type>();
        foreach (Object o in obj) {
            ret.Add(Instantiate((GameObject)o).GetComponent<DT_Entity_Type>());
        }
        return ret;
    }
}
