using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum NameTune
{
    Engine,
    Suspension,
    Tire,
    Downforce,
}
public class BoxUpgrate : MonoBehaviour
{
    public NameTune nameTune;
    public MyButton btn;
    public TMP_Text level;
    public TMP_Text Coin;
    public Tool tool;
    public int num_level;
    // Start is called before the first frame update
    void Start()
    {
        num_level = DataGame.Get(tool.nameTune.ToString());
        
        Coin.text = tool.Coin[num_level].ToString();
        level.text = (1+ num_level) + "/" + tool.Coin.Count;
        nameTune = tool.nameTune;
        btn.onClick.AddListener(ShowPopUpgrate);
    }
    public void ShowPopUpgrate()
    {
        ManagePopUp.Instance.upgratePopUp.Show();
        ManagePopUp.Instance.upgratePopUp.SetData(tool);
    }
    
}
