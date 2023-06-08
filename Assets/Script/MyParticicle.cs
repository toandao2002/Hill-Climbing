using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyParticicle : MonoBehaviour
{
    public GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    Vector3 smooth = new Vector3(0, 0.3f, 0);
    // Update is called once per frame
    private void FixedUpdate()
    {
        transform.position = target.transform.position -smooth ;
    }
}
