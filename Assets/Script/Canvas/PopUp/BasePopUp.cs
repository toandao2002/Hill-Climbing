using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class BasePopUp : MonoBehaviour
{
    public GameObject Main;
    public void Hide()
    {
        if (Main != null)
        {
            Main.SetActive(false);
        }
        else 
        gameObject.SetActive(false);
    }
    public void Show()
    {
        if (Main != null)
        {
            Main.SetActive(true);
        }
        else
            gameObject.SetActive(true);
    }
}
