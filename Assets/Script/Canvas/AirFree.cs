using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class AirFree : MonoBehaviour
{
    RectTransform rect;
    public TMP_Text airFree;

    public static AirFree instance;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        rect = gameObject.GetComponent<RectTransform>();
    }
    private void OnEnable()
    {

    }
    public void ShowBackFlip(int val)
    {
        StopAllCoroutines();
        StartCoroutine(show_());
        airFree.text = "Air Free \n+" + val;
    }

    IEnumerator show_()
    {
        rect.DOKill();
        airFree.gameObject.SetActive(true);

        rect.DOScale(1, 0.2f).From(0.3f).SetEase(Ease.InOutBack);
        yield return new WaitForSeconds(1);
        airFree.gameObject.SetActive(false);
    }
}
