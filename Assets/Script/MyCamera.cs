using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCamera : MonoBehaviour
{
    public static MyCamera Instance;
    public GameObject Target;
    public Vector3 Dis;
    bool OK = false;
    private void Awake()
    {
        if (Instance == null) Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void SetTarGet(GameObject gTarget)
    {
        if (OK && GameController.instance.ModeGameOnline) return;
        OK = true;
        Target = gTarget;
        Dis = transform.position - Target.transform.position;
    }
    private void LateUpdate()
    {
        if (OK && Target != null)
            transform.position = Dis + Target.transform.position;
    }
}
