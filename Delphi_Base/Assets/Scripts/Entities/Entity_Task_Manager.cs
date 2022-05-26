using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity_Task_Manager : MonoBehaviour {

    public List<Task_Starter> starters;
    public List<Task_Completer> completers;

    void Initialise() {
        starters = Default_Task_Handler.Get_Starters();
        completers = Default_Task_Handler.Get_Completers();
    }

    public void Tick_Start(DT_Entity e, Delphi_Tiles dt) {
        if (e.task_queue.Count < 1) { return; }
        Entity_Task current = e.task_queue[0];
        if (!current.started) { starters[current.type](e, current, dt); current.started = true; }
    }

    public void Tick_End(DT_Entity e, Delphi_Tiles dt) {
        if (e.task_queue.Count < 1) { return; }
        Entity_Task current = e.task_queue[0];
        current.tick_length--;
        if (current.tick_length <= 0) { completers[current.type](e, current, dt); e.task_queue.RemoveAt(0); }
    }

} 

public class Entity_Task
{
    public Vector3Int target;
    public int type; // 0 = Move
    public int tick_length;
    public int total_tick_length;
    public bool started = false;
}