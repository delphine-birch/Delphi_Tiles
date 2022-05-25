using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Tile_Set_Save : ScriptableObject {
    public List<Tile_Set_Struct> tiles;
    public Tile_Set_Save(List<Tile_Set_Struct> tss) { tiles = tss; }
}
