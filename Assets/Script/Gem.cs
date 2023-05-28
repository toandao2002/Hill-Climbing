using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : Item
{
    public int Gem_Value;
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
   
        if (collision.gameObject.CompareTag("Player"))
        {
            ManangeAudio.Instacne.PlaySound(NameSound.Coin);
            if (collision.GetComponent<CarController>().IsOwner || !GameController.instance.ModeGameOnline)
                        MyEvent.IncGen?.Invoke(Gem_Value);
        }
            
        else if (collision.gameObject.CompareTag("Man"))
            if (collision.transform.parent.GetComponent<CarController>().IsOwner || !GameController.instance.ModeGameOnline)
            {
                ManangeAudio.Instacne.PlaySound(NameSound.Coin);
                MyEvent.IncGen?.Invoke(Gem_Value);
            }
    }
}
