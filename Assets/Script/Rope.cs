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
        v = new List<Vector2>();
        line.SetVertexCount(pos.Count);
        for (int i = 0; i < pos.Count; i++)
        {
            v.Add(new Vector2());

        }

    }
    List<Vector2> v;
    private void FixedUpdate()
    {
        for (int i = 0; i < pos.Count; i++)
        {
            line.SetPosition(i, pos[i].localPosition);
            v[i] = pos[i].localPosition;
        }
        line.GetComponent<EdgeCollider2D>().SetPoints(v);
    }
}
