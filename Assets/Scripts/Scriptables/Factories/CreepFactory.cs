using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "Assets/ScriptableObjects/Creeps/New Creep Factory", order = 0, fileName = "New Creep Factory")]
public class CreepFactory : ScriptableObject
{
    [SerializeField] private GameObject baseCreep;
    [SerializeField] private GameObject frostCreep;

    [SerializeField] private GameObject healthbarComponent;

    private float threshold = 0.65f;

    public List<BaseCreep> FabricateRandomCreepWave(int amount, Transform position, Canvas canvas)
    {
        var creeps = new List<BaseCreep>();
        for (int i = 0; i < amount; i++)
        {
            var creep = FabricateRandomCreep(position).GetComponent<BaseCreep>();
            creeps.Add(creep);
        }

        return creeps;
    }
    private GameObject FabricateRandomCreep(Transform position)
    {
        var odds = Random.Range(0f, 1f);

        if (odds >= threshold) Instantiate(baseCreep,position);

        return Instantiate(frostCreep,position);
    }

    public GameObject FabricateBaseCreep(Transform position)
    {
        return Instantiate(baseCreep,position);
    }
    
    public GameObject FabricateFrostCreep(Transform position)
    {
        return Instantiate(frostCreep,position);
    }
}
