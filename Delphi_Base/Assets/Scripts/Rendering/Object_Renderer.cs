using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class Object_Renderer : MonoBehaviour
{
    public Mesh mesh;
    public Material[] materials;
    public Vector3 map_location;
    public float scale;
    public Vector3 euler_rotation;

    public void Render(Map_Manager map) {
        MeshFilter mf = GetComponent<MeshFilter>();
        MeshRenderer mr = GetComponent<MeshRenderer>();
        mf.mesh = mesh;
        mr.materials = materials;
        transform.position = map.Map_To_World_Pos(map_location);
        transform.localScale = new Vector3(scale, scale, scale);
        transform.rotation = Quaternion.Euler(euler_rotation.x, euler_rotation.y, euler_rotation.z);
    }
}