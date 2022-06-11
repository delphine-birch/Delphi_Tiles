using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity_Task_Manager : MonoBehaviour {

    public List<Task_Starter> starters;
    public List<Task_Completer> completers;

    void Initialise(List<Task_Starter> s, List<Task_Completer> c) {
        starters = s;
        completers = c;
    }

    public void Tick_Start(List<DT_Entity> el, Delphi_Tiles dt) {
        foreach (DT_Entity e in el) {
            if (e.task_queue.Count < 1) { continue; }
            Entity_Task current = e.task_queue[0];
            if (!current.started) { starters[current.type](e, current, dt); current.started = true; }
        }
    }

    public void Tick_End(List<DT_Entity> el, Delphi_Tiles dt) {
        foreach (DT_Entity e in el) {
            if (e.task_queue.Count < 1) { continue; }
            Entity_Task current = e.task_queue[0];
            current.tick_length--;
            if (current.tick_length <= 0) { completers[current.type](e, current, dt); e.task_queue.RemoveAt(0); }
        }
    }

    public void Push_Task(List<DT_Entity> el, Entity_Task t) {
        foreach (DT_Entity e in el) {
            e.task_queue.Add(t);
        }
    }

    public void Insert_Task(List<DT_Entity> el, Entity_Task t, int i = 0) {
        foreach (DT_Entity e in el) {
            if (i <= e.task_queue.Count) { e.task_queue.Insert(i, t); }
        }
    }

    public void Clear_Tasks(List<DT_Entity> el) {
        foreach (DT_Entity e in el) {
            e.task_queue = new List<Entity_Task>();
        }
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