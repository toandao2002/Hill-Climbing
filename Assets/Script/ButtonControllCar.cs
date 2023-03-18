using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonControllCar : MonoBehaviour
{

    public MyButton GasBtn;
    public MyButton BrakeBtn;

    // Start is called before the first frame update
    void Start()
    {
        GasBtn.onDown.AddListener(Gas);
        GasBtn.onUp.AddListener(NotGas);
        BrakeBtn.onDown.AddListener(Brake);
        BrakeBtn.onUp.AddListener(NotBrake);
    }
    void Effect(GameObject obj , bool down)
    {
        if (down)
        {
            obj.transform.GetChild(0).gameObject.SetActive(false);
            obj.transform.GetChild(1).gameObject.SetActive(true);
        }
        else
        {
            obj.transform.GetChild(0).gameObject.SetActive(true);
            obj.transform.GetChild(1).gameObject.SetActive(false);
        }

    }
    public void Gas()
    {
        MyEvent.Gas?.Invoke();
        Effect(GasBtn.gameObject, true);
    }
    public void NotGas()
    {
        MyEvent.ReleaseGas?.Invoke();
        
        Effect(GasBtn.gameObject, false);
    }
    public void Brake()
    {
        MyEvent.Brake?.Invoke();
        Effect(BrakeBtn.gameObject, true);
    }
    public void NotBrake()
    {
        MyEvent.ReleaseBrake?.Invoke();
        Effect(BrakeBtn.gameObject, false);
    }

}
