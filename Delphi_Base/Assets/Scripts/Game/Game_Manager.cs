using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Manager : MonoBehaviour {

    Delphi_Tiles dt;
    public Tile_Map_Data tmd;
    public Initial_Entity_Data ied;

    DT_Entity active;

    int turn_count;

    public void Awake() {
        GameObject tiles = new GameObject("Delphi Tiles");
        dt = tiles.AddComponent(typeof(Delphi_Tiles)) as Delphi_Tiles;
        dt.Initialise(tmd, ied, Default_Task_Handler.Get_Starters(), Default_Task_Handler.Get_Completers());
    }
}