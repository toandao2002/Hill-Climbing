using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemChoseCar : ItemChose
{
    public override void BeChosed()
    {
        base.BeChosed();

        if (!active)
        {

            if (DataGame.GetCoin() >= Int32.Parse(Coin.text))
            {
                DataGame.Set(DataGame.ActiveStage + id, 1);
                DataGame.SetCoin(DataGame.GetCoin() - Int32.Parse(Coin.text));
                MyEvent.IncCoin?.Invoke(DataGame.GetCoin());
                Lock.SetActive(false);
                active = true;
            }
            else
            {
                return;
            }
        }
        else
        {
            gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(450, 200);
            Main.transform.DOScale(1.9f, duration).From(1.5f);
            MyEvent.ChosedCar?.Invoke(transform.localPosition, id);
            DataGame.SetCar(id);
        }
       
    }
    public override void NoBeChosed()
    {
        base.NoBeChosed();
    }
}
