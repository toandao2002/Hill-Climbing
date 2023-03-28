using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Man : MonoBehaviour
{
  
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            // game lose
            DOVirtual.DelayedCall(3 ,()=> {
                MyEvent.GameLose?.Invoke();
                
                });
            int count = transform.childCount;
            for (int i =  count-1; i >=0 ; i--)
            {
                transform.GetChild(i).gameObject.AddComponent<Rigidbody2D>();
                transform.GetChild(i).SetParent(null);
            }
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
