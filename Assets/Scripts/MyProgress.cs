using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/MyStats", fileName = "New Stats")]

public class MyProgress : ScriptableObject
{
    public float bestTime;
    public int attemtpsAmount;

}
