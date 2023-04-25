using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Assets/ScriptableObjects/Creeps/New Creep Data", order = 0, fileName = "New Creep Data")]
public class CreepData : ScriptableData
{
    [SerializeField] private float damagePoints;
    [SerializeField] private float health;
    [SerializeField] private float speed;
    [SerializeField] private float reward;

    public float Damage => damagePoints;
    public float Health => health;
    public float Speed => speed;

    public float Reward => reward;
}
