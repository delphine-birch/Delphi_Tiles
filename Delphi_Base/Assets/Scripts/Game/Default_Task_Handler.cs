using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate int Task_Starter(DT_Entity e, Entity_Task t, Delphi_Tiles dt);
public delegate int Task_Completer(DT_Entity e, Entity_Task t, Delphi_Tiles dt);

public static class Default_Task_Handler {
    public static int Move_Starter(DT_Entity e, Entity_Task t, Delphi_Tiles dt) {
        e.render.moving = true;
        e.render.target = t.target;
        return 1;
    }
    public static int Move_Completer(DT_Entity e, Entity_Task t, Delphi_Tiles dt) {
        e.render.moving = false;
        e.render.cell_pos = t.target;
        return 1;
    }
    public static List<Task_Starter> Get_Starters() {
        List<Task_Starter> starters = new List<Task_Starter>();
        starters.Add(Move_Starter);
        return starters;
    }
    public static List<Task_Completer> Get_Completers() {
        List<Task_Completer> completers = new List<Task_Completer>();
        completers.Add(Move_Completer);
        return completers;
    }
}