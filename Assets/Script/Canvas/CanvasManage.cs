using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManage : MonoBehaviour
{
    public static CanvasManage instance;
    public float witdh;
    private void Awake()
    {
        instance = this;
        float sx = (Screen.width / 720f);
        float sy = (Screen.height / 1280f);

        this.GetComponent<CanvasScaler>().matchWidthOrHeight = (sx < sy ? 0 : 1);
        witdh = GetComponent<RectTransform>().rect.width;
    }
    // Start is called before the first frame update
    void Start()
    {
        
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
