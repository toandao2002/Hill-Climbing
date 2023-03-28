using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Unity.Netcode.Transports.UNET;
using UnityEngine.UI;
using TMPro;
using Unity.Netcode.Transports.UTP;
public class MyNetWork : MonoBehaviour
{
    NetworkClient networkClient = new NetworkClient();
    // Start is called before the first frame update
    public MyButton btn_client;
    public MyButton btn_server;
    public MyButton btn_host;
    public MyButton btn_stop;
    public TMP_InputField IpAddressField;
    void Start()
    {
        btn_client.onClick.AddListener(()=> {
            StartClient();
        });
        btn_server.onClick.AddListener(()=> {
            StartServer();
        });
        btn_host.onClick.AddListener(() => {
            StartHost();
        });
        btn_stop.onClick.AddListener(() => {
            stop();
        });
        
    }
    void changeServerIpAddress(string serverIPAddress)
    {
        if (serverIPAddress.Trim().Length == 0) return;  
      
         
        
        UnityTransport unt = (UnityTransport)NetworkManager.Singleton.NetworkConfig.NetworkTransport;
        
        unt.ConnectionData.Address = serverIPAddress;



    }
    public void StartClient()
    {
        changeServerIpAddress(IpAddressField.text);
        Debug.Log(IpAddressField.text);
        NetworkManager.Singleton.StartClient();
        // 192.168.1.85
    }
    public void StartServer()
    {
        changeServerIpAddress(IpAddressField.text);
        NetworkManager.Singleton.StartServer();
    }
    public void StartHost()
    {
        changeServerIpAddress(IpAddressField.text);
        NetworkManager.Singleton.StartHost();
    }
    public void stop() {
        NetworkManager.Singleton.DisconnectClient(NetworkManager.Singleton.LocalClientId);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
