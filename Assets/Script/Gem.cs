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
        ManangeAudio.Instacne.PlaySound( NameSound.Coin);
        MyEvent.IncGen?.Invoke(Gem_Value);

    }
}
