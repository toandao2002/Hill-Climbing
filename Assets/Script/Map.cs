using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class Map : MonoBehaviour
{
    public   SpriteShapeController spr;
    public List<GameObject> itemPref;
    public GameObject parItem;
    // Start is called before the first frame update
    void Start()
    {
        int posX = 2;

        spr.spline.Clear();
        spr.spline.InsertPointAt(0, new Vector3(0 * 2, 3, 0));
        spr.spline.SetTangentMode(0, ShapeTangentMode.Broken);
        
        for (int i = 1; i<100; i++)
        {
            posX +=  Random.Range(7, 14);
            spr.spline.InsertPointAt(i,new Vector3(posX , Random.Range(3,7), 0));
            if (_ramdom(0.6f))
            {
                var v = spr.spline.GetPosition(i) - spr.spline.GetPosition(i-1 );
                float deg = Vector3.Angle( v,Vector3.right );
                if (v.y < 0) deg = -deg;
                var _item = Instantiate(itemPref[Random.Range(0, 2)], parItem.transform);
                _item.transform.position = spr.spline.GetPosition(i - 1) + new Vector3(0,0.6f,0);
                _item.transform.localRotation = Quaternion.Euler(0, 0, deg);
            }
            
 
        }
       
        spr.spline.InsertPointAt(100, new Vector3(posX+ 2, 0, 0));
        spr.spline.SetTangentMode(100, ShapeTangentMode.Broken);
        spr.spline.InsertPointAt(101, new Vector3(0 * 2, 0, 0));
        spr.spline.SetTangentMode(101, ShapeTangentMode.Broken);
        spr.autoUpdateCollider = true ;
        for (int i = 1; i < 100; i++)
        {
           
            spr.spline.SetTangentMode(i, ShapeTangentMode.Continuous);
            spr.spline.SetLeftTangent(i, new Vector3(-1, 0, 0));
            spr.spline.SetRightTangent(i, new Vector3(1, 0, 0));
        }
      
    }
    public bool  _ramdom(float v)
    {
        return Random.Range(0, 1) >= v;
       
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
