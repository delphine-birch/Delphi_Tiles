using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public static class File_Manager
{
    public static void Save_List<T>(List<T> l, string filename, string location) {
        AssetDatabase.CreateAsset(l, "Assets/" + location + "/" + filename + ".asset");
    }
    
    public static void Save_Tile(Tile_Template tt, string filename) {
        AssetDatabase.CreateAsset(tt, "Assets/Tiles/" + filename + ".asset");
    }
    
    public static Tile_Template Load_Tile(string filename) {
        return (Tile_Template)AssetDatabase.LoadAssetAtPath("Assets/Tiles/" + filename + ".asset", typeof(Tile_Template));
    }
}
