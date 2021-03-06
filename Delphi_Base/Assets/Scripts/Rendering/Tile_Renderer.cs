using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class Tile_Renderer : MonoBehaviour
{
    public Tile_Template tile;
    public Vector3 pos;
    public Vector3 scale;
    MeshFilter mf;
    MeshRenderer mr;
    void OnEnable()
    {
        mf = GetComponent<MeshFilter>();
        mr = GetComponent<MeshRenderer>();
    }

    public void Render(Tile_Template t, Vector3 p, Vector3 s)
    {
        OnEnable();
        tile = t;
        pos = p;
        scale = s;
        mf.mesh = tile.mesh;
        mr.materials = tile.materials;
        transform.position = pos;
        transform.localScale = new Vector3(scale.x*t.scale.x, scale.y*t.scale.y, scale.z*t.scale.z);
        transform.rotation = Quaternion.Euler(tile.euler_rotation);
    }

}
