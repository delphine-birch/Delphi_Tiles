using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DT_Entity 
{
    public int ID;
    public string entity_name;
    public string entity_type;
    public DT_Entity_Type entity_template;
    public Entity_Renderer render;
    public Vector3 pos;
    public Vector3Int cell_pos;
    public bool lock_to_cell;
    public List<Entity_Task> task_queue;

    public DT_Entity(DT_Entity_Save dtes, Delphi_Tiles dt) {
        ID = dtes.ID;
        entity_name = dtes.name;
        entity_type = dtes.type;
        entity_template = dt.entities.entity_types[dtes.type];
        pos = new Vector3(dtes.x, dtes.y, dtes.z);
        cell_pos = dt.map.World_To_Cell_Pos(pos);
        lock_to_cell = true;
        task_queue = new List<Entity_Task>();
        render = dt.render.Generate_Entity_Renderer(this);
    }

    public DT_Entity(DT_Entity_Type dtet, int i, string n, Vector3 p, Delphi_Tiles dt) {
        ID = i;
        entity_name = n;
        entity_type = dtet.type_name;
        entity_template = dtet;
        pos = p;
        cell_pos = dt.map.World_To_Cell_Pos(p);
        lock_to_cell = true;
        task_queue = new List<Entity_Task>();
        render = dt.render.Generate_Entity_Renderer(this);
    }

    public DT_Entity_Save Save() {
        return new DT_Entity_Save { ID = ID, name = entity_name, type = entity_type, x = pos.x, y = pos.y, z = pos.z };
    }
}
public struct DT_Entity_Save
{
    public int ID;
    public string name;
    public string type;
    public float x;
    public float y;
    public float z;
}
    
    
    
