using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Man : MonoBehaviour
{
    public CarController carController;
    public bool touchedGround = false;
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
            if (!touchedGround)
            {
                touchedGround = true;
            }
            else
            {
                return;
            }
            GameController.instance.GameLose = true;
            DOVirtual.DelayedCall(3, () => {
                if((carController.CheckIsOwner()|| !GameController.instance.ModeGameOnline))
                    MyEvent.GameLose?.Invoke();
                 
                

            });
            int count = transform.childCount;
            for (int i = count - 1; i >= 0; i--)
            {
                transform.GetChild(i).gameObject.AddComponent<Rigidbody2D>();
                transform.GetChild(i).SetParent(null);
            }
            GetComponent<BoxCollider2D>().enabled = false;
        
        }
    }
}
