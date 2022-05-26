using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//North is +Z, East is +X, Up is +Y

public class Tile_Map
{
    public Vector3Int dim;
    int[,,] map;
    Tile_Set tiles;
    Tile_Connection_Graph graph;
    int default_tile = 0;
    Tile_Connection_Graph special_connections;

    public Tile_Map(Tile_Map_Data tmd) {
        dim = new Vector3Int(tmd.dimx, tmd.dimy, tmd.dimz);
        map = Construct_Dimensions(tmd.map, dim);
        tiles = new Tile_Set(tmd.tiles.tiles);
        default_tile = tmd.default_tile;
        special_connections = new Tile_Connection_Graph(tmd.special_connections);
        Graph_Gen();
    }

    public Tile_Map(int x, int y, int z, Tile_Set t) {
        dim = new Vector3Int(x, y, z);
        map = new int[x, y, z];
        tiles = t;
        special_connections = new Tile_Connection_Graph();
        Clear_Map();
        Graph_Gen();
    }

    public Tile_Map() {
        dim = new Vector3Int(20, 20, 20);
        map = new int[20, 20, 20];
        tiles = new Tile_Set();
        special_connections = new Tile_Connection_Graph();
        Clear_Map();
        Graph_Gen();
    }

    public List<Tile_Instance_Data> Get_Instance_Data(Vector3 origin, float scale) {
        Dictionary<int, List<Vector3>> loc = new Dictionary<int, List<Vector3>>();
        foreach (KeyValuePair<int, Tile_Template> kv in tiles.tiles) {
            loc[kv.Key] = new List<Vector3>();
        }
        for (int i = 0; i < dim.x; i++) {
            for (int j = 0; j < dim.y; j++) {
                for (int k = 0; k < dim.z; k++) {
                    int v = map[i,j,k];
                    if (v != -1) { loc[v].Add(new Vector3(i*scale + origin.x, j*scale + origin.y, k*scale + origin.z)); }
                }
            }
        }
        List<Tile_Instance_Data> data = new List<Tile_Instance_Data>();
        foreach (KeyValuePair<int, List<Vector3>> kv in loc) {
            Tile_Instance[] ti = tiles.tiles[kv.Key].Instancing_Data();
            Matrix4x4[] matrices = new Matrix4x4[kv.Value.Count];
            int count = 0;
            foreach (Vector3 v in kv.Value) { matrices[count] = Matrix4x4.TRS(v, Quaternion.Euler(0, 0, 0), new Vector3(scale, scale, scale)); count++; }
            for (int i = 0; i < ti.Length; i++) { data.Add(new Tile_Instance_Data { data = ti[i], transforms = matrices }); }
        }
        return data;
    }

    void Clear_Map() {
        for (int i = 0; i < dim.x; i++) {
            for (int j = 0; j < dim.y; j++) {
                for (int k = 0; k < dim.z; k++) {
                    map[i, j, k] = -1;
                }
            }
        }
    }
    
    int[] Deconstruct_Dimensions(int[,,] m, Vector3Int d) {
        int[] ret = new int[d.x*d.y*d.z];
        for (int i = 0; i < d.x; i++) {
            for (int j = 0; j < d.y; j++) {
                for (int k = 0; k < d.z; k++) {
                    ret[i + d.x*(j + d.y*k)] = m[i, j, k];
                }
            }
        }
        return ret;
    }
    
    int[,,] Construct_Dimensions(int[] m, Vector3Int d) {
        int[,,] ret = new int[d.x, d.y, d.z];
        for (int i = 0; i < d.x; i++) {
            for (int j = 0; j < d.y; j++) {
                for (int k = 0; k < d.z; k++) {
                    ret[i, j, k] = m[i + d.x*(j + d.y*k)];
                }
            }
        }
        return ret;
    }
    
    public void Set_Tile_Set(Tile_Set ts) { tiles = ts; }

    public Tile_Template Get_Tile(Vector3Int v) {
        int i;
        try { i = map[v.x, v.y, v.z]; }
        catch (System.IndexOutOfRangeException ex) { i = -1; Debug.Log("COORDINATE ERROR: Trying to get tile outside of map range.\n" + ex.Message); }
        if (i == -1) { i = default_tile; }
        return tiles.Get_Tile(i);
    }

    public bool Is_Open(Vector3Int v) {
        return Get_Tile(v).open;
    }

    void Set_Tile(Vector3Int v, int t) {
        try { map[v.x, v.y, v.z] = t; }
        catch (System.IndexOutOfRangeException ex) { Debug.Log("COORDINATE ERROR: Trying to set tile outside of map range.\n" + ex.Message); }
    }

    void Graph_Gen() {
        graph = new Tile_Connection_Graph();
        for (int i = 0; i < dim.x; i++) {
            for (int j = 0; j < dim.y; j++) {
                for (int k = 0; k < dim.z; k++) {
                    Vector3Int v = new Vector3Int(i, j, k);
                    graph.Initialise(v);
                    Auto_Connect(v, 1);
                }
            }
        }
        foreach (KeyValuePair<Vector3Int, List<Tile_Connection>> kv in special_connections.connections) {
            foreach (Tile_Connection tc in kv.Value) { graph.Add_Connection(kv.Key, tc); }
        }
    }

    void Connect(Vector3Int a, Vector3Int b, float w) {
        Tile_Connection atc;
        Tile_Connection btc;
        atc.coords = b;
        btc.coords = a;
        atc.weight = w;
        btc.weight = w;
        graph.Add_Connection(a, atc);
        graph.Add_Connection(b, btc);
    }
    
    void Asym_Connect(Vector3Int a, Vector3Int b, float awb, float bwa) {
        Tile_Connection atc;
        Tile_Connection btc;
        atc.coords = b;
        btc.coords = a;
        atc.weight = awb;
        btc.weight = bwa;
        graph.Add_Connection(a, atc);
        graph.Add_Connection(b, btc);
    }

    void Special_Connect(Vector3Int a, Vector3Int b, float awb, float bwa) {
        Tile_Connection atc;
        Tile_Connection btc;
        atc.coords = b;
        btc.coords = a;
        atc.weight = awb;
        btc.weight = bwa;
        special_connections.Add_Connection(a, atc);
        special_connections.Add_Connection(b, btc);
    }

    void Auto_Connect(Vector3Int a, float w) {
        Tile_Template tt = Get_Tile(a);
        for (int x = -1; x < 2; x++) {
            for (int y = -1; y < 2; y++) {
                for (int z = -1; z < 2; z++) {
                    Vector3Int offset = new Vector3Int(x, y, z);
                    if (!(offset.x == 0 && offset.y == 0 && offset.z == 0)) {
                        Vector3Int v = a + offset;
                        if (v.x >= 0 && v.x < dim.x && v.y >= 0 && v.y < dim.y && v.z >= 0 && v.z < dim.z) {
                            Tile_Connection tc;
                            tc.coords = v;
                            tc.weight = tt.Get_Access(offset);
                            graph.Add_Connection(a, tc);
                        }
                    }
                }
            }
        }
    }
    public Tile_Map_Data Save_Data() {
        return new Tile_Map_Data(dim, Deconstruct_Dimensions(map, dim), tiles, default_tile, special_connections);
    }
}

[System.Serializable]
public struct Tile_Connection
{
    public Vector3Int coords;
    public float weight;
}

[System.Serializable]
public class Tile_Map_Data
{
    public int dimx;
    public int dimy;
    public int dimz;
    public int[] map;
    public Tile_Set_Save tiles;
    public List<Tile_Connection_Save> special_connections;
    public int default_tile;
    public Tile_Map_Data(Vector3Int d, int[] m, Tile_Set_Save t, int dt, Tile_Connection_Graph sc) {
        dim = d;
        map = m;
        tiles = t;
        default_tile = dt;
        special_connections = sc.Save();
    }
}

public struct Tile_Instance_Data
{
    public Tile_Instance data;
    public Matrix4x4[] transforms;
}


