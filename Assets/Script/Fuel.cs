using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fuel : Item
{
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
            MyEvent.ChangecFuel?.Invoke(1);
        }
    }
}
