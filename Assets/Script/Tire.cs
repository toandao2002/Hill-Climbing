using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Tire : MonoBehaviour
{
    // Start is called before the first frame update
    public bool IsOwner;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator CountTime()
    {
        yield return new WaitForSeconds(1);
        int valCoin = 25;
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            if (GameController.instance.GameLose) yield break ;
            AirFree.instance.ShowBackFlip(valCoin );
            ManangeAudio.Instacne.PlaySound(NameSound.Coin);
            MyEvent.IncCoin?.Invoke(valCoin);
            if (valCoin >= 100) valCoin += 100;
            else if (valCoin >= 500) valCoin += 500;
            else 
            valCoin += 25;
        }

    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (!IsOwner) return;
        if (collision.gameObject.CompareTag("Ground"))
        {
          
            StopAllCoroutines();
            StartCoroutine(CountTime());
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!IsOwner) return;
        if (collision.gameObject.CompareTag("Ground"))
        {
            StopAllCoroutines();
        }
    }
}
