using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class Tile_Access_Editor : MonoBehaviour
{
    public Vector3Int vec;
    public float weight = -1;
    public bool access = false;
    public Material on;
    public Material off;

    public void OnValidate() {
        if (!access) { weight = -1; }
        MeshRenderer mr = GetComponent<MeshRenderer>();
        if (weight != -1) { mr.material = on; } else { mr.material = off; }
    }
}
