using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "Tune", menuName = "Data/Tune", order = 1)]
public class Tune : ScriptableObject
{
    public List<Tool> tools;
}
