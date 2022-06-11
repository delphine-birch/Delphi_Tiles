using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Manager : MonoBehaviour {

    Delphi_Tiles dt;
    public Tile_Map_Data tmd;
    public Initial_Entity_Data ied;
    public Dictionary<string, Creature_Type> creature_types;
    public List<Creature> creatures;

    public float ticks_per_second;

    Creature active;
    int active_index;
    bool active_turn;
    float active_turn_start;
    float active_tick_start;

    int turn_count = 0;

    public void Awake() {
        GameObject tiles = new GameObject("Delphi Tiles");
        dt = tiles.AddComponent(typeof(Delphi_Tiles)) as Delphi_Tiles;
        dt.Initialise(tmd, ied, Default_Task_Handler.Get_Starters(), Default_Task_Handler.Get_Completers());

        creature_types = new Dictionary<string, Creature_Type>();
        creatures = new List<Creature>();

        Object[] obj = Resources.LoadAll("Creature_Types", typeof(Creature_Type_Save));
        foreach(Object o in obj) {
            Creature_Type ct = (Creature_Type)o;
            creature_types[ct.type_key] = ct;
        }

        foreach(DT_Entity dte in dt.entities.entities) {
            Creature c = new Creature(dte);
            creatures.Add(c);
        }
        creatures.Sort();

        active_index = creatures.Count - 1;
        active = creatures[active];
    }

    public void Next_Turn() {
        if (active.dte.task_queue.Count > 0) {
            active_turn = true;
            active_turn_start = Time.time;
            active_tick_start = Time.time;
            dt.entities.task_manager.Tick_Start(new List<DT_Entity> { active.dte }, dt);
        }
        else {
            active_index--;
            if (active_index < 0) { active_index = creatures.Count - 1; }
            active = creatures[active_index];
        }
    }

    public void Update() {
        if (active_turn) {
            dt.render.Tick_Update(ticks_per_second*(Time.time - active_tick_start), ticks_per_second*(Time.time - active_turn_start))
            if ((Time.time - active_tick_start)*ticks_per_second > 1) {
                active_tick_start = Time.time;
                dt.entities.task_manager.Tick_End(new List<DT_Entity> { active.dte }, dt);
                if (dt.task_queue.Count < 1) {
                    active_index--;
                    if (active_index < 0) { active_index = creatures.Count - 1; }
                    active = creatures[active_index];
                    active_turn = false;
                }
            }
            
        } else {
            dt.render.Tick_Update(0, 0);
        }
    }
}