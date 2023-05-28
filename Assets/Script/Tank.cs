using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : CarController
{
    public List<WheelJoint2D> Wheels = new List<WheelJoint2D>();
    public GameObject chaintrack;
    public void Start()
    {
        Rig = GetComponent<Rigidbody2D>();
        int speed = DataGame.Get(DataGame.CarToolEngine + DataGame.GetCar());
        if (speed == 0) speed = 1000;
        ForceTire = new Vector2(speed, 0);
        friction = DataGame.Get(DataGame.CarToolTire + DataGame.GetCar());
        if (friction == 0) friction = 1;
        for (int i = 0; i< chaintrack.transform.childCount; i++)
        {
            var chain = chaintrack.transform.GetChild(i);
            chain.GetComponent<Rigidbody2D>().sharedMaterial.friction = friction;
            chain.GetComponent<Rigidbody2D>().sharedMaterial = chain.GetComponent<Rigidbody2D>().sharedMaterial;
        }
       
       
        foreach ( var i in GetComponents<WheelJoint2D>())
        {
            Wheels.Add(i);
        }
    }
    public override IEnumerator MoveToFoward()
    {


        while ((true || !Lose))
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
                else if (_brake)
                {
                    /*if (TwoEngine)
                        AddForceTorque(FrontTire, ForceTire);
                    //FrontTire.AddTorque(TimeDelay * ForceTire , forceMode2D);
                    //BackTire.AddTorque(TimeDelay * ForceTire , forceMode2D);
                    AddForceTorque(BackTire, ForceTire);*/
                    move(ForceTire.x);
                    Rig.AddTorque(-TimeDelay * ForceTorque, forceMode2D);
                    fx.gameObject.SetActive(true);
                    fx.transform.rotation = Quaternion.EulerAngles(0, -30, 0);

                }
                else
                {
                    if (_gas)
                    {
                        /*if (TwoEngine)
                            FrontTire.AddTorque(-TimeDelay * 200, forceMode2D);
                        BackTire.AddTorque(-TimeDelay * 200 , forceMode2D);*/

                      /*  if (TwoEngine)
                            AddForceTorque(FrontTire, -ForceTire);
                        //FrontTire.AddTorque(TimeDelay * ForceTire , forceMode2D);
                        //BackTire.AddTorque(TimeDelay * ForceTire , forceMode2D);
                        AddForceTorque(BackTire, -ForceTire);*/
                        move(-ForceTire.x);
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
    Vector2 force_tor;
    public void  move(float speed)
    {

        force_tor = Vector2.SmoothDamp(new Vector2(Wheels[0].motor.motorSpeed, 0),new Vector2( speed,0), ref velocity_tor, smoothTime);
        if (speed > 0)
        {
            speed = Mathf.Max(Wheels[0].motor.motorSpeed, force_tor.x);
        }
        else
        {
            speed = Mathf.Min(Wheels[0].motor.motorSpeed, force_tor.x);
        }
        foreach (var i in Wheels)
        {
            var tmp = i.motor ;
            tmp.motorSpeed = speed;
            i.motor = tmp;
        }
    }
    void useMoto(bool b)
    {
        foreach (var i in Wheels)
        {
            i.useMotor = b;
        }
    }
    public override void Gas()
    {
        _gas = true;
        ManangeAudio.Instacne.PlaySound(NameSound.StartRun);
        ManangeAudio.Instacne.LoopAudio(NameSound.CarRun, 1);
        useMoto(true);

    }
    public override void ReleaseGas()
    {
        _gas = false;
        useMoto(false);
        ManangeAudio.Instacne.LoopAudio(NameSound.CarIdle, 1);
    }

    public override void Brake()
    {
        useMoto(true);
        ManangeAudio.Instacne.PlaySound(NameSound.StartRun);
        ManangeAudio.Instacne.LoopAudio(NameSound.CarRun, 1);
        _brake = true;
    }
    public override void  ReleaseBrake()
    {
        useMoto(false);
        ManangeAudio.Instacne.LoopAudio(NameSound.CarIdle, 1);
        _brake = false;
    }
}
