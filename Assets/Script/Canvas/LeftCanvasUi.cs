using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LeftCanvasUi : MonoBehaviour
{
    public static LeftCanvasUi Instance;
    private void Awake()
    {
        if (Instance == null) Instance = this;
    }
    private void OnEnable()
    {
        MyEvent.IncCoin += SetCoin;   
    }
    private void OnDisable()
    {
        MyEvent.IncCoin -= SetCoin;

    }
    public TMP_Text Coin;
    public TMP_Text Gem;

    private void Start()
    {
        SetCoin(DataGame.GetCoin());
        SetGem(DataGame.GetGem());
    }
    public void SetCoin(int value)
    {
        Coin.text = value.ToString();
    }
    public void SetGem (int value)
    {
        Gem.text = value.ToString();
    }
}
