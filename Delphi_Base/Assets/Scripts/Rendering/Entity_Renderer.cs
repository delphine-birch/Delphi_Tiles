using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(Animator))]
public class Entity_Renderer : MonoBehaviour
{
    public Vector3Int cell_pos;
    public Vector3Int target;
    public bool moving;
    Mesh mesh;
    Materials materials;
    AnimatorController animator;

    MeshFilter mf;
    MeshRenderer mr;
    Animator an;

    public void Initialise(DT_Entity dte, DT_Entity_Type dtet) {
        cell_pos = dte.cell_pos;
        target = dte.cell_pos;
        moving = false;
        mesh = dtet.mesh;
        materials = dtet.materials;
        animator = dtet.animator;

        mf = this.GetComponent<MeshFilter>();
        mr = this.GetComponent<MeshRenderer>();
        an = this.GetComponent<Animator>();

        mf.mesh = mesh;
        mr.materials = materials;
        an.runtimeAnimatorController = animator;
    }
    
}
