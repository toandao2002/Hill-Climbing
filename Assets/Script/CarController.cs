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
    public  void Start()
    {
        BackTire.GetComponent<Tire>().IsOwner = IsOwner || !GameController.instance.ModeGameOnline;
        Rig = GetComponent<Rigidbody2D>();
        int speed = DataGame.Get(DataGame.CarToolEngine + DataGame.GetCar());
        if (speed == 0) speed = 1200;
        
        ForceTire = new Vector2(speed, 0);
        friction = DataGame.GetF(DataGame.CarToolTire + DataGame.GetCar());
        if (friction == 0) friction = 1;
        suspension = DataGame.Get(DataGame.CarToolSuspension + DataGame.GetCar());
        if (suspension == 0) suspension = 3;
        ChangeFriction();
        var wheelJoint2D = GetComponents<WheelJoint2D>();
        ForceTorque -= DataGame.Get(DataGame.CarToolDownForce + DataGame.GetCar());
        
        
        foreach (WheelJoint2D i in wheelJoint2D)
        {
            var temp = i.suspension;

            temp.frequency = suspension;
            i.suspension = temp;
        }

        if (DataGame.Get(DataGame.Stage)== 2)
        {
            gravity(-2f);
        }

        int layer =6;
     /*   for (int i = 6;i <=10; i++)
        {
            if (!GameController.instance.layers.Contains(i))
            {
                layer = i;
                GameController.instance.layers.Add(i);
                break;
            }
        }*/
        
        ChangeChildrenLayer(gameObject, layer);
        gameObject.layer = layer;
    }
    public void gravity(float val)
    {
        Rig.gravityScale = val;
    }
    private void ChangeChildrenLayer(GameObject parent, int layer)
    {
        parent.layer = layer;
        for (int i = 0; i < parent.transform.childCount; i++)
        {
            parent.transform.GetChild(i).gameObject.layer = layer;
            ChangeChildrenLayer(parent.transform.GetChild(i).gameObject, layer);
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
    int deg = 0;

    void Update()
    {
        if (!(IsOwner || !GameController.instance.ModeGameOnline)) return;
        if ( deg ==0 && transform.localRotation.eulerAngles.z >=90 && transform.localRotation.eulerAngles.z <= 180)
        {
            deg++;
        }
        else if ( deg ==1 && transform.localRotation.eulerAngles.z >= 180 && transform.localRotation.eulerAngles.z <= 270)
        {
            deg++;
        }
        else if (deg == 2 && transform.localRotation.eulerAngles.z >= 270)
        {
            deg++;
        }
        else  if ( transform.localRotation.eulerAngles.z >= 350)
        {
            if (deg == 3)
            {
               
                showTextTurn();
            }
            deg = 0;
        }

    }
    public void showTextTurn()
    {
        BackFlip.instance.ShowBackFlip();
        ManangeAudio.Instacne.PlaySound(NameSound.Coin);
        MyEvent.IncCoin?.Invoke(200);
    }
    public void GameLose()
    {
        if (!IsOwner && GameController.instance.ModeGameOnline) return;                          
        Lose = true;
        GameController.instance.Game_Lose();
        
    }
    public virtual void Gas()
    {
        _gas = true;
        ManangeAudio.Instacne.PlaySound(NameSound.StartRun);
        ManangeAudio.Instacne.LoopAudio(NameSound.CarRun, 1);
        EffectGround(-30, true);
    }
    public virtual void ReleaseGas()
    {
        _gas = false;
        ManangeAudio.Instacne.LoopAudio(NameSound.CarIdle, 1);
        EffectGround(-30, false);
    }
    
    public  virtual void Brake()
    {
        ManangeAudio.Instacne.PlaySound(NameSound.StartRun);
        ManangeAudio.Instacne.LoopAudio(NameSound.CarRun, 1);
        _brake = true;
        EffectGround(-120, true);
    }
    public virtual void ReleaseBrake()
    {
        ManangeAudio.Instacne.LoopAudio(NameSound.CarIdle, 1);
        _brake = false;
        EffectGround(-120, false);
    }
    public float TimeDelay = 0.02f;
    public ForceMode2D forceMode2D;
    
    public virtual IEnumerator  MoveToFoward()
    {
       
  
        while ((true || !Lose)   )
        {
            if (/*(networkObject.OwnerClientId == NetworkManager.Singleton.LocalClientId || networkObject.IsOwner) ||*/ IsOwner || !GameController.instance.ModeGameOnline)
            {
                if (GameController.instance.ModeGameOnline)
                    MyCamera.Instance.SetTarGet(gameObject);
                RPM.instacne.Zr = Mathf.Abs(BackTire.angularVelocity);
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
                      

                    }
                    
                }
                


            }
         
            yield return new WaitForSeconds(TimeDelay);
        }

    }
  
    public void EffectGround(int deg, bool val)
    {
        //fx.gameObject.SetActive(val);
        fx.loop = val;
        if(val) fx.Play();
        fx.transform.localRotation = Quaternion.Euler(  deg,-90,90 );
    }

    public Vector2 velocity_tor;
    void AddForceTorque(Rigidbody2D obj_rig, Vector2 target)
    {

        
        Vector2 force_tor = Vector2.SmoothDamp(new Vector2(obj_rig.angularVelocity , 0 ), target,ref velocity_tor,smoothTime );
        if (target.x > 0)
        {
            obj_rig.angularVelocity = Mathf.Max(obj_rig.angularVelocity, force_tor.x);
        }
        else
        {
            obj_rig.angularVelocity = Mathf.Min(obj_rig.angularVelocity, force_tor.x);
        }
       
         
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

    void Handle(int offset, int [] a )
    {
        int size = a.Length;
        offset %= size;
         
        if (offset <= 0)
        {
            for (int i = -offset; i < size; i++)
            {
                print(a[i] + " ");
            }
            for (int i = 0; i < -offset; i++)
            {
                print(a[i] + " ");
            }
        }
        else
        {
            for (int i = size - offset; i < size; i++)
            {
                print(a[i] + " ");
            }
            for (int i = 0; i < size - offset; i++)
            {
                print(a[i] + " ");
            }

        }
    }
    private void OnDestroy()
    {
        StopAllCoroutines();
     
    }
}
