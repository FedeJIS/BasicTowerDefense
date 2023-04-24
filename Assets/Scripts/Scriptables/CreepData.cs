using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Assets/ScriptableObjects/Creeps/New Creep Data", order = 0, fileName = "New Creep Data")]
public class CreepData : ScriptableObject
{
    public int Damage;
    public int Health;
    public int Speed;
}
