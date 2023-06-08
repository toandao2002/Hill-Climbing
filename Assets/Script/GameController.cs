using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Unity.Netcode;
using Unity.Netcode.Components;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    public GameObject canvasModeOnl;
    public int Coin;
    public int Gen;
    public int id;
    public bool GameLose = false;
    public GameObject MyCar;
    public Transform PosFirstCar;
     
    public bool ModeGameOnline = false;
    public PlayerSpawner playerSpawner;
    public List<int> layers;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
      
    }
    void Start()
    {
        if (DataGame.GetMode() == 1)
        {
            canvasModeOnl.SetActive(true);
            ModeGameOnline = true;
        }
        else
        {
            canvasModeOnl.SetActive(false);
            ModeGameOnline = false;
        }
        AutoConnectServer();
        if (!ModeGameOnline)
          InitializeCarOffliner();
        else
        {
           
        }

        Coin = DataGame.GetCoin();
        Gen = DataGame.GetGem();

    }
    public void  initializeCarOnLine()
    {
        id = DataGame.GetCar();
        Debug.Log($"/Vehicle/Car {id}");
        GameObject car = (Resources.Load<GameObject>($"Vehicle/Car {id}"));
        // call server for get Player
        playerSpawner.SpawnPlayerServerRpc(NetworkManager.Singleton.LocalClientId, id);

    }
    public void AutoConnectServer()
    {
        string hostName = Dns.GetHostName(); // Retrive the Name of HOST
        Console.WriteLine(hostName);
        // Get the IP
        string myIP = Dns.GetHostByName(hostName).AddressList[0].ToString();
        Debug.Log(myIP);
        Console.WriteLine("My IP Address is :" + myIP);
        Console.ReadKey();
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
    bool captured=false;
    // Start is called before the first frame update
    public void Game_Lose()
    {
        if (!captured)
        {
            captured = true;
            StartCoroutine(CaptureImage());
        }
        
        
    }
    public void StopCorotine()
    {
        StopAllCoroutines();
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
        StopAllCoroutines();

    }
    public void OnDisable()
    {
       

    }

 
}
