using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Render_Manager : MonoBehaviour
{
    public Dictionary<Vector3Int, Tile_Renderer> tile_renderers;
    public List<Entity_Renderer> entity_renderers;
    public void Initialise(Delphi_Tiles dt) {
        Generate_Map_Renderers(dt.map.origin, new Vector3(dt.map.scale, dt.map.scale, dt.map.scale), dt.map.tile_map);
    }

    public void Tick_Update(float tick1, float tick2) {
        foreach(Entity_Renderer er in entity_renderers) {
            AnimatorControllerParameter[] parameters = er.an.parameters;
            for (int i = 0; i < er.an.parameterCount; i++) {
                int m = er.parameter_mask[i];
                if (m == 1) { er.an.SetFloat(parameters[i].name, tick1); }
                else if (m == 2) { er.an.SetFloat(parameters[i].name, tick2); }
            }
        }
    }
    
    public void Generate_Map_Renderers(Vector3 origin, Vector3 scale, Tile_Map tile_map) {
        tile_renderers = new Dictionary<Vector3Int, Tile_Renderer>();
        for (int i = 0; i < tile_map.dim.x; i++) {
            for (int j = 0; j < tile_map.dim.y; j++) {
                for(int k = 0; k < tile_map.dim.z; k++) {
                    Vector3Int c = new Vector3Int(i, j, k);
                    GameObject go = new GameObject("Tile_Renderer");
                    go.transform.parent = transform;
                    go.AddComponent(typeof(MeshFilter));
                    go.AddComponent(typeof(MeshRenderer));
                    tile_renderers[c] = go.AddComponent(typeof(Tile_Renderer)) as Tile_Renderer;
                    tile_renderers[c].Render(tile_map.Get_Tile(c), origin + (Vector3)c*scale.x, scale*100);
                }
            }
        }
    }

    public Entity_Renderer Generate_Entity_Renderer(DT_Entity dte) {
        GameObject go = new GameObject("Entity Renderer: " + dte.entity_name);
        go.transform.parent = transform;
        go.AddComponent(typeof(MeshFilter));
        go.AddComponent(typeof(MeshRenderer));
        go.AddComponent(typeof(Animator));
        Entity_Renderer er = go.AddComponent(typeof(Entity_Renderer)) as Entity_Renderer;
        er.Initialise(dte, dte.entity_template);
        entity_renderers.Add(er);
        return er;
    }
}
