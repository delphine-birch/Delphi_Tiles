using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DT_Entity 
{
    public int ID;
    public string entity_name;
    public Entity_Renderer renderer;
    public Vector3 pos;
    public Vector3Int cell_pos;
    public bool lock_to_cell;
    List<Entity_Task> task_queue;
    
    public DT_Entity(int i, string n, Vector3 p, Delphi_Tiles dt) {
        ID = i;
        entity_name = n;
        pos = p;
        cell_pos = dt.World_To_Cell_Pos(p);
        lock_to_cell = true;
        task_queue = new List<Entity_Task>();
    }
}

public class Entity_Task
{
    public Vector3Int target;
    public int type; // 0 = Move
    public int tick_length;
}
    
    
    
