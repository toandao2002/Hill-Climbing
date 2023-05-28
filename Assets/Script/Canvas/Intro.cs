using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class Intro : MonoBehaviour
{
    public Image bar;
    // Start is called before the first frame update
    void Start()
    {
        bar.DOFillAmount(1, 2.5f).From(0).OnComplete(()=> {
            
            float width = Screen.width;
            gameObject.GetComponent<RectTransform>().DOAnchorPosX(-width, 1).OnComplete(()=> { 
               SceneManager.LoadScene("MainMenu");
            
            });
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void load()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
