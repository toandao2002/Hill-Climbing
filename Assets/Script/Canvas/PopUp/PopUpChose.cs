using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PopUpChose : BasePopUp
     /* // 2
       , IDragHandler
       , IPointerEnterHandler*/
  
    ,IPointerExitHandler
{
    public GameObject content;
    public Transform PosCenter;
    bool PoiterMove = false;
    // Start is called before the first frame update
    void Start()
    {
        HorizontalLayoutGroup layoutGroup = content.GetComponent<HorizontalLayoutGroup>();
        layoutGroup.padding.left = (int)CanvasManage.instance.witdh / 2;
        layoutGroup.padding.right = (int) CanvasManage.instance.witdh / 2 ;
        Debug.Log(CanvasManage.instance.witdh / 2);
    }
    private void OnEnable()
    {
        MyEvent.ChosedCar += updatePosContent;
        MyEvent.ChosedState += updatePosContent;
    }
    private void OnDisable()
    {
        MyEvent.ChosedCar -= updatePosContent;
        MyEvent.ChosedState -= updatePosContent;
    }
    void updatePosContent(Vector3 posItem , int id)
    {
       
        content.transform.localPosition =   new Vector3(CanvasManage.instance.witdh / 2 - posItem.x + 350 / 2, content.transform.localPosition.y,0);
        for (int i = 0 ;i < content.transform.childCount; i ++){
            if (i != id)
            {
                content.transform.GetChild(i).GetComponent<ItemChose>().NoBeChosed();
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
       /* for (int i = 0; i < content.transform.childCount; i++)
        {
            if (Mathf.Abs(content.transform.GetChild(i).position.x - PosCenter.position.x) < 175)
            {
                content.transform.GetChild(i).GetComponent<ItemChose>().BeChosed();
            }
        }*/
    }
}
