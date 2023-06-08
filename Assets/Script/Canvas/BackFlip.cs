using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BackFlip : MonoBehaviour
{
    RectTransform rect;
    public GameObject backFlip;
  
    public static BackFlip instance;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        rect = backFlip.GetComponent<RectTransform>();
    }
    private void OnEnable()
    {
        
    }
    public void ShowBackFlip()
    {
        StopAllCoroutines();
        StartCoroutine(show_());
    }
 
    IEnumerator show_()
    {
        rect.DOKill();
        backFlip.SetActive(true);

        rect.DOScale(1, 0.2f).From(0.2f).SetEase(Ease.InOutBack);
        yield return new WaitForSeconds(1);
        backFlip.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
