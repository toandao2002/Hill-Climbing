using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class UpgratePopUp : BasePopUp
{
    public TMP_Text Name;
    public TMP_Text content;
    public TMP_Text State;
    public MyButton Close;
    public MyButton upgrate;
    public Tool _tool;
    int id;
    // Start is called before the first frame update
    void Start()
    {
        Close.onClick.AddListener(_hide);
        upgrate.onClick.AddListener(Upgrate);
    }
    public void SetData(Tool tool)
    {
        _tool = tool;
        Name.text = tool.nameTune.ToString();
        content.text = tool.content;
        id = DataGame.Get(_tool.nameTune.ToString() + DataGame.GetCar());
        if (DataGame.GetCoin() >= _tool.Coin[id])
        {
            State.text = "Upgrate";
        }
        else
        {
            State.text = "GetCoin";
        }
    }
    public void Upgrate()
    {
        int num_level = DataGame.Get(_tool.nameTune.ToString() + DataGame.GetCar());

        if (_tool.Coin.Count <= num_level+1) return;
        if (DataGame.GetCoin() >= _tool.Coin[DataGame.Get(_tool.nameTune.ToString() +DataGame.GetCar())])
        {

          

            DataGame.SetCoin(DataGame.GetCoin ()  - _tool.Coin[DataGame.Get(_tool.nameTune.ToString() +DataGame.GetCar())]);
            DataGame.Set(_tool.nameTune.ToString() + DataGame.GetCar(), DataGame.Get(_tool.nameTune.ToString() + DataGame.GetCar()) +1);
            handleUpgrate();
            MyEvent.IncCoin?.Invoke(DataGame.GetCoin());
            MyEvent.ChangeLevelTune?.Invoke();
        }
    }
    void handleUpgrate()
    {
        if (_tool.nameTune == NameTune.Engine)
        {
            handleUpgrateEngine();
        }
        else if (_tool.nameTune == NameTune.Tire)
        {
            handleUpgrateTire();
        }
        else if (_tool.nameTune == NameTune.Suspension)
        {
            handleUpgrateSuspension();
        }
        else if (_tool.nameTune == NameTune.Downforce)
        {
            handleUpgrateDownForce();
        }
        _hide();



    }
    void handleUpgrateEngine()
    {
        DataGame.Set(DataGame.CarToolEngine + DataGame.GetCar(), DataGame.Get(DataGame.CarToolEngine+ DataGame.GetCar()) + _tool.Engine[id]);
        Debug.Log(DataGame.CarToolEngine + DataGame.GetCar());
        Debug.Log(_tool.Engine[id]);
    }
    void handleUpgrateTire()
    {
        DataGame.SetF(DataGame.CarToolTire + DataGame.GetCar(), DataGame.GetF(DataGame.CarToolTire+ DataGame.GetCar()) + _tool.Tire[id]);
    }
    void handleUpgrateSuspension()
    {
        DataGame.Set(DataGame.CarToolSuspension + DataGame.GetCar(), DataGame.Get(DataGame.CarToolSuspension+ DataGame.GetCar()) + _tool.Suspension[id]);
    }
    void handleUpgrateDownForce()
    {
        DataGame.Set(DataGame.CarToolDownForce + DataGame.GetCar(), DataGame.Get(DataGame.CarToolDownForce+ DataGame.GetCar()) + _tool.DownForce[id]);
    }
    public void _hide()
    {
       
        Main.transform.DOScale(0, 0.2f).From(1).SetEase(Ease.InOutSine).OnComplete(()=> { 
             Main.SetActive(false);
        });
       
    }
    public void _show()
    {
        Main.SetActive(true);
        Main.transform.DOScale(1,0.2f).From(0).SetEase(Ease.InOutSine).OnComplete(() => {

        });
        
    }
}
