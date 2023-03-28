using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;

public class PopUpGameLose : BasePopUp
{
    public static PopUpGameLose Instance;
    public MyButton Continue;
    public TMP_Text Distance;
    public TMP_Text Coin_txt;
    public TMP_Text Gem_txt;
    public Image img_gameLose;
   
    public GameObject GameLoseParentImg;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    private void Start()
    {

        Continue.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("MainMenu");
        });
        Continue.transform.DOScale(1.3f, 0.3f).SetLoops(-1, LoopType.Yoyo);
    }
    private void OnEnable()
    {

        StartCoroutine( CaptureImg());

        Coin_txt.text ="+"+ (GameController.instance.Coin - DataGame.GetCoin()).ToString()+ " COINS";
        Gem_txt .text ="+"+ (GameController.instance.Gen - DataGame.GetGem()).ToString()+ " GEMS";
        if (GameController.instance.MyCar != null)
            Distance.text = "DISTANCE:" +( (int)(GameController.instance.MyCar.transform.position.x - GameController.instance.PosFirstCar.position.x) )+"m";
    }
    public float duration;
    IEnumerator CaptureImg()
    {
        img_gameLose.sprite = GameController.instance.imageDisplay.sprite;
        ManangeAudio.Instacne.PlaySound(NameSound.CaputureScreen);
        GameLoseParentImg.transform.DORotate(new Vector3(2, 0, 0), duration, RotateMode.FastBeyond360).From(new Vector3(0, 0, 90));
        GameLoseParentImg.transform.DOScale(1, duration).From(0);
        yield return new WaitForSeconds(1);
    }
}



