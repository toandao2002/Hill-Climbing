using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;
public class ItemChose : MonoBehaviour
{
    public MyButton Btn;
    public TMP_Text Name;
    public TMP_Text Coin;
    public GameObject Main;
    public int id;
    public float duration = 0.2f;
    private void Start()
    {
        Btn.onClick.AddListener(BeChosed);
    }

    public virtual void BeChosed()
    {
        gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(450, 200);
        Main.transform.DOScale(1.9f, duration).From(1.5f);
         
    }
    public virtual void NoBeChosed()
    {
        gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(350, 200);
        Main.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);

    }
}
