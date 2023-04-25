using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "Assets/ScriptableObjects/Creeps/New Creep Factory", order = 0, fileName = "New Creep Factory")]
public class CreepFactory : ScriptableObject
{
    [SerializeField] private CreepData[] creepsData;

    private Dictionary<CreepType, CreepData> _creepMap;

    private const string TurretsPath = "ScriptableData/CreepsData";
    
    public void InitializeCreepFactory()
    {
        creepsData = Resources.LoadAll<CreepData>(TurretsPath);
        _creepMap = new Dictionary<CreepType, CreepData>();

        foreach (var creepData in creepsData)
        {
            if (_creepMap.ContainsKey(creepData.Type)) continue;
            _creepMap.Add(creepData.Type, creepData);
        }
    }
    public GameObject FabricateRandomCreep(Transform position)
    {
        return Instantiate(GetRandomCreep(), position);
    }

    private GameObject GetRandomCreep()
    {
        System.Random rand = new System.Random();
        var count = _creepMap.Count;
        int index = rand.Next(0, count);
        var data =  _creepMap.ElementAt(index).Value;
        return data.Prefab;
    }

}
