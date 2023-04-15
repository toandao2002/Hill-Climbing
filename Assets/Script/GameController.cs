using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.Netcode;
using Unity.Netcode.Components;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    public int Coin;
    public int Gen;
    public int id;
    public bool GameLose = false;
    public GameObject MyCar;
    public Transform PosFirstCar;
    public bool ModeMultilPlayer = true;
    public bool ModeGameOnline = false;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
       
       
        
    }
    void Start()
    {

        InitializeCarOffliner();

        Coin = DataGame.GetCoin();
        Gen = DataGame.GetGem();

    }
    void InitializeCarOffliner()
    {
       
     
        id = DataGame.GetCar();
        Debug.Log($"/Vehicle/Car {id}");
        var car = Instantiate(Resources.Load<GameObject>($"Vehicle/Car {id}"));
         
        MyCar = car;

        car.transform.position = PosFirstCar.position;
        MyCamera.Instance.SetTarGet(car);
    }
    // Start is called before the first frame update
    public void Game_Lose()
    {
        if (!GameLose)
        {
            GameLose = true;
            StartCoroutine(CaptureImage());
        }
        
        
    }
   
    bool Finished = false;
    IEnumerator CaptureImage()
    {
        StartCoroutine(captureScreenshot());
 
        yield return new WaitForEndOfFrame();
  
        bool ok = false;
     
    
        ManagePopUp.Instance.ShowGameLose();
        
    }
    public Image imageDisplay;
 
     
    private void OnEnable()
    {
    
    }
    IEnumerator captureScreenshot()
    {
        yield return new WaitForEndOfFrame();
 
        Texture2D texture = new Texture2D(Screen.width, Screen.height);
        //Get Image from screen
        texture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        texture.Apply();
        var tmp = Camera.main.WorldToScreenPoint(MyCar.transform.position);
        imageDisplay.sprite = Sprite.Create(texture, new Rect(tmp.x -250,  tmp.y-200, 500, 435), new Vector2(0,0)); ;
        
    }
    public int UpdateCoin(int value)
    {
        Coin += value;
        return Coin;
    }
    public int UpdatGen(int value)
    {
        Gen += value;
        return Gen;
    }


    ////////////network
    public bool CheckIsHost()
    {
         
        return (MyCar.GetComponent<CarController>().IsOwner|| NetworkManager.Singleton.IsHost);
    }


    public void OnDestroy()
    {
        DataGame.SetCoin(Coin);
        DataGame.SetGen(Gen);
        DataGame.Save();
       
    }
    public void OnDisable()
    {
       

    }



}
