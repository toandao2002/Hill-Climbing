using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MyEvent 
{
    //////////Control 
    public static Action Gas;
    public static Action ReleaseGas;
    public static Action Brake;
    public static Action ReleaseBrake;

    /////////////
    public static Action<int> IncCoin;
    public static Action<int> IncGen;
    public static Action<float> ChangecFuel;

    public static Action ChangeLevelTune;

    public static Action<Vector3, int> ChosedCar;
    public static Action<Vector3, int> ChosedState;

    //////
    public static Action GameLose;

}
