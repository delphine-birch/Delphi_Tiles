using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature {
    public DT_Entity dte;
    Creature_Type creature_type;
    public int hp;
    public int dp;
    public int initiative;
    public int range;

    public Creature(Creature_Type ct, int ID, string n, Vector3Int pos, Delphi_Tiles dt) {
        dte = new DT_Entity(dtet, ID, n, pos, dt);
        creature_type = ct;
        hp = ct.hp;
        dp = ct.dp;
        initiative = ct.initiative;
        range = ct.range;
    }

    public Creature(DT_Entity d, Dictionary<string, Creature_Type> types) {
        dte = d;
        creature_type = types[d.entity_template.type_key];
        hp = ct.hp;
        dp = ct.dp;
        initiative = ct.initiative;
        range = ct.range;
    }

    public int CompareTo(Creature c) {
        return this.initiative.CompareTo(c.initiative);
    }
}

public class Creature_Type {
    public DT_Entity_Type dtet;
    public int hp;
    public int dp;
    public int initiative;
    public int range;
    public string type_name;
    public string type_key;

    public Creature_Type(DT_Entity_Type d, int h, int a, int i, int r) {
        hp = h;
        dp = a;
        dtet = d;
        initiative = i;
        range = r;
        type_name = d.type_name;
        type_key = d.type_key;
    }

    public Creature_Type_Save Save() {
        Creature_Type_Save save = ScriptableObject.CreateInstance<Creature_Type_Save>();
        save.type_name = type_name;
        save.type_key = type_key;
        save.hp = hp;
        save.dp = dp;
        save.initiative = initiative;
        save.range = range;
    }
}

public class Creature_Type_Save : ScriptableObject {
    string type_name;
    string type_key;
    int hp;
    int dp;
    int initiative;
    int range;
}