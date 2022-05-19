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
}
