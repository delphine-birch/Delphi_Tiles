using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Entity_Task_Manager))]
public class Entity_Manager : MonoBehaviour
{
    public List<DT_Entity> entities;
    public List<DT_Object> objects;

    Entity_Task_Manager task_manager;
    
    public void Initialise(Initial_Entity_Data data, Delphi_Tiles dt) {
      entities = new List<DT_Entity>();
      objects = new List<DT_Object>();
      task_manager = GetComponent<Entity_Task_Manager>();
      foreach (DT_Entity_Save dtes in data.entities) { entities.Add(new DT_Entity(dtes, dt)); }
      foreach (DT_Object_Save dtos in data.objects) { objects.Add(new DT_Object(dtos, dt)); }
    }

    public List<Vector3Int> Get_Obstructed() {
      List<Vector3Int> ret = new List<Vector3Int>();
      foreach (DT_Entity e in entities) { ret.Add(e.cell_pos); }
      foreach (DT_Object o in objects) { if (o.obstructive) { ret.Add(Util.V3_to_V3I(o.map_position)); } }
      return ret;
    }
}

public struct Initial_Entity_Data
{
  public List<DT_Entity_Save> entities;
  public List<DT_Object_Save> objects;
}
