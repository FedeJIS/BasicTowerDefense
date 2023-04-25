using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Assets/ScriptableObjects/Turrets/New Turret Data", order = 0, fileName = "New Turret Data")]
[Serializable]
public class TurretData : ScriptableObject
{
    [SerializeField] private int damagePoints;
    [SerializeField] private int buildCost;
    [SerializeField] private GameObject prefab;
    [SerializeField] private TurretType type;
    
    public int DamagePoints => damagePoints;
    public int BuildCost => buildCost;
    public GameObject Prefab => prefab;

    public TurretType Type => type;

}

public enum TurretType
{
    BASE,
    FROST,
}
