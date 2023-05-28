using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Tool", menuName = "Data/Tool", order = 2)]
[System.Serializable]
public class Tool :ScriptableObject
{
    public NameTune nameTune;
    public List<int> Coin;
    public List<int> Fricion;
    public List<int> DownForce;
    public List<int> Engine;
    public List<int> Suspension;
    public List<float> Tire;
    public float tmp;
    public string content;
}
