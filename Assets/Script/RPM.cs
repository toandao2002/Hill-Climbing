using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPM : MonoBehaviour
{
    public static RPM instacne;
    public GameObject bar;
    RectTransform rec;
    public float Zr;
    
    // Start is called before the first frame update
    void Start()
    {
      
        instacne = this;
        rec = bar.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        rec.localRotation =  Quaternion.Euler(0,0,(Zr/6000) * 270);
       
    }
}
