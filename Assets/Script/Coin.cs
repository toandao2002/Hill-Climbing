using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Item
{
    public int Score ;
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void OnTriggerEnter2D(Collider2D collision)
    {

        base.OnTriggerEnter2D(collision);
        ManangeAudio.Instacne.PlaySound(NameSound.Coin);
        if (collision.gameObject.CompareTag("Player")) { 

            if (collision.GetComponent<CarController>().IsOwner)
                MyEvent.IncCoin?.Invoke(Score); 
        }
            else if (collision.gameObject.CompareTag("Man"))
                if (collision.transform.parent.GetComponent<CarController>().IsOwner)
                {
                    MyEvent.IncCoin?.Invoke(Score);
                }
    }
}
