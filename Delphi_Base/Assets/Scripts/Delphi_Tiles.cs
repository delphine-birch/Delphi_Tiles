using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Delphi_Tiles : MonoBehaviour {
  public Map_Manager map;
  public Entity_Manager entities;
  public Render_Manager render;
  
  public Tile_Map_Data map_data;
  public Initial_Entity_Data entity_data;

  void Initialise(Tile_Map_Data tmd, Initial_Entity_Data ied, List<Task_Starter> starters, List<Task_Completer> completers) {
    GameObject m = new GameObject("Map");
    GameObject e = new GameObject("Entities");
    GameObject r = new GameObject("Rendering");
    m.transform.parent = transform;
    e.transform.parent = transform;
    r.transform.parent = transform;
    map = m.AddComponent(typeof(Map_Manager)) as Map_Manager;
    entities = e.AddComponent(typeof(Entity_Manager)) as Entity_Manager;
    render = r.AddComponent(typeof(Render_Manager)) as Render_Manager;
    map.Initialise(tmd);
    entities.Initialise(ied, this, starters, completers);
    render.Initialise(this);
  }
}
