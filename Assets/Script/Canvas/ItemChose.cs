using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;
using System;

public class ItemChose : MonoBehaviour
{
    public MyButton Btn;
    public TMP_Text Name;
    public TMP_Text Coin;
    public GameObject Main;
    public GameObject Lock;
    public int id;
    public float duration = 0.2f;
    public bool active = false;
    private void Start()
    {
        if (DataGame.Get(DataGame.ActiveStage+ id) == 1)
        {
            Lock.SetActive(false);
            active = true;
        }
        else
        {
            Lock.SetActive(true);
        }
        Btn.onClick.AddListener(BeChosed);
    }

    public virtual void BeChosed()
    {
        
        {
            
        }
        
       
     
         
    }
    public virtual void NoBeChosed()
    {
        gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(350, 200);
        Main.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);

    }
}
