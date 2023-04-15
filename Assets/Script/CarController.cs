using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class CarController : NetworkBehaviour
{
   
    public Rigidbody2D FrontTire;
    public Rigidbody2D BackTire;
    public float smoothTime = 0.02f;
    public Vector2 ForceTire;
    public bool _gas, _brake;
    public float ForceTorque;
    public Rigidbody2D Rig;
    public float Fuel = 1;
    public bool TwoEngine = false;
    public bool Lose = false;
    public ParticleSystem fx;
    NetworkObject networkObject;
    public float friction;
    public float suspension;
    // Start is called before the first frame update
    void Start()
    {
        Rig = GetComponent<Rigidbody2D>();
        ForceTire = new Vector2(DataGame.Get(DataGame.CarToolEngine + DataGame.GetCar()), 0);
        friction = DataGame.Get(DataGame.CarToolTire + DataGame.GetCar());
        suspension = DataGame.Get(DataGame.CarToolSuspension + DataGame.GetCar());
        ChangeFriction();
        var wheelJoint2D = GetComponents<WheelJoint2D>();
        foreach (WheelJoint2D i in wheelJoint2D)
        {
            var temp = i.suspension;
            temp.frequency =  suspension;
            i.suspension = temp;
        }
    }
    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        GameController.instance.MyCar =  this.gameObject;
    }
    private void OnEnable()
    {
        SetUpNetWork();

        MyEvent.Gas += Gas;
        MyEvent.ReleaseGas += ReleaseGas;
        MyEvent.Brake += Brake;
        MyEvent.ReleaseBrake += ReleaseBrake;
        MyEvent.ChangecFuel += UpdateFuel;
        MyEvent.GameLose += GameLose;
        StartCoroutine(MoveToFoward());
        StartCoroutine(FuelDown(3, 0.05f));
    }
    public void SetUpNetWork()
    {
        networkObject = GetComponent<NetworkObject>();

        
    }
    private void OnDisable()
    {
        MyEvent.Gas -= Gas;
        MyEvent.ReleaseGas -= ReleaseGas;
        MyEvent.Brake -= Brake;
        MyEvent.ReleaseBrake -= ReleaseBrake;
        MyEvent.ChangecFuel -= UpdateFuel;
        MyEvent.GameLose -= GameLose;

        StopCoroutine(MoveToFoward());
        StopCoroutine(FuelDown(3, 0.05f));
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        
    }
    public void GameLose()
    {
        if (!IsOwner && GameController.instance.ModeGameOnline) return;                          
        Lose = true;
        GameController.instance.Game_Lose();
        
    }
    public void Gas()
    {
        _gas = true;
    }
    public void ReleaseGas()
    {
        _gas = false;
        
    }
    
    public void Brake()
    {
        _brake = true;
    }
    public void ReleaseBrake()
    {
        _brake = false;
    }
    public float TimeDelay = 0.02f;
    public ForceMode2D forceMode2D;
    
    IEnumerator MoveToFoward()
    {
       
  
        while ((true || !Lose)   )
        {
            if (/*(networkObject.OwnerClientId == NetworkManager.Singleton.LocalClientId || networkObject.IsOwner) ||*/ IsOwner || !GameController.instance.ModeGameOnline)
            {
                if (GameController.instance.ModeGameOnline)
                    MyCamera.Instance.SetTarGet(gameObject);
                if (Fuel <= 0)
                {
                    fx.gameObject.SetActive(false);

                }
                else if  (_brake )
                {
                    if (TwoEngine)
                        AddForceTorque(FrontTire, ForceTire);
                    //FrontTire.AddTorque(TimeDelay * ForceTire , forceMode2D);
                    //BackTire.AddTorque(TimeDelay * ForceTire , forceMode2D);
                    AddForceTorque(BackTire, ForceTire);
                    Rig.AddTorque(-TimeDelay * ForceTorque , forceMode2D);
                    fx.gameObject.SetActive(true);
                    fx.transform.rotation =  Quaternion.EulerAngles(0, -30, 0);

                }
                else
                {
                    if (_gas)
                    {
                        /*if (TwoEngine)
                            FrontTire.AddTorque(-TimeDelay * 200, forceMode2D);
                        BackTire.AddTorque(-TimeDelay * 200 , forceMode2D);*/

                        if (TwoEngine)
                            AddForceTorque(FrontTire, -ForceTire);
                        //FrontTire.AddTorque(TimeDelay * ForceTire , forceMode2D);
                        //BackTire.AddTorque(TimeDelay * ForceTire , forceMode2D);
                        AddForceTorque(BackTire, -ForceTire);
                        Rig.AddTorque(TimeDelay * ForceTorque, forceMode2D);
                        fx.gameObject.SetActive(true);
                        fx.transform.rotation = Quaternion.EulerAngles(0, 30, 0);

                    }
                    else
                    {
                        fx.gameObject.SetActive(false);

                    }
                }
                


            }
         
            yield return new WaitForSeconds(TimeDelay);
        }

    }
    Vector2 velocity_tor;
    void AddForceTorque(Rigidbody2D obj_rig, Vector2 target)
    {
        Vector2 force_tor = Vector2.SmoothDamp(new Vector2(obj_rig.angularVelocity , 0 ), target,ref velocity_tor,smoothTime );
      
        obj_rig.angularVelocity = force_tor.x;
    }
    void UpdateFuel(float value ) {
        Fuel = value;
    }
   
    IEnumerator FuelDown(float duration , float value_change)
    {
        while (true)
        {
            yield return new WaitForSeconds(duration);
            if (Fuel >=0)
            {
                MyEvent.ChangecFuel?.Invoke(Fuel);
                Fuel -= value_change;
            }
            else
            {
                if (!Lose &&(IsOwner || !GameController.instance.ModeGameOnline))
                    MyEvent.GameLose?.Invoke();
                break;
            }
           
           
        }

    }
 
    public void ChangeFriction()
    {
        FrontTire.GetComponent<Rigidbody2D>().sharedMaterial.friction = friction;
        FrontTire.GetComponent<Rigidbody2D>().sharedMaterial = FrontTire.GetComponent<Rigidbody2D>().sharedMaterial;
        BackTire.GetComponent<Rigidbody2D>().sharedMaterial.friction = friction;
        BackTire.GetComponent<Rigidbody2D>().sharedMaterial = BackTire.GetComponent<Rigidbody2D>().sharedMaterial;
    }

    public bool CheckIsOwner()
    {
   
        return IsOwner;
    }



}
