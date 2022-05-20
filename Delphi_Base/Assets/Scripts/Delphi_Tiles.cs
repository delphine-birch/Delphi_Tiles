using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Delphi_Tiles : MonoBehaviour {
  public Map_Manager map;
  public Entity_Manager entities;
  public Render_Manager renderer;
  
  public Tile_Map_Data map_data;
  public Initial_Entity_Data entity_data;
  
  void Awake() {
    map.Initialise(map_data);
    entities.Initialise(entity_data);
    renderer.Initialise(this);
  }
}
