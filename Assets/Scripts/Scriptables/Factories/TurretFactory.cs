using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Assets/ScriptableObjects/Turrets/New Turret Factory", order = 0, fileName = "New Turret Factory")]
public class TurretFactory : ScriptableObject
{
    [SerializeField] private TurretData[] turretsData;

    private Dictionary<int, TurretData> _turretMap;
    private const string TurretsPath = "ScriptableData/TurretsData";
    private static int towerIndex;
    public void InitializeTurretFactory()
    {
        turretsData = Resources.LoadAll<TurretData>(TurretsPath);
        _turretMap = new Dictionary<int, TurretData>();

        foreach (var turretData in turretsData)
        {
            if (_turretMap.ContainsKey(turretData.Id)) continue;
            _turretMap.Add(turretData.Id, turretData);
        }
    }
    
    public GameObject CreateTurretFromId(int id)
    {
        var turretData = GetTurretFromId(id);

        if (turretData == null) return null;

        var turretInstance =  Instantiate(turretData.Prefab);
        
        turretInstance.name = "Turret_" + turretData.Type + "_" + towerIndex;

        towerIndex++;

        return turretInstance;
    }

    private TurretData GetTurretFromId(int id)
    {
        if (!_turretMap.ContainsKey(id)) return null;

        return _turretMap[id];
    }
}
