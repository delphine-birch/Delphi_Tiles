using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public static class File_Manager
{
    public static void Save_Tile_Set(Tile_Set_Save tss, string filename) {
        AssetDatabase.CreateAsset(tss, "Assets/Data/Tile_Sets/" + filename + ".asset");
    }

    public static Tile_Set Load_Tile_Set(string filename) {
        return new Tile_Set((Tile_Set_Save)AssetDatabase.LoadAssetAtPath("Assets/Data/Tile_Sets/" + filename + ".asset", typeof(Tile_Set_Save)));
    }

    public static void Save_Tile(Tile_Template tt, string filename) {
        AssetDatabase.CreateAsset(tt, "Assets/Data/Tiles/" + filename + ".asset");
    }
    
    public static Tile_Template Load_Tile(string filename) {
        return (Tile_Template)AssetDatabase.LoadAssetAtPath("Assets/Data/Tiles/" + filename + ".asset", typeof(Tile_Template));
    }

    public static void Save_Map_Data(Tile_Map_Data tmd, string filename) {
        AssetDatabase.CreateAsset(tmd, "Assets/Data/Maps/" + filename + ".asset");
    }

    public static Tile_Map_Data Load_Map_Data(string filename) {
        return (Tile_Map_Data)AssetDatabase.LoadAssetAtPath("Assets/Data/Maps/" + filename + ".asset", typeof(Tile_Map_Data));
    }
}
