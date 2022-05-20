using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity_Manager : MonoBehaviour
{
    public List<DT_Entity> entities;
    
    public void Initialise(Initial_Entity_Data data) {
      entities = dt.entities;
    }
}

public struct Initial_Entity_Data
{
  public List<DT_Entity> entities;
}
