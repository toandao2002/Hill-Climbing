using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class ScreenGamePlay : MonoBehaviour
{
    public TMP_Text Coin; 
    public TMP_Text Gem;
    public Image BarFuel;
    public MyButton Pause;
    public void Start()
    {
        Pause.onClick.AddListener(()=> {
            ManagePopUp.Instance.ShowPaused();
        });
        Coin.text = DataGame.GetCoin().ToString();
        Gem.text = DataGame.GetGem().ToString();
    }
    // Start is called before the first frame update
    private void OnEnable()
    {
        MyEvent.IncCoin += UpdateCoin;
        MyEvent.IncGen += UpdateGen ;
        MyEvent.ChangecFuel += UpdateFuel;

    }
    
    public void UpdateFuel( float value)
    {
        if (value <= 0.3f)
        {
            BarFuel.color = new Color32(255, 0, 0,255);
        }
        else
        {
            BarFuel.color = new Color32(255, 255,255, 255);

        }
        BarFuel.fillAmount = value;
    }
    private void OnDisable()
    {
        MyEvent.IncCoin -= UpdateCoin;
        MyEvent.IncGen -= UpdateGen;
        MyEvent.ChangecFuel -= UpdateFuel;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void UpdateCoin (int value)
    {
        Coin.text =GameController.instance.UpdateCoin(value).ToString();
        EffectUpDown(Coin.gameObject);

    }
    void UpdateGen (int Value)
    {
        Gem.text = GameController.instance.UpdatGen(Value).ToString();
        EffectUpDown(Gem.gameObject);
    }
    public float duration= 0.02f;
    public void EffectUpDown(GameObject obj)
    {
        obj.transform.DOKill();
        obj.transform.DOScale(1.3f, duration).From(1).SetLoops(2,LoopType.Yoyo);
    }
}
