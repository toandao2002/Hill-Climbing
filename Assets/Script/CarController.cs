using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
   
    public Rigidbody2D FrontTire;
    public Rigidbody2D BackTire;
    public float Force;
    public bool _gas, _brake;
    public float ForceTorque;
    public Rigidbody2D Rig;
    public float Fuel = 1;
    public bool TwoEngine = false;
    public bool Lose = false;
    public ParticleSystem fx;
    // Start is called before the first frame update
    void Start()
    {
        Rig = GetComponent<Rigidbody2D>();
    }
    private void OnEnable()
    {
        MyEvent.Gas += Gas;
        MyEvent.ReleaseGas += ReleaseGas;
        MyEvent.Brake += Brake;
        MyEvent.ReleaseBrake += ReleaseBrake;
        MyEvent.ChangecFuel += UpdateFuel;
        MyEvent.GameLose += GameLose;
        StartCoroutine(MoveToFoward());
        StartCoroutine(FuelDown(3, 0.05f));
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
        Lose = true;
        
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
         
        while (true || !Lose)
        {
            if (Fuel <= 0)
            {
                fx.gameObject.SetActive(false);

            }
            else if  (_brake )
            {
                if (TwoEngine)  
                    FrontTire.AddTorque(TimeDelay * Force , forceMode2D);
                BackTire.AddTorque(TimeDelay * Force , forceMode2D);
                Rig.AddTorque(-TimeDelay * ForceTorque , forceMode2D);
                fx.gameObject.SetActive(true);
                fx.transform.rotation =  Quaternion.EulerAngles(0, -30, 0);

            }
            else
            {
                if (_gas)
                {
                    if (TwoEngine)
                        FrontTire.AddTorque(-TimeDelay * Force , forceMode2D );
                    BackTire.AddTorque(-TimeDelay * Force , forceMode2D);
                    Rig.AddTorque(TimeDelay * ForceTorque , forceMode2D);
                    fx.gameObject.SetActive(true);
                    fx.transform.rotation = Quaternion.EulerAngles(0, 30, 0);

                }
                else
                {
                    fx.gameObject.SetActive(false);

                }
            }
           
         
            yield return new WaitForSeconds(TimeDelay);
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
                if (!Lose)
                    MyEvent.GameLose?.Invoke();
                break;
            }
           
           
        }
    }

}
