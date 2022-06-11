using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Entity_Task_Manager))]
public class Entity_Manager : MonoBehaviour
{
    public List<DT_Entity> entities;
    public List<DT_Object> objects;

    public Entity_Task_Manager task_manager;
    
    public void Initialise(Initial_Entity_Data data, List<Task_Starter> starters, List<Task_Completer> completers, Delphi_Tiles dt) {
      entities = new List<DT_Entity>();
      objects = new List<DT_Object>();
      task_manager = gameObject.AddComponent(typeof(Entity_Task_Manager)) as Entity_Task_Manager;
      task_manager.Initialise(starters, completers);
      foreach (DT_Entity_Save dtes in data.entities) { entities.Add(new DT_Entity(dtes, dt)); }
      foreach (DT_Object_Save dtos in data.objects) { objects.Add(new DT_Object(dtos, dt)); }
    }

    public List<Vector3Int> Get_Obstructed() {
      List<Vector3Int> ret = new List<Vector3Int>();
      foreach (DT_Entity e in entities) { ret.Add(e.cell_pos); }
      foreach (DT_Object o in objects) { if (o.obstructive) { ret.Add(Util.V3_to_V3I(o.map_position)); } }
      return ret;
    }

    public Initial_Entity_Data Save_Entity_Data() {
      Initial_Entity_Data save = ScriptableObject.CreateInstance<Initial_Entity_Data>();
      save.entities = new List<DT_Entity_Save>();
      save.object = new List<DT_Object_Save>();
      foreach (DT_Entity e in entities) { save.entities.Add(e.Save()); }
      foreach (DT_Object o in objects) { save.objects.Add(o.Save()); }
      return save;
    }

    public DT_Entity Get_Entity_By_Name(string name) {
      foreach(DT_Entity e in entities) {
        if (e.name == name) { return e; }
      }
    }

    public List<DT_Entity> Get_Entities_By_Type(string type) {
      List<DT_Entity> ret = new List<DT_Entity>();
      foreach (DT_Entity e in entities) {
        if (e.type == type) { ret.Add(e); }
      }
      return ret;
    }
}

[System.Serializable]
public struct Initial_Entity_Data : ScriptableObject
{
  public List<DT_Entity_Save> entities;
  public List<DT_Object_Save> objects;
}
