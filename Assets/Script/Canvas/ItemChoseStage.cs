using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemChoseStage : ItemChose
{
    public override void BeChosed()
    {
        base.BeChosed();
        MyEvent.ChosedState(transform.localPosition, id);
        DataGame.Set( DataGame.Stage, id);
    }
    public override void NoBeChosed()
    {
        base.NoBeChosed();
    }
}
