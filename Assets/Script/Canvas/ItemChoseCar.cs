using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemChoseCar : ItemChose
{
    public override void BeChosed()
    {
        base.BeChosed();
        MyEvent.ChosedCar?.Invoke(transform.localPosition, id);
        DataGame.SetCar(id);
    }
    public override void NoBeChosed()
    {
        base.NoBeChosed();
    }
}
