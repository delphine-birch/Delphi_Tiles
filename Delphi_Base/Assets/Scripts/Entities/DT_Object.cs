using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DT_Object {
    public int ID;
    public string type;
    public Vector3 map_position;
    public bool lock_to_cell;
    public bool obstructive;
    public Object_Renderer object_renderer;

    public DT_Object(int i, string t, Vector3 mp, bool o, Delphi_Tiles dt) {
        ID = i;
        type = t;
        map_position = mp;
        lock_to_cell = false;
        obstructive = o;
    }

    public DT_Object(DT_Object_Save dtos, Delphi_Tiles dt) {
        ID = dtos.ID;
        type = dtos.type;
        map_position = new Vector3(dtos.x, dtos.y, dtos.z);
        lock_to_cell = false;
        obstructive = dtos.o;
    }

    public DT_Object_Save Save() {
        return new DT_Object_Save { ID = ID, type = type, x = map_position.x, y = map_position.y, z = map_position.z, o = obstructive };
    }

    public void Object_Update() {
        if (lock_to_cell) { map_position = Util.V3_Round(map_position); }
        object_renderer.map_location = map_position;
        object_renderer.Render();
    }

    public void Set_Pos(Vector3 v) {
        map_position = v;
    }
    public void Set_Scale(float s) {
        object_renderer.scale = s;
    }
    public void Set_Rotation(Vector3 v) {
        object_renderer.rotation = v;
    }
    public void Set_Obstructive(bool b) {
        obstructive = b;
    }
}

public struct DT_Object_Save {
    public int ID;
    public string type;
    public float x;
    public float y;
    public float z;
    public bool o;
}