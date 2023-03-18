using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    public List<Transform> pos;
   
    public LineRenderer line;
    
    // Start is called before the first frame update
    void Start()
    {
        line.SetVertexCount(pos.Count);
      
    }

    private void FixedUpdate()
    {
        for (int i =0; i < pos.Count; i++)
        {
            line.SetPosition(i, pos[i].localPosition);
      
        }
      
    }
}
