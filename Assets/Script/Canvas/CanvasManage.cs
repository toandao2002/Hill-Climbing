using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManage : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        float sx = (Screen.width / 720f);
        float sy = (Screen.height / 1280f);
        this.GetComponent<CanvasScaler>().matchWidthOrHeight = (sx < sy ? 0 : 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void _Reset()
    {
        DataGame.resetData();
    }
}
