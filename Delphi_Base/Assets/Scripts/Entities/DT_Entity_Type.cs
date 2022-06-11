using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DT_Entity_Type : MonoBehaviour {
    public string type_name;
    public string type_key;
    public AnimatorController animator;
    public Mesh mesh;
    public Materials[] materials;
    public int[] parameter_mask //0: Static, 1: Ticks since last Tick, 2: Ticks since last Turn
}