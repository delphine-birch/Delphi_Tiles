using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Tile_Connection_Graph
{
    public Dictionary<Vector3Int, List<Tile_Connection>> connections;
    public Tile_Connection_Graph() { connections = new Dictionary<Vector3Int, List<Tile_Connection>>(); }
    public Tile_Connection_Graph(Dictionary<Vector3Int, List<Tile_Connection>> c) { connections = c; }
    public void Clear() { connections = new Dictionary<Vector3Int, List<Tile_Connection>>(); }
    public void Initialise(Vector3Int v) { connections[v] = new List<Tile_Connection>(); }
    public void Add_Connection(Vector3Int v, Tile_Connection tc) { if (!connections.ContainsKey(v)) { Initialise(v); } connections[v].Add(tc); }
    public List<Tile_Connection> Get_Connections(Vector3Int v) { if (connections.ContainsKey(v)) { 
        return connections[v]; 
    } else { 
        Debug.Log("COORDINATE ERROR: No Connections from this Coordinate."); return new List<Tile_Connection>(); 
    }}
    public Vector3Int min(List<Vector3Int> heap, Dictionary<Vector3Int, float> score) {
        Vector3Int min = heap[0];
        float min0 = score[min];
        foreach (Vector3Int v in heap) { if (score[v] < min0) { min0 = score[v]; min = v; } }
        return min;
    }
    public float Get_Score(Dictionary<Vector3Int, float> scores, Vector3Int v) {
        if (scores.ContainsKey(v)) { return scores[v]; }
        else { return 100000000; }
    }
    public List<Vector3Int> Construct_Path(Dictionary<Vector3Int, Vector3Int> connect, Vector3Int end) {
        List<Vector3Int> path = new List<Vector3Int>();
        path.Add(new Vector3Int(end.x, end.y, end.z));
        Vector3Int current = end;
        while (connect.ContainsKey(current)) {
            current = connect[current];
            path.Insert(0, new Vector3Int(current.x, current.y, current.z));
        }
        return path;
    }
    public List<Vector3Int> Get_Path(Vector3Int a, Vector3Int b, int max_iter=1000) {
        List<Vector3Int> open = new List<Vector3Int>();
        open.Add(a);
        Dictionary<Vector3Int, Vector3Int> came_from = new Dictionary<Vector3Int, Vector3Int>();
        Dictionary<Vector3Int, float> g_score = new Dictionary<Vector3Int, float>();
        Dictionary<Vector3Int, float> f_score = new Dictionary<Vector3Int, float>();
        g_score[a] = 0;
        f_score[a] = (b - a).magnitude;
        
        int iter = 0;
        while (open.Count != 0 && iter < max_iter) {
            iter++;
            Vector3Int current = min(open, fscore);
            if (V3I_Equals(current, b)) { return Construct_Path(came_from, current); }
            open.Remove(current);
            List<Tile_Connection> connections = Get_Connections(current);
            foreach (Tile_Connection tc in connections) {
                float tgscore = gscore[current] = tc.weight;
                if (tgscore < gscore[tc.coords]) {
                    came_from[tc.coords] = current;
                    gscore[tc.coords] = tgscore;
                    fscore[tc.coords] = tgscore + (b - tc.coords).magnitude;
                    if (!open.Contains(tc.coords)) { open.Add(tc.coords)); }
                }
            }
        }
        return new List<Vector3Int>();
    }          
}
