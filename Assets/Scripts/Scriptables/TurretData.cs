using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Assets/ScriptableObjects/Turrets/New Turret Data", order = 0, fileName = "New Turret Data")]
[Serializable]
public class TurretData : ScriptableData
{
    [SerializeField] private int damagePoints;
    [SerializeField] private int buildCost;

    public int DamagePoints => damagePoints;
    public int BuildCost => buildCost;

}
