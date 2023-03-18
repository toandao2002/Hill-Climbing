using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Item : MonoBehaviour
{
    Animator Ani;
    public GameObject Icon;
    public float Duration= 2f;
    public BoxCollider2D boxCol;
    // Start is called before the first frame update
    public virtual void  Start()
    {
        Ani = GetComponent<Animator>();
        boxCol = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void EffetTakeItem()
    {
        Icon.transform.DOLocalMoveY(3, Duration).OnComplete(()=>
        {
            gameObject.SetActive(false);
        });
        Icon.GetComponent<SpriteRenderer>().DOFade(0, Duration);
    }
    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            EffetTakeItem();
            boxCol.enabled = false;
        }
    }
     
}
