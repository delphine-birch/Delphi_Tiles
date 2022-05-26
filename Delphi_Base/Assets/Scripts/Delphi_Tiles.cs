using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Delphi_Tiles : MonoBehaviour {
  public Map_Manager map;
  public Entity_Manager entities;
  public Render_Manager render;
  public Game_Manager game;

  public float tick_count;
  public float ticks_per_second;
  public int tick_mode = 1;
  public bool tick_on;
  
  public Tile_Map_Data map_data;
  public Initial_Entity_Data entity_data;
  
  void Awake() {
    map.Initialise(map_data);
    entities.Initialise(entity_data, this);
    render.Initialise(this);
    game.Initialise(this);
  }

  void Update() {
    if (Input.GetKeyDown("space") && !tick_on) { Begin_Tick(); }

    if (tick_on) {
      float delta = Time.deltaTime;
      float delta_tick = ticks_per_second * delta;
      float new_tick = tick_count + delta_tick;
      if (tick_mode == 1) {
        if (new_tick > Mathf.Floor(tick_count) + 1) {
          tick_count = Mathf.Floor(tick_count) + 1;
          Tick_Complete();
          tick_on = false;
        } else {
          tick_count = new_tick;
        }
      }
      if (tick_mode == 2) {
        if (new_tick > Mathf.Floor(tick_count) + 1) {
          Tick_Complete();
          Tick_Start();
        }
        tick_count = new_tick;
      }
    }
  }

  void Begin_Tick() { tick_on = true; Tick_Start(); }

  void Tick_Start() {
    game.Tick();
    entities.task_manager.Tick_Start(game.Get_Active_Entities(), this);
  }

  void Tick_Complete() {
    entities.task_manager.Tick_Complete(game.Get_Active_entities(), this);
  }
}
