using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity_Manager : MonoBehaviour
{
    public List<DT_Entity> entities;
    public List<DT_Object> objects;
    
    public void Initialise(Initial_Entity_Data data) {
      entities = data.entities;
      objects = data.objects;
    }
}

public struct Initial_Entity_Data
{
  public List<DT_Entity> entities;
  public List<DT_Object> objects;
}
