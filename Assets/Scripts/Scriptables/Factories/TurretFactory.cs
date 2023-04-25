using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Assets/ScriptableObjects/Turrets/New Turret Factory", order = 0, fileName = "New Turret Factory")]
public class TurretFactory : ScriptableObject
{
    [SerializeField] private TurretData[] turretsData;

    private Dictionary<TurretType, TurretData> _turretMap;
    private const string TurretsPath = "ScriptableData/TurretsData";
    private static int towerIndex;
    public void InitializeTurretFactory()
    {
        turretsData = Resources.LoadAll<TurretData>(TurretsPath);
        _turretMap = new Dictionary<TurretType, TurretData>();

        foreach (var turretData in turretsData)
        {
            if (_turretMap.ContainsKey(turretData.Type)) continue;
            _turretMap.Add(turretData.Type, turretData);
        }
    }
    
    public GameObject CreateTurretFromType(TurretType type)
    {
        var turretData = GetTurretFromType(type);

        if (turretData == null) return null;

        var turretInstance =  Instantiate(turretData.Prefab);
        
        turretInstance.name = "Turret_" + turretData.Type + "_" + towerIndex;

        towerIndex++;

        return turretInstance;
    }

    private TurretData GetTurretFromType(TurretType type)
    {
        if (!_turretMap.ContainsKey(type)) return null;

        return _turretMap[type];
    }
}
