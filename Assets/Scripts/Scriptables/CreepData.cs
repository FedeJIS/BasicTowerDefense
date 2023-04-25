using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Assets/ScriptableObjects/Creeps/New Creep Data", order = 0, fileName = "New Creep Data")]
public class CreepData : ScriptableData
{
    [SerializeField] private int damagePoints;
    [SerializeField] private int health;
    [SerializeField] private int speed;

    public int Damage => damagePoints;
    public int Health => health;
    public int Speed => speed;
}
