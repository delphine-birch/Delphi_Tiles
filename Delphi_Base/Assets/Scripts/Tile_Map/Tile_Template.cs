using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//NOTE: All Tiles are assumed to be 1 by 1 by 1 - please ensure provided Meshes match these dimensions.
[System.Serializable]
[CreateAssetMenu()]
public class Tile_Template : ScriptableObject
{
    public Mesh mesh;
    public Vector3 euler_rotation;
    public Vector3 scale = Vector3.one;
    public float height_offset = 0;
    int n_submeshes;
    public Material[] materials;
    [SerializeField]
    public float[] access;
    public bool open;

    public Tile_Template(Mesh m, Vector3 rot, Material[] mat, float[] a) {
        mesh = m;
        euler_rotation = rot;
        materials = mat;
        n_submeshes = m.subMeshCount;
        access = a;
        open = true;
        if (n_submeshes < materials.Length) { Debug.Log("TILE ERROR: Not enough materials provided for number of submeshes."); }
    }

    public Tile_Template(Mesh m, Vector3 rot, Material[] mat) {
        mesh = m;
        euler_rotation = rot;
        materials = mat;
        n_submeshes = m.subMeshCount;
        access = new float[27];
        open = true;
    }

    public Tile_Template() {
        mesh = null;
        euler_rotation = Vector3.zero;
        materials = new Material[0];
        n_submeshes = 0;
        access = new float[27];
        open = true;
    }

    public Tile_Instance[] Instancing_Data() {
        Tile_Instance[] tiles = new Tile_Instance[n_submeshes];
        Mesh m = new Mesh();
        Quaternion r = Quaternion.Euler(euler_rotation.x, euler_rotation.y, euler_rotation.z);
        m.vertices = mesh.vertices;
        for (int i = 0; i < m.vertices.Length; i++) {
            m.vertices[i] = r*mesh.vertices[i];
        }
        m.triangles = mesh.triangles;
        m.uv = mesh.uv;
        m.RecalculateNormals();
        for (int i = 0; i < n_submeshes; i++) {
            Tile_Instance ti;
            ti.mesh = mesh;
            ti.submesh = i;
            ti.material = materials[i];
            tiles[i] = ti;
        }
        return tiles;
    }

    float[] rotate3x3(float[] inp) {
        float[] o = new float[27];
        o[0] = inp[2];
        o[1] = inp[5];
        o[2] = inp[8];
        o[3] = inp[1];
        o[4] = inp[4];
        o[5] = inp[7];
        o[6] = inp[0];
        o[7] = inp[3];
        o[8] = inp[6];

        o[9] = inp[11];
        o[10] = inp[14];
        o[11] = inp[17];
        o[12] = inp[10];
        o[13] = inp[13];
        o[14] = inp[16];
        o[15] = inp[9];
        o[16] = inp[12];
        o[17] = inp[15];

        o[18] = inp[20];
        o[19] = inp[23];
        o[20] = inp[26];
        o[21] = inp[19];
        o[22] = inp[22];
        o[23] = inp[25];
        o[24] = inp[18];
        o[25] = inp[21];
        o[26] = inp[24];

        return o;
    }

    public Tile_Template Rotation(int n) {
        Vector3 rot = euler_rotation + new Vector3(0, n*90, 0);
        Tile_Template t = new Tile_Template(mesh, rot, materials);
        float[] new_access = access;
        for (int i = 0; i < n; i++) {
            new_access = rotate3x3(new_access);
        }
        t.access = new_access;
        t.scale = scale;
        t.height_offset = height_offset;
        return t;
    }

    public void Close() { open = false; }
    public void Open() { open = true; }
    public bool Is_Open() { return open; }

    public float Get_Access(Vector3Int v) {
        if (!(v.x == 1 || v.x == -1 || v.x == 0) ||
            !(v.y == 1 || v.y == -1 || v.y == 0) ||
            !(v.z == 1 || v.z == -1 || v.z == 0)) {
                Debug.Log("VECTOR ACCESS ERROR: Trying to get non-simple directional vector access.");
                return -1f;
            }
        else {
            return access[v.x + 1 + (v.y + 1)*9 + (v.z + 1)*3];
        }
    }

    public List<float> Get_Access_List(List<Vector3Int> vectors) {
        List<float> floats = new List<float>();
        foreach (Vector3Int v in vectors) {
            if (!(v.x == 1 || v.x == -1 || v.x == 0) ||
                !(v.y == 1 || v.y == -1 || v.y == 0) ||
                !(v.z == 1 || v.z == -1 || v.z == 0)) {
                Debug.Log("VECTOR ACCESS ERROR: Trying to get non-simple directional vector access.");
                return new List<float>();
            }
            else { floats.Add(access[v.x + 1 + (v.y + 1)*9 + (v.z + 1)*3]); }
        }
        return floats;
    }

    public void Set_Inaccessible() {
        for (int i = 0; i < 3; i++) {
            for (int j = 0; j < 3; j++) {
                for (int k = 0; k < 3; k++) {
                    access[i + (j)*9 + (k)*3] = -1;
                }
            }
        }
    }

    public void Set_Fully_Accessible(float d) {
        for (int i = 0; i < 3; i++) {
            for (int j = 0; j < 3; j++) {
                for (int k = 0; k < 3; k++) {
                    access[i + (j)*9 + (k)*3] = d;
                }
            }
        }
    }

    public void Set_NESW_Accessible(bool[] nesw, float d) {
        // NESW is [+Z, +X, -Z, -X]
        Set_Inaccessible();
        if (nesw[0]) { access[16] = d; }
        if (nesw[1]) { access[14] = d; }
        if (nesw[2]) { access[10] = d; }
        if (nesw[3]) { access[12] = d; }
    }

    public void Set_Vec_Accessible(Vector3Int v, float d) {
        if (!(v.x == 1 || v.x == -1 || v.x == 0) ||
            !(v.y == 1 || v.y == -1 || v.y == 0) ||
            !(v.z == 1 || v.z == -1 || v.z == 0)) {
                Debug.Log("VECTOR ACCESS ERROR: Trying to set non-simple directional vector access.");
            }
        else {
            access[v.x + 1 + (v.y + 1)*9 + (v.z + 1)*3] = d;
        }
    }
}

public struct Tile_Instance {
    public Mesh mesh;
    public int submesh;
    public Material material;
}