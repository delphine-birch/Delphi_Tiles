using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DT_Entity 
{
    public int ID;
    public string entity_name;
    public Entity_Renderer render;
    public Vector3 pos;
    public Vector3Int cell_pos;
    public bool lock_to_cell;
    List<Entity_Task> task_queue;
    
    public DT_Entity(int i, string n, Vector3 p, Delphi_Tiles dt) {
        ID = i;
        entity_name = n;
        pos = p;
        cell_pos = dt.map.World_To_Cell_Pos(p);
        lock_to_cell = true;
        task_queue = new List<Entity_Task>();
    }

    public DT_Entity(DT_Entity_Save dtes, Delphi_Tiles dt) {
        ID = dtes.ID;
        entity_name = dtes.name;
        pos = new Vector3(dtes.x, dtes.y, dtes.z);
        cell_pos = dt.map.World_To_Cell_Pos(pos);
        lock_to_cell = true;
        task_queue = new List<Entity_Task>();
    }

    public DT_Entity_Save Save() {
        return new DT_Entity_Save { ID = ID, name = entity_name, x = pos.x, y = pos.y, z = pos.z };
    }
}

public class Entity_Task
{
    public Vector3Int target;
    public int type; // 0 = Move
    public int tick_length;
}

public struct DT_Entity_Save
{
    public int ID;
    public string name;
    public float x;
    public float y;
    public float z;
}
    
    
    
